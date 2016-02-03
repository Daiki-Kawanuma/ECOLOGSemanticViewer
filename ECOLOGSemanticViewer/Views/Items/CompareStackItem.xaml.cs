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
    /// CompareStackItem.xaml の相互作用ロジック
    /// </summary>
    public partial class CompareStackItem : UserControl
    {
        public int TotalNumberSemanticFirst { get; set; }
        public int TotalNumberSemanticSecond { get; set; }
        public double TotalLostEnergySemanticFirst { get; set; }
        public double TotalLostEnergySemanticSecond { get; set; }
        public int NumberDiff { get; set; }
        public double LostEnergyDiff { get; set; }
        public double LostEnergyDiffPercent { get; set; }

        public CompareStackItem()
        {
            InitializeComponent();
            this.DataContext = this;
            InvalidateVisual();
        }

        public void InitEnergyStringFormat()
        {
            this.LabelTotalNumberSemanticFirst.ContentStringFormat = "Total number：{0:N0} trip";
            this.LabelTotalValueSemanticFirst.ContentStringFormat = "Total energy：{0:N1}kWh";
            this.LabelTotalNumberSemanticSecond.ContentStringFormat = "Total number：{0:N0} trip";
            this.LabelTotalValueSemanticSecond.ContentStringFormat = "Total energy：{0:N1}kWh";
            this.LabelNumberDiff.ContentStringFormat = "Number diff：{0:N0} trip";
            this.LabelValueDiff.ContentStringFormat = "Energy diff：{0:N1}kWh";
            this.LabelValueDiffPercent.ContentStringFormat = "Energy diff：{0:N1}%";
        }

        public void InitTimeStringFormat()
        {
            this.LabelTotalNumberSemanticFirst.ContentStringFormat = "Total number：{0:N0} trip";
            this.LabelTotalValueSemanticFirst.ContentStringFormat = "Total time：{0:N0}s";
            this.LabelTotalNumberSemanticSecond.ContentStringFormat = "Total number：{0:N0} trip";
            this.LabelTotalValueSemanticSecond.ContentStringFormat = "Total time：{0:N0}s";
            this.LabelNumberDiff.ContentStringFormat = "Number diff：{0:N0} trip";
            this.LabelValueDiff.ContentStringFormat = "Time diff：{0:N0}s";
            this.LabelValueDiffPercent.ContentStringFormat = "Time diff：{0:N1}%";
        }
    }
}
