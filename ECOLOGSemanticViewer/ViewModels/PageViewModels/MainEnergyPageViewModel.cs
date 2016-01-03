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

        private PlotModel createPlotModel(){
            
            PlotModel plotModel = new PlotModel();

            LinearAxis axisX = new LinearAxis();
            LinearAxis axisY = new LinearAxis();
            axisX.Position = AxisPosition.Bottom;
            //axisY.Position = AxisPosition.Left;
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            for (int i = 1; i < 6; i++)
            {
                plotModel.Series.Add(createAreaSeries(i));
                Thread.Sleep(100);
            }

            

            return plotModel;
        }

        private AreaSeries createAreaSeries(int i)
        {
            AreaSeries series = new AreaSeries();
            series.Title = "Semantic";
            Random random = new Random();
            Random randomR = new Random((int)DateTime.Now.Ticks);
            Random randomG = new Random((int)DateTime.Now.Ticks + random.Next() * 10);
            Random randomB = new Random((int)DateTime.Now.Ticks * random.Next());
            series.Color = OxyColor.FromArgb((byte)random.Next(0, 255), (byte)randomR.Next(0, 255), (byte)randomG.Next(0, 255), (byte)randomB.Next(0, 255));
            series.TrackerFormatString = series.TrackerFormatString + "\nSemantic : {Tag}";

            int range = random.Next();

            series.Points.Add(new DataPoint(0 * i, 0));
            series.Points.Add(new DataPoint(1 * i, 2));
            series.Points.Add(new DataPoint(2 * i, 3));
            series.Points.Add(new DataPoint(3 * i, 5));
            series.Points.Add(new DataPoint(4 * i, 5));
            series.Points.Add(new DataPoint(5 * i, 10));
            series.Points.Add(new DataPoint(6 * i, 20));
            series.Points.Add(new DataPoint(7 * i, 30));
            series.Points.Add(new DataPoint(8 * i, 20));
            series.Points.Add(new DataPoint(9 * i, 10));
            series.Points.Add(new DataPoint(10 * i, 3));
            series.Points.Add(new DataPoint(11 * i, 0));
      
            return series;
        }
    }
}
