using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using ECOLOGSemanticViewer.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ECOLOGSemanticViewer.Models.GraphModels;
using ECOLOGSemanticViewer.Models.EcologModels;
using System.Threading.Tasks;
using ECOLOGSemanticViewer.Utils;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class DetailComparePageViewModel : ViewModel
    {
        #region SelectedSemanticLink変更通知プロパティ
        private SemanticLink _SelectedSemanticLink;

        public SemanticLink SelectedSemanticLink
        {
            get
            { return _SelectedSemanticLink; }
            set
            { 
                if (_SelectedSemanticLink == value)
                    return;
                _SelectedSemanticLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region TripDirection変更通知プロパティ
        private TripDirection _TripDirection;

        public TripDirection TripDirection
        {
            get
            { return _TripDirection; }
            set
            { 
                if (_TripDirection == value)
                    return;
                _TripDirection = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region TripIDs変更通知プロパティ
        private List<int> _TripIDs;

        public List<int> TripIDs
        {
            get
            { return _TripIDs; }
            set
            { 
                if (_TripIDs == value)
                    return;
                _TripIDs = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region TripCategories変更通知プロパティ
        private List<DetailCompareGraphType.TripCategory> _TripCategories;

        public List<DetailCompareGraphType.TripCategory> TripCategories
        {
            get
            { return _TripCategories; }
            set
            { 
                if (_TripCategories == value)
                    return;
                _TripCategories = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CurrentTripCategory変更通知プロパティ
        private DetailCompareGraphType.TripCategory _CurrentTripCategory;

        public DetailCompareGraphType.TripCategory CurrentTripCategory
        {
            get
            { return _CurrentTripCategory; }
            set
            { 
                if (_CurrentTripCategory == value)
                    return;
                _CurrentTripCategory = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region GrapTypes変更通知プロパティ
        private List<DetailCompareGraphType.GraphTypes> _GrapTypes;

        public List<DetailCompareGraphType.GraphTypes> GrapTypes
        {
            get
            { return _GrapTypes; }
            set
            { 
                if (_GrapTypes == value)
                    return;
                _GrapTypes = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CurrentGraphType変更通知プロパティ
        private DetailCompareGraphType.GraphTypes _CurrentGraphType;

        public DetailCompareGraphType.GraphTypes CurrentGraphType
        {
            get
            { return _CurrentGraphType; }
            set
            { 
                if (_CurrentGraphType == value)
                    return;
                _CurrentGraphType = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region AxisXTypes変更通知プロパティ
        private List<DetailCompareGraphType.Axes> _AxisXTypes;

        public List<DetailCompareGraphType.Axes> AxisXTypes
        {
            get
            { return _AxisXTypes; }
            set
            { 
                if (_AxisXTypes == value)
                    return;
                _AxisXTypes = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CurrentAxisX変更通知プロパティ
        private DetailCompareGraphType.Axes _CurrentAxisX;

        public DetailCompareGraphType.Axes CurrentAxisX
        {
            get
            { return _CurrentAxisX; }
            set
            { 
                if (_CurrentAxisX == value)
                    return;
                _CurrentAxisX = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region LabelMinText変更通知プロパティ
        private string _LabelMinText;

        public string LabelMinText
        {
            get
            { return _LabelMinText; }
            set
            { 
                if (_LabelMinText == value)
                    return;
                _LabelMinText = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region PlotModelMin変更通知プロパティ
        private PlotModel _PlotModelMin;

        public PlotModel PlotModelMin
        {
            get
            { return _PlotModelMin; }
            set
            { 
                if (_PlotModelMin == value)
                    return;
                _PlotModelMin = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region LabelMedianText変更通知プロパティ
        private string _LabelMedianText;

        public string LabelMedianText
        {
            get
            { return _LabelMedianText; }
            set
            { 
                if (_LabelMedianText == value)
                    return;
                _LabelMedianText = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region PlotModelMedian変更通知プロパティ
        private PlotModel _PlotModelMedian;

        public PlotModel PlotModelMedian
        {
            get
            { return _PlotModelMedian; }
            set
            { 
                if (_PlotModelMedian == value)
                    return;
                _PlotModelMedian = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region PlotModelMode変更通知プロパティ
        private PlotModel _PlotModelMode;

        public PlotModel PlotModelMode
        {
            get
            { return _PlotModelMode; }
            set
            { 
                if (_PlotModelMode == value)
                    return;
                _PlotModelMode = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region LabelMaxText変更通知プロパティ
        private string _LabelMaxText;

        public string LabelMaxText
        {
            get
            { return _LabelMaxText; }
            set
            { 
                if (_LabelMaxText == value)
                    return;
                _LabelMaxText = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region PlotModelMax変更通知プロパティ
        private PlotModel _PlotModelMax;

        public PlotModel PlotModelMax
        {
            get
            { return _PlotModelMax; }
            set
            { 
                if (_PlotModelMax == value)
                    return;
                _PlotModelMax = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public DetailComparePageViewModel()
        {
        }

        public DetailComparePageViewModel(SemanticLink link, TripDirection direction)
        {
            this.SelectedSemanticLink = link;
            this.TripDirection = direction;

            Initialize();
        }

        public void Initialize()
        {
            TripCategories = DetailCompareGraphType.GetAllTripCategories();
            GrapTypes = DetailCompareGraphType.GetAllGraphTypes();
            AxisXTypes = DetailCompareGraphType.GetAllAxesTypes();

            DisplayGraph();
        }

        public async void DisplayGraph()
        {
            #region 代表トリップ検索
            TripIDs = new List<int>();
            await Task.Run(() =>
            {
                int tripID;
                switch (this.CurrentTripCategory)
                {
                    case DetailCompareGraphType.TripCategory.EnergyRepresentativeTrips:

                        tripID = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMinOfSemanticLink(" + SelectedSemanticLink.SemanticLinkId + ", '" + TripDirection.Direction + "')")
                            .AsEnumerable()
                            .Select(x => x.Field<int>("TripID"))
                            .ElementAt(0);
                        TripIDs.Add(tripID);

                        tripID = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMedianOfSemanticLink(" + SelectedSemanticLink.SemanticLinkId + ", '" + TripDirection.Direction + "')")
                            .AsEnumerable()
                            .Select(x => x.Field<int>("TripID"))
                            .ElementAt(0);
                        TripIDs.Add(tripID);

                        tripID = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMaxOfSemanticLink(" + SelectedSemanticLink.SemanticLinkId + ", '" + TripDirection.Direction + "')")
                            .AsEnumerable()
                            .Select(x => x.Field<int>("TripID"))
                            .ElementAt(0);
                        TripIDs.Add(tripID);

                        break;
                    case DetailCompareGraphType.TripCategory.TimeRepresentativeTrips:

                        tripID = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMinOfSemanticLink(" + SelectedSemanticLink.SemanticLinkId + ", '" + TripDirection.Direction + "')")
                            .AsEnumerable()
                            .Select(x => x.Field<int>("TripID"))
                            .ElementAt(0);
                        TripIDs.Add(tripID);

                        tripID = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMedianOfSemanticLink(" + SelectedSemanticLink.SemanticLinkId + ", '" + TripDirection.Direction + "')")
                            .AsEnumerable()
                            .Select(x => x.Field<int>("TripID"))
                            .ElementAt(0);
                        TripIDs.Add(tripID);

                        tripID = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMaxOfSemanticLink(" + SelectedSemanticLink.SemanticLinkId + ", '" + TripDirection.Direction + "')")
                            .AsEnumerable()
                            .Select(x => x.Field<int>("TripID"))
                            .ElementAt(0);
                        TripIDs.Add(tripID);

                        break;
                }

            });
            #endregion

            switch (CurrentGraphType)
            {
                case DetailCompareGraphType.GraphTypes.SpeedTransitionGraph:

                    createSpeedGraphModels();

                    break;
                case DetailCompareGraphType.GraphTypes.AccTransitionGraph:

                    createAccGraphModels();

                    break;
                case DetailCompareGraphType.GraphTypes.EnergyStackGraph:

                    createStackedEnergyGraphModels();

                    break;
            }
        }

        private async void createSpeedGraphModels()
        {
            for (int i = 0; i < TripIDs.Count; i++ )
            {
                List<Ecolog> ecologs = null;
                List<DetailCompareSeriesDatum> data = null;
                String axisXString = null;

                await Task.Run(() =>
                {
                    ecologs = Ecolog.ExtractEcolog(TripIDs[i], SelectedSemanticLink);

                    switch(CurrentAxisX){
                        case DetailCompareGraphType.Axes.Time:
                            axisXString = "Time [s]";
                            data = DetailCompareSeriesDatum.CreateTimeSpeedData(ecologs);
                            break;
                        case DetailCompareGraphType.Axes.Distance:
                            axisXString = "Distance [m]";
                            data = DetailCompareSeriesDatum.CreateDistanceSpeedData(ecologs);
                            break;
                    }
                });

                PlotModel plotModel = new PlotModel();

                LinearAxis axisX = new LinearAxis();
                axisX.Position = AxisPosition.Bottom;
                axisX.Title = axisXString;

                LinearAxis axisY = new LinearAxis();
                axisY.Title = "Speed [km/h]";

                //plotModel.Axes.Add(axisX);
                plotModel.Axes.Add(axisY);

                LineSeries series = new LineSeries();
                series.ItemsSource = data;
                series.DataFieldX = "X";
                series.DataFieldY = "Y";

                plotModel.Series.Add(series);

                switch (i)
                {
                    case 0:
                        LabelMinText = "Min trip: TripID = " + TripIDs[i] + ", StartTime = " + ecologs.Min(v => v.Jst);
                        PlotModelMin = plotModel;
                        break;
                    case 1:
                        LabelMedianText = "Median trip: TripID = " + TripIDs[i] + ", StartTime = " + ecologs.Min(v => v.Jst);
                        PlotModelMedian = plotModel;
                        break;
                    case 2:
                        LabelMaxText = "Max trip: TripID = " + TripIDs[i] + ", StartTime = " + ecologs.Min(v => v.Jst);
                        PlotModelMax = plotModel;
                        break;
                }
            }
        }

        private async void createAccGraphModels()
        {
            for (int i = 0; i < TripIDs.Count; i++)
            {
                List<Ecolog> ecologs = null;
                List<DetailCompareSeriesDatum> data = null;
                String axisXString = null;

                await Task.Run(() =>
                {
                    ecologs = Ecolog.ExtractEcolog(TripIDs[i], SelectedSemanticLink);

                    switch (CurrentAxisX)
                    {
                        case DetailCompareGraphType.Axes.Time:
                            axisXString = "Time [s]";
                            data = DetailCompareSeriesDatum.CreateTimeAccData(ecologs);
                            break;
                        case DetailCompareGraphType.Axes.Distance:
                            axisXString = "Distance [m]";
                            data = DetailCompareSeriesDatum.CreateDistanceAccData(ecologs);
                            break;
                    }
                });

                PlotModel plotModel = new PlotModel();

                LinearAxis axisX = new LinearAxis();
                axisX.Position = AxisPosition.Bottom;
                axisX.Title = axisXString;

                LinearAxis axisY = new LinearAxis();
                axisY.Title = "Acc [m/s^2]";

                //plotModel.Axes.Add(axisX);
                plotModel.Axes.Add(axisY);

                LineSeries series = new LineSeries();
                series.ItemsSource = data;
                series.DataFieldX = "X";
                series.DataFieldY = "Y";

                plotModel.Series.Add(series);

                switch (i)
                {
                    case 0:
                        LabelMinText = "Min trip: TripID = " + TripIDs[i] + ", StartTime = " + ecologs.Min(v => v.Jst);
                        PlotModelMin = plotModel;
                        break;
                    case 1:
                        LabelMedianText = "Median trip: TripID = " + TripIDs[i] + ", StartTime = " + ecologs.Min(v => v.Jst);
                        PlotModelMedian = plotModel;
                        break;
                    case 2:
                        LabelMaxText = "Max trip: TripID = " + TripIDs[i] + ", StartTime = " + ecologs.Min(v => v.Jst);
                        PlotModelMax = plotModel;
                        break;
                }
            }
        }

        private async void createStackedEnergyGraphModels()
        {
            for (int i = 0; i < TripIDs.Count; i++)
            {
                List<Ecolog> ecologs = null;
                List<DetailCompareSeriesDatum> data = null;
                String axisXString = null;

                await Task.Run(() =>
                {
                    ecologs = Ecolog.ExtractEcolog(TripIDs[i], SelectedSemanticLink);

                    switch (CurrentAxisX)
                    {
                        case DetailCompareGraphType.Axes.Time:
                            axisXString = "Time [s]";
                            data = DetailCompareSeriesDatum.CreateTimeStackData(ecologs);
                            break;
                        case DetailCompareGraphType.Axes.Distance:
                            axisXString = "Distance [m]";
                            data = DetailCompareSeriesDatum.CreateDistanceStackData(ecologs);
                            break;
                    }
                });

                PlotModel plotModel = new PlotModel();

                CategoryAxis axisX = new CategoryAxis();
                axisX.Position = AxisPosition.Bottom;
                axisX.ItemsSource = data;
                axisX.StringFormat = "0";
                axisX.MajorStep = 10;
                axisX.LabelField = "X";
                axisX.Title = axisXString;

                LinearAxis axisY = new LinearAxis();
                axisY.Title = "[kWh]";

                plotModel.Axes.Add(axisX);
                plotModel.Axes.Add(axisY);

                ColumnSeries seriesRolling = new ColumnSeries();
                seriesRolling.ItemsSource = data;
                seriesRolling.ValueField = "RollingLoss";
                seriesRolling.FillColor = OxyColors.Orange;
                seriesRolling.IsStacked = true;
                plotModel.Series.Add(seriesRolling);

                ColumnSeries seriesRegeneLoss = new ColumnSeries();
                seriesRegeneLoss.ItemsSource = data;
                seriesRegeneLoss.ValueField = "RegeneLoss";
                seriesRegeneLoss.FillColor = OxyColors.DeepPink;
                seriesRegeneLoss.IsStacked = true;
                plotModel.Series.Add(seriesRegeneLoss);

                ColumnSeries seriesAir = new ColumnSeries();
                seriesAir.ItemsSource = data;
                seriesAir.ValueField = "AirLoss";
                seriesAir.FillColor = OxyColors.Yellow;
                seriesAir.IsStacked = true;
                plotModel.Series.Add(seriesAir);

                ColumnSeries seriesConvertLoss = new ColumnSeries();
                seriesConvertLoss.ItemsSource = data;
                seriesConvertLoss.ValueField = "ConvertLoss";
                seriesConvertLoss.FillColor = OxyColors.Red;
                seriesConvertLoss.IsStacked = true;
                plotModel.Series.Add(seriesConvertLoss);

                switch (i)
                {
                    case 0:
                        LabelMinText = "Min trip: TripID = " + TripIDs[i] + ", StartTime = " + ecologs.Min(v => v.Jst);
                        PlotModelMin = plotModel;
                        break;
                    case 1:
                        LabelMedianText = "Median trip: TripID = " + TripIDs[i] + ", StartTime = " + ecologs.Min(v => v.Jst);
                        PlotModelMedian = plotModel;
                        break;
                    case 2:
                        LabelMaxText = "Max trip: TripID = " + TripIDs[i] + ", StartTime = " + ecologs.Min(v => v.Jst);
                        PlotModelMax = plotModel;
                        break;
                }
            }
        }
    }
}
