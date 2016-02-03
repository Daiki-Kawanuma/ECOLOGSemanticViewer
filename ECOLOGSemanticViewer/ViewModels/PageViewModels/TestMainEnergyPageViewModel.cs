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
using System.Collections.ObjectModel;
using ECOLOGSemanticViewer.Models.GraphModels;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Axes;
using System.Windows;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using ECOLOGSemanticViewer.Views.Items;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class TestMainEnergyPageViewModel : AbstMainPageViewModel
    {
        public void Initialize()
        {
        }

        public TestMainEnergyPageViewModel()
        {
            List<SemanticLink> SemanticLinks = new List<SemanticLink>();
            SemanticLinks.Add(new SemanticLink() { SemanticLinkId = 195, Semantics = "阿久和～桃源台"});

            SelectedSemanticLinks = new ObservableCollection<SemanticLink>();

            this.ProgressBarVisibility = Visibility.Visible;
            this.TripDirection = new TripDirection() { Direction = "outward"};

            this.SemanticGraphs = new List<SemanticGraph>();
            foreach (SemanticLink link in SemanticLinks)
            {
                this.SemanticGraphs.Add(new SemanticGraph() { SemanticLink = link, SeriesVisibility = true});
            }

            this.AreaSeriesList = new List<AreaSeries>();    

            createPlotModel();
        }

        #region SemanticGraphs変更通知プロパティ
        private List<SemanticGraph> _SemanticGraphs;

        public List<SemanticGraph> SemanticGraphs
        {
            get
            { return _SemanticGraphs; }
            set
            {
                if (_SemanticGraphs == value)
                    return;
                _SemanticGraphs = value;
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

        #region AreaSeriesList変更通知プロパティ
        private List<AreaSeries> _AreaSeriesList;

        public List<AreaSeries> AreaSeriesList
        {
            get
            { return _AreaSeriesList; }
            set
            { 
                if (_AreaSeriesList == value)
                    return;
                _AreaSeriesList = value;
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
            // plotModel.LegendPlacement = LegendPlacement.Outside;
            // plotModel.LegendPosition = LegendPosition.TopRight;

            LinearAxis axisX = new LinearAxis();
            LinearAxis axisY = new LinearAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.Title = "Lost energy [kWh]";
            axisY.Title = "Number";
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            await Task.Run(() =>
            {
                
                plotModel.Series.Add(createAreaSeries(SemanticGraphs[0], 12));
                plotModel.Series.Add(createAreaSeries(SemanticGraphs[0], 24));
                
            });

            ProgressBarVisibility = Visibility.Collapsed;
            this.PlotModel = plotModel;
        }

        private AreaSeries createAreaSeries(SemanticGraph semanticGraph, int sensorID)
        {
            AreaSeries series = new AreaSeries();
            //series.TrackerFormatString = series.TrackerFormatString + "\n" + link.Semantics + " : {Tag}";
            series.Title = "Sensor ID: " + sensorID;

            series.MouseDown += (s, e) =>
            {
                if (e.ChangedButton == OxyMouseButton.Left)
                {
                    Console.WriteLine("SEMANTICS: " + semanticGraph.SemanticLink.Semantics);

                    if (SelectedSemanticLinks.Count > 0)
                    {
                        var dialog = new MainPageCompareDialog
                        {
                            Message = { Text = semanticGraph.SemanticLink.Semantics },
                            TripDirection = this.TripDirection,
                            SelectedSemanticLinks = this.SelectedSemanticLinks.ToList(),
                            SemanticLink = semanticGraph.SemanticLink,
                            ViewModel = this
                        };

                        DialogHost.Show(dialog, "RootDialog");
                    }
                    else
                    {
                        var dialog = new MainPageShowDetailDialog
                        {
                            Message = { Text = semanticGraph.SemanticLink.Semantics },
                            TripDirection = this.TripDirection,
                            SemanticLink = semanticGraph.SemanticLink,
                            ViewModel = this
                        };

                        DialogHost.Show(dialog, "RootDialog");
                    }  
                }
            };

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            // SemanticHistogramDatum datum = SemanticHistogramDatum.GetEnergyInstance(semanticGraph.SemanticLink, this.TripDirection, sensorID);
            SemanticHistogramDatum datum = null;

            sw.Stop();
            Console.WriteLine("COST: " + sw.Elapsed);

            series.Points.Add(new DataPoint(datum.MinLevel - datum.ClassWidth, 0));
            
            foreach (LevelAndValue item in datum.HistogramData)
            {
                series.Points.Add(new DataPoint(item.Level, item.Value));
            }

            series.Points.Add(new DataPoint(datum.MaxLevel + datum.ClassWidth, 0));

            AreaSeriesList.Add(series);
            semanticGraph.Series = series;

            return series;
        }
    }
}
