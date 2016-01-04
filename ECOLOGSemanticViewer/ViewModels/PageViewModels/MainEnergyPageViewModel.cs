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

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class MainEnergyPageViewModel : ViewModel
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

            for (int i = 0; i < 3; i++)
            {
                plotModel.Series.Add(createAreaSeries(i));
            }



            return plotModel;
        }

        private AreaSeries createAreaSeries(int i)
        {
            AreaSeries series = new AreaSeries();
            series.TrackerFormatString = series.TrackerFormatString + "\nSemantic : {Tag}";

            switch (i)
            {
                case 0:
                    series.Title = "自宅～綾瀬市役所前";
                    series.Color = OxyColors.OrangeRed;

                    series.Points.Add(new DataPoint(0.18043131, 0));
                    series.Points.Add(new DataPoint(0.186795715, 4));
                    series.Points.Add(new DataPoint(0.19316012, 3));
                    series.Points.Add(new DataPoint(0.199524525, 9));
                    series.Points.Add(new DataPoint(0.20588893, 30));
                    series.Points.Add(new DataPoint(0.212253335, 65));
                    series.Points.Add(new DataPoint(0.218617741, 64));
                    series.Points.Add(new DataPoint(0.224982146, 51));
                    series.Points.Add(new DataPoint(0.231346551, 48));
                    series.Points.Add(new DataPoint(0.237710956, 20));
                    series.Points.Add(new DataPoint(0.244075361, 14));
                    series.Points.Add(new DataPoint(0.250439767, 9));
                    series.Points.Add(new DataPoint(0.256804172, 0));

                    break;

                case 1:
                    series.Title = "綾瀬市役所前～与蔵山下";
                    series.Color = OxyColors.SkyBlue;

                    series.Points.Add(new DataPoint(0.103455455, 0));
                    series.Points.Add(new DataPoint(0.110529318, 2));
                    series.Points.Add(new DataPoint(0.117603181, 3));
                    series.Points.Add(new DataPoint(0.124677043, 4));
                    series.Points.Add(new DataPoint(0.131750906, 55));
                    series.Points.Add(new DataPoint(0.138824769, 91));
                    series.Points.Add(new DataPoint(0.145898632, 79));
                    series.Points.Add(new DataPoint(0.152972494, 104));
                    series.Points.Add(new DataPoint(0.160046357, 82));
                    series.Points.Add(new DataPoint(0.16712022, 40));
                    series.Points.Add(new DataPoint(0.174194083, 27));
                    series.Points.Add(new DataPoint(0.181267945, 25));
                    series.Points.Add(new DataPoint(0.188341808, 9));
                    series.Points.Add(new DataPoint(0.195415671, 0));

                    break;

                case 2:
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
                        break;

            }



            return series;
        }
    }
}
