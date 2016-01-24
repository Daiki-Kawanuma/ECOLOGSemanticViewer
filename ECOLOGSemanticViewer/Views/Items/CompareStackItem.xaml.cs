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
    }
}
