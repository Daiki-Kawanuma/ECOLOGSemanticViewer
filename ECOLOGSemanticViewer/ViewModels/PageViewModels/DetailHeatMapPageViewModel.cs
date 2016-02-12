using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using ECOLOGSemanticViewer.Models;
using ECOLOGSemanticViewer.Models.EcologModels;
using OxyPlot;
using ECOLOGSemanticViewer.Utils;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Data;
using System.Threading.Tasks;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class DetailHeatMapPageViewModel : ViewModel
    {
        private readonly int classNumber = 10;

        private SemanticLink semantciLink;
        private TripDirection direction;
        private List<Driver> drivers;
        private List<Car> cars;
        private List<Sensor> sensors;

        private DataTable dataTable;

        private double firstQuartileEnergy;
        private double secondQuartileEnergy;
        private double thirdQuartileEnergy;
        private double iqrEnergy;
        private double minExcludedEnergy;
        private double maxExcludedEnergy;
        private double classWidthEnergy;

        private double firstQuartileTime;
        private double secondQuartileTime;
        private double thirdQuartileTime;
        private double iqrTime;
        private double minExcludedTime;
        private double maxExcludedTime;
        private double classWidthTime;

        #region PlotModel変更通知プロパティ
        private PlotModel _PlotModel;

        public PlotModel PlotModel
        {
            get
            { return _PlotModel; }
            set
            {
                if (_PlotModel == value)
                    return;
                _PlotModel = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ProgressBarVisibility変更通知プロパティ
        private System.Windows.Visibility _ProgressBarVisibility;

        public System.Windows.Visibility ProgressBarVisibility
        {
            get
            { return _ProgressBarVisibility; }
            set
            {
                if (_ProgressBarVisibility == value)
                    return;
                _ProgressBarVisibility = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public DetailHeatMapPageViewModel()
        {
            
        }

        public void Initialize()
        {
        }

        public DetailHeatMapPageViewModel(SemanticLink semanticLink, TripDirection direction, List<Driver> drivers, List<Car> cars, List<Sensor> sensors)
        {
            this.semantciLink = semanticLink;
            this.direction = direction;
            this.drivers = drivers;
            this.cars = cars;
            this.sensors = sensors;

            this.ProgressBarVisibility = System.Windows.Visibility.Visible;

            createPlotModel();
        }


        private string createQuery()
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("DECLARE @SemanticLinkID INT");
            query.AppendLine("DECLARE @Direction VARCHAR(255)");
            query.AppendLine("DECLARE @LinksCount INT");
            query.AppendLine("SET @SemanticLinkID = " + semantciLink.SemanticLinkId);
            query.AppendLine("SET @Direction = '" + direction.Direction + "'");
            query.AppendLine("SET @LinksCount = (");
            query.AppendLine("		SELECT COUNT(*) AS LinksCount");
            query.AppendLine("		FROM SEMANTIC_LINKS");
            query.AppendLine("		WHERE SEMANTIC_LINK_ID = @SemanticLinkID");
            query.AppendLine("		);");

            query.AppendLine("WITH SelectedSemanticLink");
            query.AppendLine("AS (");
            query.AppendLine("	SELECT *");
            query.AppendLine("	FROM SEMANTIC_LINKS");
            query.AppendLine("	WHERE SEMANTIC_LINK_ID = @SemanticLinkID");
            query.AppendLine("	)");

            query.AppendLine("SELECT TRIP_ID");
            query.AppendLine("	,MIN(JST) AS DATE");
            query.AppendLine("	,COUNT(DISTINCT ECOLOG.LINK_ID) AS LinkCount");
            query.AppendLine("	,COUNT(*) AS TIME");
            query.AppendLine("	,SUM(DISTANCE_DIFFERENCE) AS SumDistance");
            query.AppendLine("	,AVG(SPEED) AS AverageSpeed");
            query.AppendLine("	,MAX(SPEED) AS MaxSpeed");
            query.AppendLine("	,MIN(SPEED) AS MinSpeed");
            query.AppendLine("	,SUM(CONSUMED_ELECTRIC_ENERGY) AS SumConsumedEnergy");
            query.AppendLine("	,SUM(LOST_ENERGY) AS SumLostEnergy");
            query.AppendLine("	,SUM(ABS(REGENE_LOSS)) AS SumRegeneLoss");
            query.AppendLine("	,SUM(ABS(CONVERT_LOSS)) AS SumConvertLoss");
            query.AppendLine("	,SUM(ENERGY_BY_ROLLING_RESISTANCE) AS SumRolling");
            query.AppendLine("	,SUM(ENERGY_BY_AIR_RESISTANCE) AS SumAir");
            query.AppendLine("FROM ECOLOG");
            query.AppendLine("INNER JOIN SelectedSemanticLink ON ECOLOG.LINK_ID = SelectedSemanticLink.LINK_ID");

            query.AppendLine("WHERE (ECOLOG.DRIVER_ID = 0");
            for (int i = 0; i < drivers.Count; i++)
            {
                query.AppendLine("  OR ECOLOG.DRIVER_ID = " + drivers[i].DriverId);
            }
            query.AppendLine("  )");

            query.AppendLine("AND (ECOLOG.CAR_ID = 0");
            for (int i = 0; i < cars.Count; i++)
            {
                query.AppendLine("  OR ECOLOG.CAR_ID = " + cars[i].CarId);
            }
            query.AppendLine("  )");

            query.AppendLine("AND (ECOLOG.SENSOR_ID = 0");
            for (int i = 0; i < sensors.Count; i++)
            {
                query.AppendLine("  OR ECOLOG.SENSOR_ID = " + sensors[i].SensorId);
            }
            query.AppendLine("  )");

            query.AppendLine("	AND TRIP_DIRECTION = @Direction");

            query.AppendLine("GROUP BY TRIP_ID");
            query.AppendLine("HAVING COUNT(DISTINCT ECOLOG.LINK_ID) > (@LinksCount * 0.75)");
            query.AppendLine("ORDER BY TRIP_ID");

            return query.ToString();
        }

        private void calculateEnergyParameter()
        {
            Tuple<double, double, double> quartiles = Quartiles(dataTable.AsEnumerable().OrderBy(r => r["SumLostEnergy"]).Select(r => r.Field<double>("SumLostEnergy")).ToList().ToArray());
            firstQuartileEnergy = quartiles.Item1;
            thirdQuartileEnergy = quartiles.Item3;
            iqrEnergy = thirdQuartileEnergy - firstQuartileEnergy;
            List<double> lostEnergies = dataTable.AsEnumerable()
                .Where(x => x.Field<double>("SumLostEnergy") > firstQuartileEnergy - 1.5 * iqrEnergy)
                .Where(x => x.Field<double>("SumLostEnergy") < thirdQuartileEnergy + 1.5 * iqrEnergy)
                .Select(x => x.Field<double>("SumLostEnergy")).ToList();

            minExcludedEnergy = lostEnergies.Min();
            maxExcludedEnergy = lostEnergies.Max();

            classWidthEnergy = (maxExcludedEnergy - minExcludedEnergy) / classNumber;
        }

        private void calculateTimeParameter()
        {

            Tuple<int, int, int> quartiles = Quartiles(dataTable.AsEnumerable().OrderBy(r => r["TIME"]).Select(r => r.Field<int>("TIME")).ToList().ToArray());
            firstQuartileTime = quartiles.Item1;
            thirdQuartileTime = quartiles.Item3;
            iqrTime = thirdQuartileTime - firstQuartileTime;
            List<int> times = dataTable.AsEnumerable()
                .Where(x => x.Field<int>("TIME") > firstQuartileTime - 1.5 * iqrTime)
                .Where(x => x.Field<int>("TIME") < thirdQuartileTime + 1.5 * iqrTime)
                .Select(x => x.Field<int>("TIME")).ToList();

            minExcludedTime = times.Min();
            maxExcludedTime = times.Max();

            classWidthTime = (maxExcludedTime - minExcludedTime) / classNumber;

        }

        private async void createPlotModel()
        {
            await Task.Run(() =>
            {
                this.dataTable = DatabaseAccesserEcolog.GetResult(createQuery());
            });
            
            calculateEnergyParameter();
            calculateTimeParameter();

            var plotModel = new PlotModel();
            plotModel.Subtitle = "SemananticLink: " + semantciLink.Semantics + ", Direction: " + direction.Direction;
            plotModel.Title = "Semantic Matrix";

            var linearColorAxis = new LinearColorAxis();
            linearColorAxis.HighColor = OxyColors.Gray;
            linearColorAxis.LowColor = OxyColors.Black;
            linearColorAxis.Position = AxisPosition.Right;
            plotModel.Axes.Add(linearColorAxis);

            var linearAxis1 = new LinearAxis();
            linearAxis1.Title = "Time";
            linearAxis1.Unit = "s";
            linearAxis1.Position = AxisPosition.Bottom;
            plotModel.Axes.Add(linearAxis1);
            var linearAxis2 = new LinearAxis();
            linearAxis2.Title = "Lost Energy";
            linearAxis2.Unit = "kWh";
            plotModel.Axes.Add(linearAxis2);

            var heatMapSeries1 = new HeatMapSeries();
            heatMapSeries1.LabelFormatString = "0";
            heatMapSeries1.X0 = minExcludedTime;
            heatMapSeries1.X1 = maxExcludedTime;
            heatMapSeries1.Y0 = minExcludedEnergy;
            heatMapSeries1.Y1 = maxExcludedEnergy;
            heatMapSeries1.LabelFontSize = 0.2;
            heatMapSeries1.Data = new Double[classNumber + 1, classNumber + 1];

            int count = 0; //  for debug

            double preTimeLevel = 0;
            double currentTimeLevel = minExcludedTime;

            for (int i = 0; i < classNumber + 1; i++)
            {
                double preEnergyLevel = 0;
                double currentEnergyLevel = minExcludedEnergy;

                for (int j = 0; j < classNumber + 1; j++)
                {
                    heatMapSeries1.Data[i, j] = dataTable.AsEnumerable()
                        .Where(x => x.Field<double>("SumLostEnergy") > preEnergyLevel)
                        .Where(x => x.Field<double>("SumLostEnergy") <= currentEnergyLevel)
                        .Where(x => x.Field<int>("TIME") > preTimeLevel)
                        .Where(x => x.Field<int>("TIME") <= currentTimeLevel).Count();

                    count += (int)heatMapSeries1.Data[i, j];

                    if (j == 0)
                    {
                        preEnergyLevel = minExcludedEnergy;
                    }
                    else
                    {
                        preEnergyLevel += classWidthEnergy;
                    }

                    currentEnergyLevel += classWidthEnergy;

                }

                if (i == 0)
                {
                    preTimeLevel = minExcludedTime;
                }
                else
                {
                    preTimeLevel += classWidthTime;
                }

                currentTimeLevel += classWidthTime;
            }

            plotModel.Series.Add(heatMapSeries1);

            this.ProgressBarVisibility = System.Windows.Visibility.Collapsed;
            this.PlotModel = plotModel;

        }

        internal Tuple<int, int, int> Quartiles(int[] afVal)
        {
            int iSize = afVal.Length;
            int iMid = iSize / 2; //this is the mid from a zero based index, eg mid of 7 = 3;

            double fQ1 = 0;
            double fQ2 = 0;
            double fQ3 = 0;

            if (iSize % 2 == 0)
            {
                //================ EVEN NUMBER OF POINTS: =====================
                //even between low and high point
                fQ2 = (afVal[iMid - 1] + afVal[iMid]) / 2;

                int iMidMid = iMid / 2;

                //easy split 
                if (iMid % 2 == 0)
                {
                    fQ1 = (afVal[iMidMid - 1] + afVal[iMidMid]) / 2;
                    fQ3 = (afVal[iMid + iMidMid - 1] + afVal[iMid + iMidMid]) / 2;
                }
                else
                {
                    fQ1 = afVal[iMidMid];
                    fQ3 = afVal[iMidMid + iMid];
                }
            }
            else if (iSize == 1)
            {
                //================= special case, sorry ================
                fQ1 = afVal[0];
                fQ2 = afVal[0];
                fQ3 = afVal[0];
            }
            else
            {
                //odd number so the median is just the midpoint in the array.
                fQ2 = afVal[iMid];

                if ((iSize - 1) % 4 == 0)
                {
                    //======================(4n-1) POINTS =========================
                    int n = (iSize - 1) / 4;
                    fQ1 = (afVal[n - 1] * .25) + (afVal[n] * .75);
                    fQ3 = (afVal[3 * n] * .75) + (afVal[3 * n + 1] * .25);
                }
                else if ((iSize - 3) % 4 == 0)
                {
                    //======================(4n-3) POINTS =========================
                    int n = (iSize - 3) / 4;

                    fQ1 = (afVal[n] * .75) + (afVal[n + 1] * .25);
                    fQ3 = (afVal[3 * n + 1] * .25) + (afVal[3 * n + 2] * .75);
                }
            }

            return new Tuple<int, int, int>((int)fQ1, (int)fQ2, (int)fQ3);

        }

        internal Tuple<double, double, double> Quartiles(double[] afVal)
        {
            int iSize = afVal.Length;
            int iMid = iSize / 2; //this is the mid from a zero based index, eg mid of 7 = 3;

            double fQ1 = 0;
            double fQ2 = 0;
            double fQ3 = 0;

            if (iSize % 2 == 0)
            {
                //================ EVEN NUMBER OF POINTS: =====================
                //even between low and high point
                fQ2 = (afVal[iMid - 1] + afVal[iMid]) / 2;

                int iMidMid = iMid / 2;

                //easy split 
                if (iMid % 2 == 0)
                {
                    fQ1 = (afVal[iMidMid - 1] + afVal[iMidMid]) / 2;
                    fQ3 = (afVal[iMid + iMidMid - 1] + afVal[iMid + iMidMid]) / 2;
                }
                else
                {
                    fQ1 = afVal[iMidMid];
                    fQ3 = afVal[iMidMid + iMid];
                }
            }
            else if (iSize == 1)
            {
                //================= special case, sorry ================
                fQ1 = afVal[0];
                fQ2 = afVal[0];
                fQ3 = afVal[0];
            }
            else
            {
                //odd number so the median is just the midpoint in the array.
                fQ2 = afVal[iMid];

                if ((iSize - 1) % 4 == 0)
                {
                    //======================(4n-1) POINTS =========================
                    int n = (iSize - 1) / 4;
                    fQ1 = (afVal[n - 1] * .25) + (afVal[n] * .75);
                    fQ3 = (afVal[3 * n] * .75) + (afVal[3 * n + 1] * .25);
                }
                else if ((iSize - 3) % 4 == 0)
                {
                    //======================(4n-3) POINTS =========================
                    int n = (iSize - 3) / 4;

                    fQ1 = (afVal[n] * .75) + (afVal[n + 1] * .25);
                    fQ3 = (afVal[3 * n + 1] * .25) + (afVal[3 * n + 2] * .75);
                }
            }

            return new Tuple<double, double, double>(fQ1, fQ2, fQ3);

        }
    }
}
