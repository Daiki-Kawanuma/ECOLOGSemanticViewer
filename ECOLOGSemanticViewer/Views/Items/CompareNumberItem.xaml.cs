using ECOLOGSemanticViewer.ViewModels.PageViewModels;
using Livet;
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

namespace ECOLOGSemanticViewer.Views.Items
{
    /// <summary>
    /// CompareNumberItem.xaml の相互作用ロジック
    /// </summary>
    public partial class CompareNumberItem : UserControl
    {
        public CompareEnergyPageViewModel ParentViewModel { get; set; }
        public double MinSemanticFirst { get; set; }
        public double MinSemanticSecond { get; set; }
        public double ModeSemanticFirst { get; set; }
        public double ModeSemanticSecond { get; set; }
        public double MaxSemanticFirst { get; set; }
        public double MaxSemanticSecond { get; set; }
        public double AverageSemanticFirst { get; set; }
        public double AverageSemanticSecond { get; set; }

        public CompareNumberItem()
        {
            InitializeComponent();
            DataContext = this;
            InvalidateVisual();
        }

        private void ButtonNumber_Number(object sender, RoutedEventArgs e)
        {
            this.ParentViewModel.CreatePlotModel();
        }

        private void ButtonPercent_Percent(object sender, RoutedEventArgs e)
        {
            this.ParentViewModel.CreatePercentilePlotModel();
        }
    }
}
