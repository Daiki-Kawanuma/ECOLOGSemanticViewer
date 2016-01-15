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
using System.Threading;
using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.Models.GraphModels;
using System.Threading.Tasks;
using System.Windows;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class MainEnergyPageViewModel : AbstMainPageViewModel
    {

        public void Initialize()
        {
        }

        public MainEnergyPageViewModel()
        {

        }

        public MainEnergyPageViewModel(List<SemanticLink> extractedSemanticLinks)
        {
            this.ProgressBarVisibility = Visibility.Visible;
            this.ExtractedSemanticLinks = extractedSemanticLinks;
            this.SelectedSemanticLinks = new List<SemanticLink>();
            createPlotModel();
        }

        #region ExtractedSemanticLinks変更通知プロパティ
        private List<SemanticLink> _ExtractedSemanticLinks;

        public List<SemanticLink> ExtractedSemanticLinks
        {
            get
            { return _ExtractedSemanticLinks; }
            set
            {
                if (_ExtractedSemanticLinks == value)
                    return;
                _ExtractedSemanticLinks = value;
                RaisePropertyChanged();
            }
        }
        #endregion

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

        private async void createPlotModel()
        {
            PlotModel plotModel = new PlotModel();

            LinearAxis axisX = new LinearAxis();
            LinearAxis axisY = new LinearAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.Title = "Lost energy [kWh]";
            axisY.Title = "Number";
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            await Task.Run(() =>
            {
                foreach (SemanticLink link in ExtractedSemanticLinks)
                {
                    plotModel.Series.Add(createAreaSeries(link));
                }
            });

            ProgressBarVisibility = Visibility.Collapsed;
            this.PlotModel = plotModel;
        }

        private AreaSeries createAreaSeries(SemanticLink link)
        {
            AreaSeries series = new AreaSeries();
            // TODO 意味確認
            series.TrackerFormatString = series.TrackerFormatString + "\n" + link.Semantics + " : {Tag}";
            series.Title = link.Semantics;

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            SemanticHistogramDatum datum = SemanticHistogramDatum.GetEnergyInstance(link, new TripDirection() { Direction = "outward" });

            sw.Stop();
            Console.WriteLine("COST: " + sw.Elapsed);

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
