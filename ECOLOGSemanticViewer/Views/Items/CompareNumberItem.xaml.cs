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
        public AbstComparePageViewModel ParentViewModel { get; set; }
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

        public void InitEnergyStringFormat()
        {
            this.LabelMinSemanticFirst.ContentStringFormat = "A：{0:N3}kWh";
            this.LabelMinSemanticSecond.ContentStringFormat = "B：{0:N3}kWh";
            this.LabelModeSemanticFirst.ContentStringFormat = "A：{0:N3}kWh";
            this.LabelModeSemanticSecond.ContentStringFormat = "B：{0:N3}kWh";
            this.LabelMaxSemanticFirst.ContentStringFormat = "A：{0:N3}kWh";
            this.LabelMaxSemanticSecond.ContentStringFormat = "B：{0:N3}kWh";
            this.LabelAverageSemanticFirst.ContentStringFormat = "A：{0:N3}kWh";
            this.LabelAverageSemanticSecond.ContentStringFormat = "B：{0:N3}kWh";
        }

        public void InitNormalizedEnergyStringFormat()
        {
            this.LabelMinSemanticFirst.ContentStringFormat = "A：{0:N3}kWh/km";
            this.LabelMinSemanticSecond.ContentStringFormat = "B：{0:N3}kWh/km";
            this.LabelModeSemanticFirst.ContentStringFormat = "A：{0:N3}kWh/km";
            this.LabelModeSemanticSecond.ContentStringFormat = "B：{0:N3}kWh/km";
            this.LabelMaxSemanticFirst.ContentStringFormat = "A：{0:N3}kWh/km";
            this.LabelMaxSemanticSecond.ContentStringFormat = "B：{0:N3}kWh/km";
            this.LabelAverageSemanticFirst.ContentStringFormat = "A：{0:N3}kWh/km";
            this.LabelAverageSemanticSecond.ContentStringFormat = "B：{0:N3}kWh/km";
        }

        public void InitTimeStringFormat()
        {
            this.LabelMinSemanticFirst.ContentStringFormat = "A：{0:N0}s";
            this.LabelMinSemanticSecond.ContentStringFormat = "B：{0:N0}s";
            this.LabelModeSemanticFirst.ContentStringFormat = "A：{0:N0}s";
            this.LabelModeSemanticSecond.ContentStringFormat = "B：{0:N0}s";
            this.LabelMaxSemanticFirst.ContentStringFormat = "A：{0:N0}s";
            this.LabelMaxSemanticSecond.ContentStringFormat = "B：{0:N0}s";
            this.LabelAverageSemanticFirst.ContentStringFormat = "A：{0:N0}s";
            this.LabelAverageSemanticSecond.ContentStringFormat = "B：{0:N0}s";
        }

        public void InitNormalizedTimeStringFormat()
        {
            this.LabelMinSemanticFirst.ContentStringFormat = "A：{0:N0}s/km";
            this.LabelMinSemanticSecond.ContentStringFormat = "B：{0:N0}s/km";
            this.LabelModeSemanticFirst.ContentStringFormat = "A：{0:N0}s/km";
            this.LabelModeSemanticSecond.ContentStringFormat = "B：{0:N0}s/km";
            this.LabelMaxSemanticFirst.ContentStringFormat = "A：{0:N0}s/km";
            this.LabelMaxSemanticSecond.ContentStringFormat = "B：{0:N0}s/km";
            this.LabelAverageSemanticFirst.ContentStringFormat = "A：{0:N0}s/km";
            this.LabelAverageSemanticSecond.ContentStringFormat = "B：{0:N0}s/km";
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
