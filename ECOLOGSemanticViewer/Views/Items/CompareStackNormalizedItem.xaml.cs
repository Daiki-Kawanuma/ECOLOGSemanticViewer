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
        public AbstComparePageViewModel ParentViewModel { get; set; }
        public int CalclateTripNumber { get; set; }
        public double NormalizedValueSemanticFirst{get; set;}
        public double NormalizedValueSemanticSecond { get; set; }
        public double CalculatedValueSemanticFirst { get; set; }
        public double CalculatedValueSemanticSecond { get; set; }
        public double NormalizedValueDiff { get; set; }
        public double NormalizedValueDiffPercent { get; set; }
        public double CalculatedValueDiff { get; set; }
        public double CalculatedValueDiffPercent { get; set; }

        public CompareStackNormalizedItem()
        {
            InitializeComponent();
            this.DataContext = this;
            InvalidateVisual();
        }

        public void InitEnergyStringFormat()
        {
            this.LabelNormalizedValueSemanticFirst.ContentStringFormat = "Normalized lost energy：{0:N3}kWh/trip";
            this.LabelNormalizedValueSemanticSecond.ContentStringFormat = "Normalized lost energy：{0:N3}kWh/trip";
            this.LabelCalculatedValueSemanticFirst.ContentStringFormat = "Calculated lost energy：{0:N3}kWh";
            this.LabelCalculatedValueSemanticSecond.ContentStringFormat = "Calculated lost energy：{0:N3}kWh";
            this.LabelNormalizedValueDiff.ContentStringFormat = "Normalized lost energy diff：{0:N3}kWh/trip";
            this.LabelNormalizedValueDiffPercent.ContentStringFormat = "Normalized lost energy diff：{0:N1}%";
            this.LabelCalculatedValueDiff.ContentStringFormat = "Calculated lost energy diff：{0:N3}kWh";
            this.LabelCalculatedValueDiffPercent.ContentStringFormat = "Calculated lost energy diff：{0:N1}%";
        }

        public void InitTimeStringFormat()
        {
            this.LabelNormalizedValueSemanticFirst.ContentStringFormat = "Normalized time：{0:N0}s/trip";
            this.LabelNormalizedValueSemanticSecond.ContentStringFormat = "Normalized time：{0:N0}s/trip";
            this.LabelCalculatedValueSemanticFirst.ContentStringFormat = "Calculated time：{0:N0}s";
            this.LabelCalculatedValueSemanticSecond.ContentStringFormat = "Calculated time：{0:N0}s";
            this.LabelNormalizedValueDiff.ContentStringFormat = "Normalized time diff：{0:N0}s/trip";
            this.LabelNormalizedValueDiffPercent.ContentStringFormat = "Normalized time diff：{0:N1}%";
            this.LabelCalculatedValueDiff.ContentStringFormat = "Calculated time diff：{0:N0}s";
            this.LabelCalculatedValueDiffPercent.ContentStringFormat = "Calculated time diff：{0:N0}s";
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
