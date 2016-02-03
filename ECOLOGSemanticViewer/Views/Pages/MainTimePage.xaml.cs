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
    /// MainTimePage.xaml の相互作用ロジック
    /// </summary>
    public partial class MainTimePage : Page
    {
        public MainTimePage()
        {
            InitializeComponent();
        }

        private void Button_Number(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainTimePageViewModel;

            if (context == null)
                return;

            context.CreateNumberModel();
        }

        private void Button_Percent(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainTimePageViewModel;

            if (context == null)
                return;

            context.CreatePercentModel();
        }

        private void Button_Raw(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainTimePageViewModel;

            if (context == null)
                return;

            context.CreatePlotModel();
        }

        private void Button_Normalized(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainTimePageViewModel;

            if (context == null)
                return;

            context.CreateNormalizedPlotModel();
        }
    }
}
