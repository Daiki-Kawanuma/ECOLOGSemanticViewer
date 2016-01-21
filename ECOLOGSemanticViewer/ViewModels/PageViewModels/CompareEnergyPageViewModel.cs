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
using ECOLOGSemanticViewer.Models.GraphModels;
using OxyPlot;
using System.Threading.Tasks;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class CompareEnergyPageViewModel : ViewModel
    {
        #region SelectedSemanticLinks変更通知プロパティ
        private List<SemanticLink> _SelectedSemanticLinks;

        public List<SemanticLink> SelectedSemanticLinks
        {
            get
            { return _SelectedSemanticLinks; }
            set
            { 
                if (_SelectedSemanticLinks == value)
                    return;
                _SelectedSemanticLinks = value;
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

        #region EnergyHistogramDatumFirst変更通知プロパティ
        private SemanticHistogramDatum _EnergyHistogramDatumFirst;

        public SemanticHistogramDatum EnergyHistogramDatumFirst
        {
            get
            { return _EnergyHistogramDatumFirst; }
            set
            {
                if (_EnergyHistogramDatumFirst == value)
                    return;
                _EnergyHistogramDatumFirst = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EnergyHistogramDatumSecond変更通知プロパティ
        private SemanticHistogramDatum _EnergyHistogramDatumSecond;

        public SemanticHistogramDatum EnergyHistogramDatumSecond
        {
            get
            { return _EnergyHistogramDatumSecond; }
            set
            { 
                if (_EnergyHistogramDatumSecond == value)
                    return;
                _EnergyHistogramDatumSecond = value;
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

        public CompareEnergyPageViewModel()
        {
        }

        public CompareEnergyPageViewModel(List<SemanticLink> semanticLinks, TripDirection direction)
        {
            this.SelectedSemanticLinks = semanticLinks;
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
            Console.WriteLine("COUNT: " + this.SelectedSemanticLinks.Count);

            await Task.Run(() =>
            {
                this.EnergyHistogramDatumFirst = SemanticHistogramDatum.GetEnergyInstance(this.SelectedSemanticLinks[0], this.TripDirection);
                this.EnergyHistogramDatumSecond = SemanticHistogramDatum.GetEnergyInstance(this.SelectedSemanticLinks[1], this.TripDirection);
            });

            setRepresentativeValue();

            createNumberModel();
        }

        private void setRepresentativeValue()
        {
            /*
            this.Min = this.EnergyHistogramDatum.MinLevel;
            this.Mode = this.EnergyHistogramDatum.ModeLevel;
            this.Median = this.EnergyHistogramDatum.MedianLevel;
            this.Max = this.EnergyHistogramDatum.MaxLevel;

            this.DistUnderMode = this.EnergyHistogramDatum.DistUnderMode;
            this.DistMode = this.EnergyHistogramDatum.DistMode;
            this.DistUpperMode = this.EnergyHistogramDatum.DistUpperMode;

            this.CompMinMax = this.EnergyHistogramDatum.CompMinMax;
            this.CompMinMode = this.EnergyHistogramDatum.CompMinMode;
            this.CompModeMax = this.EnergyHistogramDatum.CompModeMax;*/
        }

        private void createNumberModel()
        {
            PlotModel plotModel = new PlotModel();
            // plotModel.LegendPlacement = LegendPlacement.Outside;
            // plotModel.LegendPosition = LegendPosition.TopRight;

            LinearAxis axisX = new LinearAxis();
            LinearAxis axisY = new LinearAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.Title = "Lost energy [kWh]";
            axisY.Title = "Number";
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);
   
            plotModel.Series.Add(createAreaSeries(EnergyHistogramDatumFirst));
            plotModel.Series.Add(createAreaSeries(EnergyHistogramDatumSecond));
      
            ProgressBarVisibility = System.Windows.Visibility.Collapsed;
            this.PlotModel = plotModel;
        }

        private AreaSeries createAreaSeries(SemanticHistogramDatum datum)
        {
            AreaSeries series = new AreaSeries();
            //series.TrackerFormatString = series.TrackerFormatString + "\n" + link.Semantics + " : {Tag}";
            series.Title = datum.SemanticLink.Semantics;

            series.Points.Add(new DataPoint(datum.MinLevel - datum.ClassWidth, 0));

            foreach (LevelAndValue item in datum.HistogramData)
            {
                series.Points.Add(new DataPoint(item.Level, item.Value));
            }

            series.Points.Add(new DataPoint(datum.MaxLevel + datum.ClassWidth, 0));

            return series;
        }
    }
}
