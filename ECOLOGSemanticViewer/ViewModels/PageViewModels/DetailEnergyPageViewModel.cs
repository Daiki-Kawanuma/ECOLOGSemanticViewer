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
using OxyPlot.Annotations;
using System.Windows.Data;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class DetailEnergyPageViewModel : ViewModel
    {

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

        public DetailEnergyPageViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            this.PlotModel = createPlotModel();
        }

        private PlotModel createPlotModel()
        {
            List<LevelAndValue> source = new List<LevelAndValue>() { 
                new LevelAndValue(){Level = 0.146050845, Value = 6},
                new LevelAndValue(){Level = 0.15101523, Value = 8},
                new LevelAndValue(){Level = 0.155979616, Value = 20},
                new LevelAndValue(){Level = 0.160944001, Value = 56},
                new LevelAndValue(){Level = 0.165908386, Value = 67},
                new LevelAndValue(){Level = 0.170872772, Value = 103},
                new LevelAndValue(){Level = 0.175837157, Value = 125},
                new LevelAndValue(){Level = 0.180801542, Value = 99},
                new LevelAndValue(){Level = 0.185765928, Value = 69},
                new LevelAndValue(){Level = 0.190730313, Value = 24},
                new LevelAndValue(){Level = 0.195694698, Value = 17},
            };

            PlotModel plotModel = new PlotModel();

            CategoryAxis axisX = new CategoryAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.ItemsSource = source;
            axisX.GapWidth = 0.25;
            axisX.LabelField = "Level";
            axisX.StringFormat = "0.000";
            axisX.Title = "Lost energy [kWh]";

            LinearAxis axisY = new LinearAxis();
            axisY.Title = "Number";

            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            RectangleAnnotation rectAannotation = new RectangleAnnotation();
            rectAannotation.Fill = OxyColor.FromArgb(100, OxyColors.Orange.R, OxyColors.Orange.G, OxyColors.Orange.B);
            rectAannotation.MinimumY = axisY.Minimum;
            rectAannotation.MaximumY = axisY.Maximum;
            rectAannotation.MinimumX = 4.5;
            rectAannotation.MaximumX = 7.5;
            rectAannotation.Layer = AnnotationLayer.BelowSeries;

            plotModel.Annotations.Add(rectAannotation);

            var textAnnotation = new TextAnnotation();
            textAnnotation.TextPosition = new DataPoint(6, 60);
            textAnnotation.Text = "63%";
            textAnnotation.TextColor = OxyColors.Orange;
            textAnnotation.FontSize = 50;
            textAnnotation.FontWeight = FontWeights.Bold;
            textAnnotation.Stroke = OxyColors.Transparent;

            plotModel.Annotations.Add(textAnnotation);

            ColumnSeries series = new ColumnSeries();
            series.ItemsSource = source;
            series.ValueField = "Value";
            series.FillColor = OxyColors.SkyBlue;

            plotModel.Series.Add(series);

            return plotModel;
        }

        
    }
}
