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
            button.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailTimePageViewModel;
            if (context == null) { return; }

            if (button == this.ButtonMin)
            {
                context.SetMinAnnotation();
            }
            else if (button == this.ButtonMode)
            {
                context.SetModeAnnotation();
            }
            else if (button == this.ButtonMedian)
            {
                context.SetMedianAnnotation();
            }
            else if (button == this.ButtonMax)
            {
                context.SetMaxAnnotation();
            }
            else if (button == this.ButtonDistUnderMode)
            {
                context.SetDistUnderModeAnnotation();
            }
            else if (button == this.ButtonDistMode)
            {
                context.SetDistModeAnnotation();
            }
            else if (button == this.ButtonDistUpperMode)
            {
                context.SetDistUpperModeAnnotation();
            }
            else if (button == this.ButtonComMinMax)
            {
                context.SetComMinMaxAnnotation();
            }
            else if (button == this.ButtonComMinMode)
            {
                context.SetComMinModeAnnotation();
            }
            else if (button == this.ButtonComModeMax)
            {
                context.SetComModeMaxAnnotation();
            }
        }
    }
}
