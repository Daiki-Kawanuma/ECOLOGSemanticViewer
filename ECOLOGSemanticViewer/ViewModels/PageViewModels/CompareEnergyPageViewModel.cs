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
using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.Models.GraphModels;
using OxyPlot;
using System.Threading.Tasks;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Windows.Controls;
using ECOLOGSemanticViewer.Views.Items;

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

        #region GraphTypes変更通知プロパティ
        private List<CompareGraphType.GraphTypes> _GraphTypes;

        public List<CompareGraphType.GraphTypes> GraphTypes
        {
            get
            { return _GraphTypes; }
            set
            {
                if (_GraphTypes == value)
                    return;
                _GraphTypes = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CurrentGraphType変更通知プロパティ
        private CompareGraphType.GraphTypes _CurrentGraphType;

        public CompareGraphType.GraphTypes CurrentGraphType
        {
            get
            { return _CurrentGraphType; }
            set
            {
                if (_CurrentGraphType == value)
                    return;
                _CurrentGraphType = value;

                DisplayGraph();

                RaisePropertyChanged();
            }
        }
        #endregion

        #region CurrentUserControl変更通知プロパティ
        private UserControl _CurrentUserControl;

        public UserControl CurrentUserControl
        {
            get
            { return _CurrentUserControl; }
            set
            {
                if (_CurrentUserControl == value)
                    return;
                _CurrentUserControl = value;
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
            this.GraphTypes = CompareGraphType.GetAllGraphTypes();

            DisplayGraph();
        }

        public void DisplayGraph()
        {
            this.ProgressBarVisibility = System.Windows.Visibility.Visible;
            this.PlotModel = null;

            switch (CurrentGraphType)
            {
                case CompareGraphType.GraphTypes.HistogramGraph:
                    displayHistogramGraph();
                    break;
                case CompareGraphType.GraphTypes.DistanceNormalizedHistogram:
                    displayDistanceNormalizedHistogramGraph();
                    break;
                case CompareGraphType.GraphTypes.StackGraph:
                    displayStackGraph();
                    break;
                case CompareGraphType.GraphTypes.NormalizedStackGraph:
                    displayNormalizedStackGraph();
                    break;
            }
        }

        private async void displayHistogramGraph()
        {
            await Task.Run(() =>
            {
                setHistogramData();
            });

            CurrentUserControl = new CompareNumberItem()
            {
                MinSemanticFirst = this.EnergyHistogramDatumFirst.MinLevel,
                MinSemanticSecond = this.EnergyHistogramDatumSecond.MinLevel,
                ModeSemanticFirst = this.EnergyHistogramDatumFirst.ModeLevel,
                ModeSemanticSecond = this.EnergyHistogramDatumSecond.ModeLevel,
                MaxSemanticFirst = this.EnergyHistogramDatumFirst.MaxLevel,
                MaxSemanticSecond = this.EnergyHistogramDatumSecond.MaxLevel,
                AverageSemanticFirst = this.EnergyHistogramDatumFirst.AvgLevel,
                AverageSemanticSecond = this.EnergyHistogramDatumSecond.AvgLevel
            };


            CreatePlotModel();

            this.ProgressBarVisibility = System.Windows.Visibility.Collapsed;
        }

        private async void displayDistanceNormalizedHistogramGraph()
        {
            CurrentUserControl = new CompareNumberItem();

            await Task.Run(() =>
            {
                setDistanceNormalizedHistogramData();
            });

            CurrentUserControl = new CompareNumberItem()
            {
                MinSemanticFirst = this.EnergyHistogramDatumFirst.MinLevel,
                MinSemanticSecond = this.EnergyHistogramDatumSecond.MinLevel,
                ModeSemanticFirst = this.EnergyHistogramDatumFirst.ModeLevel,
                ModeSemanticSecond = this.EnergyHistogramDatumSecond.ModeLevel,
                MaxSemanticFirst = this.EnergyHistogramDatumFirst.MaxLevel,
                MaxSemanticSecond = this.EnergyHistogramDatumSecond.MaxLevel,
                AverageSemanticFirst = this.EnergyHistogramDatumFirst.AvgLevel,
                AverageSemanticSecond = this.EnergyHistogramDatumSecond.AvgLevel
            };

            CreatePlotModel();

            this.ProgressBarVisibility = System.Windows.Visibility.Collapsed;
        }

        private async void displayStackGraph()
        {
            CurrentUserControl = new CompareStackItem();

            await Task.Run(() =>
            {
                setStackData();
            });

            CurrentUserControl = new CompareStackItem()
            {
                TotalNumberSemanticFirst = this.EnergyHistogramDatumFirst.Number,
                TotalNumberSemanticSecond = this.EnergyHistogramDatumSecond.Number,
                TotalLostEnergySemanticFirst = this.EnergyHistogramDatumFirst.SumLostEnergy,
                TotalLostEnergySemanticSecond = this.EnergyHistogramDatumSecond.SumLostEnergy,
                NumberDiff = Math.Abs(this.EnergyHistogramDatumFirst.Number - this.EnergyHistogramDatumSecond.Number),
                LostEnergyDiff = Math.Abs(this.EnergyHistogramDatumFirst.SumLostEnergy - this.EnergyHistogramDatumSecond.SumLostEnergy),
                LostEnergyDiffPercent = Math.Abs(this.EnergyHistogramDatumFirst.SumLostEnergy * 100 / this.EnergyHistogramDatumSecond.SumLostEnergy)
            };

            CreatePlotModel();

            this.ProgressBarVisibility = System.Windows.Visibility.Collapsed;
        }

        private async void displayNormalizedStackGraph()
        {
            CurrentUserControl = new CompareStackNormalizedItem();

            await Task.Run(() =>
            {
                setNormalizedStackData();
            });

            CurrentUserControl = new CompareStackNormalizedItem()
            {
                NormalizedLostEnergySemanticFirst = this.EnergyHistogramDatumFirst.SumLostEnergy / this.EnergyHistogramDatumFirst.Number,
                NormalizedLostEnergySemanticSecond = this.EnergyHistogramDatumSecond.SumLostEnergy / this.EnergyHistogramDatumSecond.Number,
                CalculatedLostEnergySemanticFirst = this.EnergyHistogramDatumFirst.SumLostEnergy / this.EnergyHistogramDatumFirst.Number * CompareStackNormalizedItem.DefaultCalculateTripNumber,
                CalculatedLostEnergySemanticSecond = this.EnergyHistogramDatumSecond.SumLostEnergy / this.EnergyHistogramDatumSecond.Number * CompareStackNormalizedItem.DefaultCalculateTripNumber,
                NormalizedLostEnergyDiff = Math.Abs((this.EnergyHistogramDatumFirst.SumLostEnergy / this.EnergyHistogramDatumFirst.Number) - (this.EnergyHistogramDatumSecond.SumLostEnergy / this.EnergyHistogramDatumSecond.Number)),
                NormalizedLostEnergyDiffPercent = Math.Abs((this.EnergyHistogramDatumFirst.SumLostEnergy / this.EnergyHistogramDatumFirst.Number) * 100 / (this.EnergyHistogramDatumSecond.SumLostEnergy / this.EnergyHistogramDatumSecond.Number)),
                CalculatedLostEnergyDiff = Math.Abs((this.EnergyHistogramDatumFirst.SumLostEnergy / this.EnergyHistogramDatumFirst.Number * CompareStackNormalizedItem.DefaultCalculateTripNumber)
                    - (this.EnergyHistogramDatumSecond.SumLostEnergy / this.EnergyHistogramDatumSecond.Number * CompareStackNormalizedItem.DefaultCalculateTripNumber)),
                CalculatedLostEnergyDiffPercent = (this.EnergyHistogramDatumFirst.SumLostEnergy / this.EnergyHistogramDatumFirst.Number * CompareStackNormalizedItem.DefaultCalculateTripNumber) * 100
                    / (this.EnergyHistogramDatumSecond.SumLostEnergy / this.EnergyHistogramDatumSecond.Number * CompareStackNormalizedItem.DefaultCalculateTripNumber)
            };

            CreatePlotModel();

            this.ProgressBarVisibility = System.Windows.Visibility.Collapsed;
        }

        private void setHistogramData()
        {
            this.EnergyHistogramDatumFirst = SemanticHistogramDatum.GetEnergyInstance(this.SelectedSemanticLinks[0], this.TripDirection);
            this.EnergyHistogramDatumSecond = SemanticHistogramDatum.GetEnergyInstance(this.SelectedSemanticLinks[1], this.TripDirection);
        }

        private void setDistanceNormalizedHistogramData()
        {
            this.EnergyHistogramDatumFirst = SemanticHistogramDatum.GetDistanceNormalizedEnergyInstance(this.SelectedSemanticLinks[0], this.TripDirection);
            this.EnergyHistogramDatumSecond = SemanticHistogramDatum.GetDistanceNormalizedEnergyInstance(this.SelectedSemanticLinks[1], this.TripDirection);
        }

        private void setStackData()
        {

            this.EnergyHistogramDatumFirst = SemanticHistogramDatum.GetStackedEnergyInstance(this.SelectedSemanticLinks[0], this.TripDirection);
            this.EnergyHistogramDatumSecond = SemanticHistogramDatum.GetStackedEnergyInstance(this.SelectedSemanticLinks[1], this.TripDirection);
        }

        private void setNormalizedStackData()
        {

            this.EnergyHistogramDatumFirst = SemanticHistogramDatum.GetNormalizedStackedEnergyInstance(this.SelectedSemanticLinks[0], this.TripDirection);
            this.EnergyHistogramDatumSecond = SemanticHistogramDatum.GetNormalizedStackedEnergyInstance(this.SelectedSemanticLinks[1], this.TripDirection);
        }

        #region 各グラフ共通のPlotModel作成メソッド
        public void CreatePlotModel()
        {
            PlotModel plotModel = new PlotModel();

            LinearAxis axisX = new LinearAxis();
            LinearAxis axisY = new LinearAxis();

            switch (CurrentGraphType)
            {
                case CompareGraphType.GraphTypes.HistogramGraph:
                    axisX.Title = "Lost energy [kWh]";
                    axisY.Title = "Number";
                    break;
                case CompareGraphType.GraphTypes.DistanceNormalizedHistogram:
                    axisX.Title = "Normalized lost energy [kWh/km]";
                    axisY.Title = "Number";
                    break;
                case CompareGraphType.GraphTypes.StackGraph:
                    axisX.Title = "Lost energy [kWh]";
                    axisY.Title = "Stacked lost energy [kWh]";
                    break;
                case CompareGraphType.GraphTypes.NormalizedStackGraph:
                    axisX.Title = "Lost energy [kWh]";
                    axisY.Title = "Normalized stacked lost energy [kWh/number]";
                    break;
            }

            axisX.Position = AxisPosition.Bottom;
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            plotModel.Series.Add(createAreaSeries(EnergyHistogramDatumFirst));
            plotModel.Series.Add(createAreaSeries(EnergyHistogramDatumSecond));

            ProgressBarVisibility = System.Windows.Visibility.Collapsed;
            this.PlotModel = plotModel;
        }
        #endregion

        #region 各グラフ共通のSeries作成メソッド
        private AreaSeries createAreaSeries(SemanticHistogramDatum datum)
        {
            AreaSeries series = new AreaSeries();

            if (datum == this.EnergyHistogramDatumFirst)
                series.Title = "A：" + datum.SemanticLink.Semantics;
            else
                series.Title = "B：" + datum.SemanticLink.Semantics;

            series.Points.Add(new DataPoint(datum.MinLevel - datum.ClassWidth, 0));

            foreach (LevelAndValue item in datum.HistogramData)
            {
                series.Points.Add(new DataPoint(item.Level, item.Value));
            }

            series.Points.Add(new DataPoint(datum.MaxLevel + datum.ClassWidth, 0));

            return series;
        }
        #endregion

        #region 各グラフ共通のパーセントPlotModel作成メソッド
        public void CreatePercentilePlotModel()
        {
            PlotModel plotModel = new PlotModel();

            LinearAxis axisX = new LinearAxis();
            LinearAxis axisY = new LinearAxis();

            switch (CurrentGraphType)
            {
                case CompareGraphType.GraphTypes.HistogramGraph:
                    axisX.Title = "Lost energy [kWh]";
                    axisY.Title = "Probability [%]";
                    break;
                case CompareGraphType.GraphTypes.DistanceNormalizedHistogram:
                    axisX.Title = "Normalized lost energy [kWh/km]";
                    axisY.Title = "Probability [%]";
                    break;
                case CompareGraphType.GraphTypes.StackGraph:
                    
                    break;
                case CompareGraphType.GraphTypes.NormalizedStackGraph:
                    
                    break;
            }

            axisX.Position = AxisPosition.Bottom;
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            plotModel.Series.Add(createPercentileAreaSeries(EnergyHistogramDatumFirst));
            plotModel.Series.Add(createPercentileAreaSeries(EnergyHistogramDatumSecond));

            ProgressBarVisibility = System.Windows.Visibility.Collapsed;
            this.PlotModel = plotModel;
        }
        #endregion

        #region 各グラフ共通のパーセントSerie作成メソッド
        private AreaSeries createPercentileAreaSeries(SemanticHistogramDatum datum)
        {
            AreaSeries series = new AreaSeries();

            if (datum == this.EnergyHistogramDatumFirst)
                series.Title = "A：" + datum.SemanticLink.Semantics;
            else
                series.Title = "B：" + datum.SemanticLink.Semantics;

            double sumValue = datum.HistogramData.Sum(v => v.Value);

            series.Points.Add(new DataPoint(datum.MinLevel - datum.ClassWidth, 0));

            foreach (LevelAndValue item in datum.HistogramData)
            {
                series.Points.Add(new DataPoint(item.Level, item.Value * 100 / sumValue));
            }

            series.Points.Add(new DataPoint(datum.MaxLevel + datum.ClassWidth, 0));

            return series;
        }
        #endregion

        public void CreateCalculatedPlotModel(int numberOfTrip)
        {
            PlotModel plotModel = new PlotModel();

            LinearAxis axisX = new LinearAxis();
            LinearAxis axisY = new LinearAxis();

            switch (CurrentGraphType)
            {
                case CompareGraphType.GraphTypes.HistogramGraph:
                    
                    break;
                case CompareGraphType.GraphTypes.DistanceNormalizedHistogram:
                    
                    break;
                case CompareGraphType.GraphTypes.StackGraph:
                    
                    break;
                case CompareGraphType.GraphTypes.NormalizedStackGraph:
                    axisX.Title = "Lost energy [kWh]";
                    axisY.Title = "Calculated stacked lost energy [kWh]";
                    break;
            }

            axisX.Position = AxisPosition.Bottom;
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            plotModel.Series.Add(createCalculatedAreaSeries(EnergyHistogramDatumFirst, numberOfTrip));
            plotModel.Series.Add(createCalculatedAreaSeries(EnergyHistogramDatumSecond, numberOfTrip));

            ProgressBarVisibility = System.Windows.Visibility.Collapsed;
            this.PlotModel = plotModel;
        }

        private AreaSeries createCalculatedAreaSeries(SemanticHistogramDatum datum, int numberOfTrip)
        {
            AreaSeries series = new AreaSeries();

            if (datum == this.EnergyHistogramDatumFirst)
                series.Title = "A：" + datum.SemanticLink.Semantics;
            else
                series.Title = "B：" + datum.SemanticLink.Semantics;

            double sumValue = datum.HistogramData.Sum(v => v.Value);

            series.Points.Add(new DataPoint(datum.MinLevel - datum.ClassWidth, 0));

            foreach (LevelAndValue item in datum.HistogramData)
            {
                series.Points.Add(new DataPoint(item.Level, item.Value * numberOfTrip));
            }

            series.Points.Add(new DataPoint(datum.MaxLevel + datum.ClassWidth, 0));

            return series;
        }
    }
}
