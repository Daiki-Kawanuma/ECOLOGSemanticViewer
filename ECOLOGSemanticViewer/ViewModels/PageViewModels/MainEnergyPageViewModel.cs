﻿using System;
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
using System.Windows.Data;
using ECOLOGSemanticViewer.Views.Items;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;

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

        public MainEnergyPageViewModel(List<SemanticLink> extractedSemanticLinks, TripDirection direction)
        {
            SelectedSemanticLinks = new ObservableCollection<SemanticLink>();

            this.ProgressBarVisibility = Visibility.Visible;
            this.TripDirection = direction;

            this.SemanticGraphs = new List<SemanticGraph>();
            foreach (SemanticLink link in extractedSemanticLinks)
            {
                this.SemanticGraphs.Add(new SemanticGraph() { SemanticLink = link, SeriesVisibility = true });
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

        #region EnergyHistogramData変更通知プロパティ
        private List<SemanticHistogramDatum> _EnergyHistogramData;

        public List<SemanticHistogramDatum> EnergyHistogramData
        {
            get
            { return _EnergyHistogramData; }
            set
            { 
                if (_EnergyHistogramData == value)
                    return;
                _EnergyHistogramData = value;
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
            this.EnergyHistogramData = new List<SemanticHistogramDatum>();

            await Task.Run(() =>
            {
                foreach (SemanticGraph graph in this.SemanticGraphs)
                {
                    this.EnergyHistogramData.Add(SemanticHistogramDatum.GetEnergyInstance(graph.SemanticLink, this.TripDirection) );
                }
            });

            CreateNumberModel();
        }

        public void CreateNumberModel()
        {
            PlotModel plotModel = new PlotModel();

            LinearAxis axisX = new LinearAxis();
            LinearAxis axisY = new LinearAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.Title = "Lost energy [kWh]";
            axisY.Title = "Number";
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            foreach (SemanticGraph link in SemanticGraphs)
            {
                plotModel.Series.Add(createNumberSeries(link));
            }

            ProgressBarVisibility = Visibility.Collapsed;
            this.PlotModel = plotModel;
        }

        private AreaSeries createNumberSeries(SemanticGraph semanticGraph)
        {
            AreaSeries series = new AreaSeries();
            series.Title = semanticGraph.SemanticLink.Semantics;

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

            SemanticHistogramDatum datum = this.EnergyHistogramData
                .Where(v => v.SemanticLink.SemanticLinkId == semanticGraph.SemanticLink.SemanticLinkId)
                .ElementAt(0);

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

        public void CreatePercentModel()
        {
            PlotModel plotModel = new PlotModel();

            LinearAxis axisX = new LinearAxis();
            LinearAxis axisY = new LinearAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.Title = "Lost energy [kWh]";
            axisY.Title = "Percent [%]";
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            foreach (SemanticGraph link in SemanticGraphs)
            {
                plotModel.Series.Add(createPercentSeries(link));
            }

            ProgressBarVisibility = Visibility.Collapsed;
            this.PlotModel = plotModel;
        }

        private AreaSeries createPercentSeries(SemanticGraph semanticGraph)
        {
            AreaSeries series = new AreaSeries();
            series.Title = semanticGraph.SemanticLink.Semantics;

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

            SemanticHistogramDatum datum = this.EnergyHistogramData
                .Where(v => v.SemanticLink.SemanticLinkId == semanticGraph.SemanticLink.SemanticLinkId)
                .ElementAt(0);

            series.Points.Add(new DataPoint(datum.MinLevel - datum.ClassWidth, 0));

            foreach (LevelAndValue item in datum.HistogramData)
            {
                series.Points.Add(new DataPoint(item.Level, item.Value * 100 / datum.HistogramData.Sum(v => v.Value)));
            }

            series.Points.Add(new DataPoint(datum.MaxLevel + datum.ClassWidth, 0));

            AreaSeriesList.Add(series);
            semanticGraph.Series = series;

            return series;
        }
    }
}
