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

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class DetailTimePageViewModel : ViewModel
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

        public DetailTimePageViewModel()
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
                 
                new LevelAndValue(){Level = 120, Value = 1},
                new LevelAndValue(){Level = 143, Value = 15},
                new LevelAndValue(){Level = 167, Value = 49},
                new LevelAndValue(){Level = 190, Value = 87},
                new LevelAndValue(){Level = 214, Value = 107},
                new LevelAndValue(){Level = 237, Value = 105},
                new LevelAndValue(){Level = 261, Value = 81},
                new LevelAndValue(){Level = 284, Value = 57},
                new LevelAndValue(){Level = 308, Value = 34},
                new LevelAndValue(){Level = 331, Value = 31},
                new LevelAndValue(){Level = 354, Value = 15},
                new LevelAndValue(){Level = 378, Value = 18},
            };

            PlotModel plotModel = new PlotModel();

            CategoryAxis axisX = new CategoryAxis();
            axisX.Position = AxisPosition.Bottom;
            axisX.ItemsSource = source;
            axisX.GapWidth = 0.25;
            axisX.LabelField = "Level";
            axisX.Title = "Time [s]";

            LinearAxis axisY = new LinearAxis();
            axisY.Title = "Number";

            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            RectangleAnnotation rectAannotation = new RectangleAnnotation();
            rectAannotation.Fill = OxyColor.FromArgb(100, OxyColors.Orange.R, OxyColors.Orange.G, OxyColors.Orange.B);
            rectAannotation.MinimumY = axisY.Minimum;
            rectAannotation.MaximumY = axisY.Maximum;
            rectAannotation.MinimumX = 2.5;
            rectAannotation.MaximumX = 6.5;
            rectAannotation.Layer = AnnotationLayer.BelowSeries;

            plotModel.Annotations.Add(rectAannotation);

            var textAnnotation = new TextAnnotation();
            textAnnotation.TextPosition = new DataPoint(4.5, 75);
            textAnnotation.Text = "67%";
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
