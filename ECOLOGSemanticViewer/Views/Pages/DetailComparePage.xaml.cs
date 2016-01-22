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
    /// DetailSpeedPage.xaml の相互作用ロジック
    /// </summary>
    public partial class DetailComparePage : Page
    {
        public DetailComparePage()
        {
            InitializeComponent();
        }

        private void Button_Display(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as DetailComparePageViewModel;
            if (context == null) { return; }

            context.DisplayGraph();
        }
    }
}
