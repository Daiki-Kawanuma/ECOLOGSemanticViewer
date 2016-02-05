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
    /// DetailTripDetail.xaml の相互作用ロジック
    /// </summary>
    public partial class DetailTripDetailPage : Page
    {
        public DetailTripDetailPage()
        {
            InitializeComponent();
        }

        public void InvokeScript(string scriptName, params object[] args)
        {
            this.webBrowser.InvokeScript(scriptName, args);
        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            var context = this.DataContext as DetailTripDetailPageViewModel;

            if (context == null)
                return;

            if (e.Key == Key.Right)
            {
                if (context.CurrentIndex < context.SliderMaximum)
                    context.CurrentIndex++;
            }
            else
            {
                if (context.CurrentIndex > 0)
                    context.CurrentIndex--;
            }
        }

        private void Page_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var context = this.DataContext as DetailTripDetailPageViewModel;

            if (context == null)
                return;

            if (context.CurrentIndex + e.Delta / 10 < context.SliderMaximum && context.CurrentIndex + e.Delta / 10 > 0)
                context.CurrentIndex += e.Delta / 10;
            // TODO アルゴリズムを考えて☆
        }
    }
}
