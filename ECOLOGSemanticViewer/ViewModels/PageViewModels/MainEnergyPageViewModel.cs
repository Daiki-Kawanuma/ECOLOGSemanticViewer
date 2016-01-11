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

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class MainEnergyPageViewModel : AbstMainPageViewModel
    {

        public void Initialize()
        {
        }

        public MainEnergyPageViewModel()
        {
            PlotModel = createPlotModel();
        }

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

        private PlotModel createPlotModel()
        {

            PlotModel plotModel = new PlotModel();

            LinearAxis axisX = new LinearAxis();
            LinearAxis axisY = new LinearAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.Title = "Lost energy [kWh]";
            axisY.Title = "Number";
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            foreach(SemanticLink link in SelectedSemanticLinks){
                plotModel.Series.Add(createAreaSeries(link));
            }

            return plotModel;
        }

        private AreaSeries createAreaSeries(SemanticLink link)
        {
            AreaSeries series = new AreaSeries();
            // TODO 意味確認
            series.TrackerFormatString = series.TrackerFormatString + "\n" + link.Semantics + " : {Tag}";
            series.Title = link.Semantics;

            

                    series.Title = "与蔵山下～代官二丁目";
                    series.Color = OxyColors.GreenYellow;

                    series.Points.Add(new DataPoint(0.164779303, 0));
                    series.Points.Add(new DataPoint(0.169821257, 6));
                    series.Points.Add(new DataPoint(0.174863211, 6));
                    series.Points.Add(new DataPoint(0.179905164, 25));
                    series.Points.Add(new DataPoint(0.184947118, 31));
                    series.Points.Add(new DataPoint(0.189989071, 78));
                    series.Points.Add(new DataPoint(0.195031025, 124));
                    series.Points.Add(new DataPoint(0.200072979, 114));
                    series.Points.Add(new DataPoint(0.205114932, 96));
                    series.Points.Add(new DataPoint(0.210156886, 74));
                    series.Points.Add(new DataPoint(0.21519884, 27));
                    series.Points.Add(new DataPoint(0.220240793, 21));
                    series.Points.Add(new DataPoint(0.225282747, 0));
              

            



            return series;
        }
    }
}
