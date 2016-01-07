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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class DetailComparePageViewModel : ViewModel
    {
        #region GrapTypes変更通知プロパティ
        private List<string> _GrapTypes;

        public List<string> GrapTypes
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
        private string _CurrentGraphType;

        public string CurrentGraphType
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
        private List<string> _AxisXTypes;

        public List<string> AxisXTypes
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
        private string _CurrentAxisX;

        public string CurrentAxisX
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

        #region AxisYTypes変更通知プロパティ
        private List<string> _AxisYTypes;

        public List<string> AxisYTypes
        {
            get
            { return _AxisYTypes; }
            set
            { 
                if (_AxisYTypes == value)
                    return;
                _AxisYTypes = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CurrentAxisY変更通知プロパティ
        private string _CurrentAxisY;

        public string CurrentAxisY
        {
            get
            { return _CurrentAxisY; }
            set
            { 
                if (_CurrentAxisY == value)
                    return;
                _CurrentAxisY = value;
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
            Initialize();
        }

        public void Initialize()
        {
            GrapTypes = new List<string>() { "Speed transition graph" , "Acc transition graph", "Energy stack graph"};
            AxisXTypes = new List<string>() { "distance", "time"};
            AxisYTypes = new List<string>() { "AxisY_1", "AxisY_2", "AxisY_3" };

            this.PlotModelMin = createSpeedGraphModel();
            this.PlotModelMedian = createAccGrapModel();
            this.PlotModelMode = createStackedEnergyModel();
        }

        private PlotModel createSpeedGraphModel()
        {
            PlotModel plotModel = new PlotModel();

            LinearAxis axisX = new LinearAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.Title = "distance [m]";

            LinearAxis axisY = new LinearAxis();
            axisY.Title = "Speed [km/h]";

            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            LineSeries lineSeries = new LineSeries();

            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                lineSeries.Points.Add(new DataPoint(i, 30 + random.NextDouble() * 20));
            }

            plotModel.Series.Add(lineSeries);

            return plotModel;
        }

        private PlotModel createAccGrapModel()
        {
            PlotModel plotModel = new PlotModel();

            LinearAxis axisX = new LinearAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.Title = "distance [m]";

            LinearAxis axisY = new LinearAxis();
            axisY.Title = "Speed [km/h]";

            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            LineSeries lineSeries = new LineSeries();
            lineSeries.Color = OxyColors.Red;

            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                lineSeries.Points.Add(new DataPoint(i, 30 + random.NextDouble() * 20));
            }

            plotModel.Series.Add(lineSeries);

            return plotModel;
        }

        private PlotModel createStackedEnergyModel()
        {
            int seed = Environment.TickCount;
            Random random = new Random(seed);

            List<LevelAndValue> sourceRolling = new List<LevelAndValue>();
            for (int i = 0; i < 100; i++)
            {
                sourceRolling.Add(new LevelAndValue() { Level = i, Value = random.NextDouble()});
            }

            random = new Random(++seed);

            List<LevelAndValue> sourceAir = new List<LevelAndValue>();
            for (int i = 0; i < 100; i++)
            {
                sourceAir.Add(new LevelAndValue() { Level = i, Value = random.NextDouble() });
            }

            random = new Random(++seed);

            List<LevelAndValue> sourceConvertLoss = new List<LevelAndValue>();
            for (int i = 0; i < 100; i++)
            {
                sourceConvertLoss.Add(new LevelAndValue() { Level = i, Value = random.NextDouble() });
            }

            random = new Random(++seed);

            List<LevelAndValue> sourceRegeneLoss = new List<LevelAndValue>();
            for (int i = 0; i < 100; i++)
            {
                sourceRegeneLoss.Add(new LevelAndValue() { Level = i, Value = random.NextDouble() });
            }

            PlotModel plotModel = new PlotModel();

            CategoryAxis axisX = new CategoryAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.ItemsSource = sourceRolling;
            axisX.GapWidth = 0;
            axisX.MajorStep = 10;
            axisX.LabelField = "Level";
            axisX.Title = "distance [m]";

            LinearAxis axisY = new LinearAxis();
            axisY.Title = "Number";

            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            ColumnSeries seriesRolling = new ColumnSeries();
            seriesRolling.ItemsSource = sourceRolling;
            seriesRolling.ValueField = "Value";
            seriesRolling.FillColor = OxyColors.Orange;
            seriesRolling.IsStacked = true;
            plotModel.Series.Add(seriesRolling);

            ColumnSeries seriesRegeneLoss = new ColumnSeries();
            seriesRegeneLoss.ItemsSource = sourceRegeneLoss;
            seriesRegeneLoss.ValueField = "Value";
            seriesRegeneLoss.FillColor = OxyColors.DeepPink;
            seriesRegeneLoss.IsStacked = true;
            plotModel.Series.Add(seriesRegeneLoss);

            ColumnSeries seriesAir = new ColumnSeries();
            seriesAir.ItemsSource = sourceAir;
            seriesAir.ValueField = "Value";
            seriesAir.FillColor = OxyColors.Yellow;
            seriesAir.IsStacked = true;
            plotModel.Series.Add(seriesAir);

            ColumnSeries seriesConvertLoss = new ColumnSeries();
            seriesConvertLoss.ItemsSource = sourceConvertLoss;
            seriesConvertLoss.ValueField = "Value";
            seriesConvertLoss.FillColor = OxyColors.Red;
            seriesConvertLoss.IsStacked = true;
            plotModel.Series.Add(seriesConvertLoss);

            return plotModel;
        }
    }
}
