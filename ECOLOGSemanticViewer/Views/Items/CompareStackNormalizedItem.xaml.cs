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

namespace ECOLOGSemanticViewer.Views.Items
{
    /// <summary>
    /// CompareStackNormalizedItem.xaml の相互作用ロジック
    /// </summary>
    public partial class CompareStackNormalizedItem : UserControl
    {
        public const double DefaultCalculateTripNumber = 1000;
        public CompareEnergyPageViewModel ParentViewModel { get; set; }
        public int CalclateTripNumber { get; set; }
        public double NormalizedLostEnergySemanticFirst{get; set;}
        public double NormalizedLostEnergySemanticSecond { get; set; }
        public double CalculatedLostEnergySemanticFirst { get; set; }
        public double CalculatedLostEnergySemanticSecond { get; set; }
        public double NormalizedLostEnergyDiff { get; set; }
        public double NormalizedLostEnergyDiffPercent { get; set; }
        public double CalculatedLostEnergyDiff { get; set; }
        public double CalculatedLostEnergyDiffPercent { get; set; }

        public CompareStackNormalizedItem()
        {
            InitializeComponent();
            this.DataContext = this;
            InvalidateVisual();
        }

        private void Button_Calculate(object sender, RoutedEventArgs e)
        {
            this.ParentViewModel.CreateCalculatedPlotModel(this.CalclateTripNumber);
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            this.ParentViewModel.CreatePlotModel();
        }
    }
}
