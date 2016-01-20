using ECOLOGSemanticViewer.ViewModels.PageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ECOLOGSemanticViewer.Views.Pages
{
    /// <summary>
    /// DetailTimePage.xaml の相互作用ロジック
    /// </summary>
    public partial class DetailTimePage : Page
    {
        private bool isAbsolute = false;

        public DetailTimePage()
        {
            InitializeComponent();
        }

        private void clearButtonCalar()
        {
            this.ButtonMin.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonMode.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonMedian.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonMax.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonDistUnderMode.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonDistMode.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonDistUpperMode.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonComMinMax.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonComMinMode.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonComModeMax.Foreground = SystemColors.ControlDarkDarkBrush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();

            Button button = (Button)sender;
            if(button != this.ButtonAbsolute && button != this.ButtonRelative)
            button.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailTimePageViewModel;
            if (context == null) { return; }

            if (button == this.ButtonMin)
            {
                context.SetLevelAnnotation(context.TimeHistogramDatum.MinLevel);
            }
            else if (button == this.ButtonMode)
            {
                context.SetLevelAnnotation(context.TimeHistogramDatum.ModeLevel);
            }
            else if (button == this.ButtonMedian)
            {
                context.SetLevelAnnotation(context.TimeHistogramDatum.MedianLevel);
            }
            else if (button == this.ButtonMax)
            {
                context.SetLevelAnnotation(context.TimeHistogramDatum.MaxLevel);
            }
            else if (button == this.ButtonDistUnderMode)
            {
                context.SetDistAnnotation(context.TimeHistogramDatum.UnderModeData, context.DistUnderMode);
            }
            else if (button == this.ButtonDistMode)
            {
                context.SetDistAnnotation(context.TimeHistogramDatum.ModeData, context.DistMode);
            }
            else if (button == this.ButtonDistUpperMode)
            {
                context.SetDistAnnotation(context.TimeHistogramDatum.UpperModeData, context.DistUpperMode);
            }
            else if (button == this.ButtonComMinMax)
            {
                context.SetCompAnnotation(context.TimeHistogramDatum.MinLevel, context.TimeHistogramDatum.MaxLevel, this.isAbsolute);
            }
            else if (button == this.ButtonComMinMode)
            {
                context.SetCompAnnotation(context.TimeHistogramDatum.MinLevel, context.TimeHistogramDatum.ModeLevel, this.isAbsolute);
            }
            else if (button == this.ButtonComModeMax)
            {
                context.SetCompAnnotation(context.TimeHistogramDatum.ModeLevel, context.TimeHistogramDatum.MaxLevel, this.isAbsolute);
            }
            else if (button == this.ButtonNumber)
            {
                context.createNumberModel();
            }
            else if (button == this.ButtonPercent)
            {
                context.createPercentModel();
            }
            else if (button == this.ButtonAbsolute)
            {
                this.isAbsolute = true;
                context.CompMinMax = context.Max - context.Min;
                context.CompMinMode = context.Mode - context.Min;
                context.CompModeMax = context.Max - context.Mode;

                var bindingMinMax = new Binding("CompMinMax")
                {
                    StringFormat = "Min-Max：{0:N0}s"
                };
                this.TextBlockCompMinMax.SetBinding(TextBlock.TextProperty, bindingMinMax);

                var bindingMinMode = new Binding("CompMinMode")
                {
                    StringFormat = "Min-Mode：{0:N0}s"
                };
                this.TextBlockCompMinMode.SetBinding(TextBlock.TextProperty, bindingMinMode);

                var bindingModeMax = new Binding("CompModeMax")
                {
                    StringFormat = "Mode-Max：{0:N0}s"
                };
                this.TextBlockCompModeMax.SetBinding(TextBlock.TextProperty, bindingModeMax);

                this.InvalidateVisual();
            }
            else if (button == this.ButtonRelative)
            {
                this.isAbsolute = false;
                context.CompMinMax = context.Max * 100 / context.Min;
                context.CompMinMode = context.Mode * 100 / context.Min;
                context.CompModeMax = context.Max * 100 / context.Mode;

                var bindingMinMax = new Binding("CompMinMax")
                {
                    StringFormat = "Min-Max：{0:N0}%"
                };
                this.TextBlockCompMinMax.SetBinding(TextBlock.TextProperty, bindingMinMax);

                var bindingMinMode = new Binding("CompMinMode")
                {
                    StringFormat = "Min-Mode：{0:N0}%"
                };
                this.TextBlockCompMinMode.SetBinding(TextBlock.TextProperty, bindingMinMode);

                var bindingModeMax = new Binding("CompModeMax")
                {
                    StringFormat = "Mode-Max：{0:N0}%"
                };
                this.TextBlockCompModeMax.SetBinding(TextBlock.TextProperty, bindingModeMax);

                this.InvalidateVisual();
            }
        }
    }
}
