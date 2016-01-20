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
using OxyPlot.Annotations;
using OxyPlot.Series;
using ECOLOGSemanticViewer.Models.GraphModels;
using System.Threading.Tasks;
using ECOLOGSemanticViewer.Models.EcologModels;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class DetailTimePageViewModel : ViewModel
    {
        
        #region SemanticLink変更通知プロパティ
        private SemanticLink _SemanticLink;

        public SemanticLink SemanticLink
        {
            get
            { return _SemanticLink; }
            set
            { 
                if (_SemanticLink == value)
                    return;
                _SemanticLink = value;
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

        #region EnergyHistogramDatum変更通知プロパティ
        private SemanticHistogramDatum _TimeHistogramDatum;

        public SemanticHistogramDatum TimeHistogramDatum
        {
            get
            { return _TimeHistogramDatum; }
            set
            { 
                if (_TimeHistogramDatum == value)
                    return;
                _TimeHistogramDatum = value;
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
      
        #region Min変更通知プロパティ
        private double _Min;

        public double Min
        {
            get
            { return _Min; }
            set
            { 
                if (_Min == value)
                    return;
                _Min = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Mode変更通知プロパティ
        private double _Mode;

        public double Mode
        {
            get
            { return _Mode; }
            set
            { 
                if (_Mode == value)
                    return;
                _Mode = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Median変更通知プロパティ
        private double _Median;

        public double Median
        {
            get
            { return _Median; }
            set
            { 
                if (_Median == value)
                    return;
                _Median = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Max変更通知プロパティ
        private double _Max;

        public double Max
        {
            get
            { return _Max; }
            set
            { 
                if (_Max == value)
                    return;
                _Max = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region DistUnderMode変更通知プロパティ
        private double _DistUnderMode;

        public double DistUnderMode
        {
            get
            { return _DistUnderMode; }
            set
            { 
                if (_DistUnderMode == value)
                    return;
                _DistUnderMode = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region DistMode変更通知プロパティ
        private double _DistMode;

        public double DistMode
        {
            get
            { return _DistMode; }
            set
            { 
                if (_DistMode == value)
                    return;
                _DistMode = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region DistUpperMode変更通知プロパティ
        private double _DistUpperMode;

        public double DistUpperMode
        {
            get
            { return _DistUpperMode; }
            set
            { 
                if (_DistUpperMode == value)
                    return;
                _DistUpperMode = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CompMinMax変更通知プロパティ
        private double _CompMinMax;

        public double CompMinMax
        {
            get
            { return _CompMinMax; }
            set
            { 
                if (_CompMinMax == value)
                    return;
                _CompMinMax = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CompMinMode変更通知プロパティ
        private double _CompMinMode;

        public double CompMinMode
        {
            get
            { return _CompMinMode; }
            set
            { 
                if (_CompMinMode == value)
                    return;
                _CompMinMode = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CompModeMax変更通知プロパティ
        private double _CompModeMax;

        public double CompModeMax
        {
            get
            { return _CompModeMax; }
            set
            { 
                if (_CompModeMax == value)
                    return;
                _CompModeMax = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public DetailTimePageViewModel()
        {
        }

        public DetailTimePageViewModel(SemanticLink link, TripDirection direction)
        {
            this.SemanticLink = link;
            this.TripDirection = direction;

            Initialize();
        }

        public void Initialize()
        {
            this.ProgressBarVisibility = System.Windows.Visibility.Visible;
            createPlotModel();
        }

        private async void createPlotModel()
        {
            await Task.Run(() =>
            {
                this.TimeHistogramDatum = SemanticHistogramDatum.GetTimeInstance(this.SemanticLink, this.TripDirection);
            });

            setRepresentativeValue();

            createNumberModel();
        }

        private void setRepresentativeValue()
        {
            this.Min = this.TimeHistogramDatum.MinLevel;
            this.Mode = this.TimeHistogramDatum.ModeLevel;
            this.Median = this.TimeHistogramDatum.MedianLevel;
            this.Max = this.TimeHistogramDatum.MaxLevel;

            this.DistUnderMode = this.TimeHistogramDatum.DistUnderMode;
            this.DistMode = this.TimeHistogramDatum.DistMode;
            this.DistUpperMode = this.TimeHistogramDatum.DistUpperMode;

            this.CompMinMax = this.TimeHistogramDatum.CompMinMax;
            this.CompMinMode = this.TimeHistogramDatum.CompMinMode;
            this.CompModeMax = this.TimeHistogramDatum.CompModeMax;
        }

        public void createNumberModel()
        {
            PlotModel plotModel = new PlotModel();

            CategoryAxis axisX = new CategoryAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.ItemsSource = this.TimeHistogramDatum.HistogramData;
            axisX.GapWidth = 0.25;
            axisX.LabelField = "Level";
            axisX.StringFormat = "0";
            axisX.Title = "Time [s]";

            LinearAxis axisY = new LinearAxis();
            axisY.Title = "Number";

            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            ColumnSeries series = new ColumnSeries();
            series.ItemsSource = TimeHistogramDatum.HistogramData;
            series.ValueField = "Value";
            series.FillColor = OxyColors.SkyBlue;

            plotModel.Series.Add(series);

            this.ProgressBarVisibility = System.Windows.Visibility.Collapsed;
            this.PlotModel = plotModel;
        }

        public void createPercentModel()
        {
            PlotModel plotModel = new PlotModel();

            CategoryAxis axisX = new CategoryAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.ItemsSource = this.TimeHistogramDatum.HistogramData;
            axisX.GapWidth = 0.25;
            axisX.LabelField = "Level";
            axisX.StringFormat = "0";
            axisX.Title = "Time [s]";

            LinearAxis axisY = new LinearAxis();
            axisY.Title = "Percent";

            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            List<LevelAndValue> source = new List<LevelAndValue>();
            foreach (LevelAndValue item in this.TimeHistogramDatum.HistogramData)
            {
                source.Add(new LevelAndValue() { Level = item.Level, Value = item.Value * 100 / this.TimeHistogramDatum.HistogramData.Sum(v => v.Value) });
            }

            ColumnSeries series = new ColumnSeries();
            series.ItemsSource = source;
            series.ValueField = "Value";
            series.FillColor = OxyColors.SkyBlue;

            plotModel.Series.Add(series);

            this.ProgressBarVisibility = System.Windows.Visibility.Collapsed;
            this.PlotModel = plotModel;
        }

        public void SetLevelAnnotation(double level)
        {
            this.PlotModel.Annotations.Clear();

            int index = this.TimeHistogramDatum.HistogramData
                .FindIndex(v => v.Level == Math.Ceiling(level / this.TimeHistogramDatum.ClassWidth) * this.TimeHistogramDatum.ClassWidth);

            RectangleAnnotation rectAannotation = new RectangleAnnotation();
            rectAannotation.Fill = OxyColor.FromArgb(100, OxyColors.Orange.R, OxyColors.Orange.G, OxyColors.Orange.B);
            rectAannotation.MinimumX = index - 0.5;
            rectAannotation.MaximumX = index + 0.5;
            rectAannotation.Layer = AnnotationLayer.BelowSeries;

            this.PlotModel.Annotations.Add(rectAannotation);

            // TODO アルゴリズム考えよう
            int x;
            if (index < 2)
            {
                x = 2;
            }
            else if (index > 8)
            {
                x = 8;
            }
            else
            {
                x = index;
            }

            var textAnnotation = new TextAnnotation();
            textAnnotation.TextPosition = new DataPoint(x, this.PlotModel.Axes[1].ActualMaximum / 2);
            textAnnotation.Text = String.Format("{0:f0}s", level);
            textAnnotation.TextColor = OxyColors.Orange;
            textAnnotation.FontSize = 50;
            textAnnotation.FontWeight = FontWeights.Bold;
            textAnnotation.Stroke = OxyColors.Transparent;

            this.PlotModel.Annotations.Add(textAnnotation);
            
            this.PlotModel.InvalidatePlot(true);
        }

        public void SetDistAnnotation(List<LevelAndValue> seriesList, double percent)
        {
            this.PlotModel.Annotations.Clear();

            int indexMin = this.TimeHistogramDatum.HistogramData
                .FindIndex(a => a.Level == seriesList.Min(b => b.Level));
            int indexMax = this.TimeHistogramDatum.HistogramData
                .FindIndex(a => a.Level == seriesList.Max(b => b.Level));

            RectangleAnnotation rectAannotation = new RectangleAnnotation();
            rectAannotation.Fill = OxyColor.FromArgb(100, OxyColors.Orange.R, OxyColors.Orange.G, OxyColors.Orange.B);
            rectAannotation.MinimumX = indexMin - 0.5;
            rectAannotation.MaximumX = indexMax + 0.5;
            rectAannotation.Layer = AnnotationLayer.BelowSeries;

            this.PlotModel.Annotations.Add(rectAannotation);

            var textAnnotation = new TextAnnotation();
            textAnnotation.TextPosition = new DataPoint((float)(indexMax + indexMin) / 2, this.PlotModel.Axes[1].ActualMaximum / 2);
            textAnnotation.Text = String.Format("{0:f1}%", percent);
            textAnnotation.TextColor = OxyColors.Orange;
            textAnnotation.FontSize = 50;
            textAnnotation.FontWeight = FontWeights.Bold;
            textAnnotation.Stroke = OxyColors.Transparent;

            this.PlotModel.Annotations.Add(textAnnotation);

            this.PlotModel.InvalidatePlot(true);
        }

        public void SetCompAnnotation(double low, double high, bool isAbsolute)
        {
            this.PlotModel.Annotations.Clear();

            int indexLow = this.TimeHistogramDatum.HistogramData
                .FindIndex(v => v.Level == Math.Ceiling(low / this.TimeHistogramDatum.ClassWidth) * this.TimeHistogramDatum.ClassWidth);

            int indexHigh = this.TimeHistogramDatum.HistogramData
                .FindIndex(v => v.Level == Math.Ceiling(high / this.TimeHistogramDatum.ClassWidth) * this.TimeHistogramDatum.ClassWidth);

            RectangleAnnotation rectAannotationMin = new RectangleAnnotation();
            rectAannotationMin.Fill = OxyColor.FromArgb(100, OxyColors.Orange.R, OxyColors.Orange.G, OxyColors.Orange.B);
            rectAannotationMin.MinimumX = indexLow - 0.5;
            rectAannotationMin.MaximumX = indexLow + 0.5;
            rectAannotationMin.Layer = AnnotationLayer.BelowSeries;

            this.PlotModel.Annotations.Add(rectAannotationMin);

            RectangleAnnotation rectAannotationMax = new RectangleAnnotation();
            rectAannotationMax.Fill = OxyColor.FromArgb(100, OxyColors.Orange.R, OxyColors.Orange.G, OxyColors.Orange.B);
            rectAannotationMax.MinimumX = indexHigh - 0.5;
            rectAannotationMax.MaximumX = indexHigh + 0.5;
            rectAannotationMax.Layer = AnnotationLayer.BelowSeries;

            this.PlotModel.Annotations.Add(rectAannotationMax);

            var textAnnotation = new TextAnnotation();
            textAnnotation.TextPosition = new DataPoint((float)(indexHigh + indexLow) / 2, this.PlotModel.Axes[1].ActualMaximum / 2);

            if (isAbsolute)
                textAnnotation.Text = String.Format("{0:f0}s", high - low);
            else
                textAnnotation.Text = String.Format("{0:f0}%", high * 100 / low);
                

            textAnnotation.TextColor = OxyColors.Orange;
            textAnnotation.FontSize = 50;
            textAnnotation.FontWeight = FontWeights.Bold;
            textAnnotation.Stroke = OxyColors.Transparent;

            this.PlotModel.Annotations.Add(textAnnotation);

            this.PlotModel.InvalidatePlot(true);
        }

        public void SetAbsoluteCompare()
        {

        }
    }
}
