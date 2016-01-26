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

        public CompareStackNormalizedItem()
        {
            InitializeComponent();
            this.DataContext = this;
            InvalidateVisual();
        }

        public void InitEnergyStringFormat()
        {
            this.LabelNormalizedValueSemanticFirst.ContentStringFormat = "Norm energy：{0:N3}kWh/trip";
            this.LabelNormalizedValueSemanticSecond.ContentStringFormat = "Norm energy：{0:N3}kWh/trip";
            this.LabelCalculatedValueSemanticFirst.ContentStringFormat = "Calc energy：{0:N3}kWh";
            this.LabelCalculatedValueSemanticSecond.ContentStringFormat = "Calc energy：{0:N3}kWh";
            this.LabelNormalizedValueDiff.ContentStringFormat = "Norm energy diff：{0:N3}kWh/trip";
            this.LabelNormalizedValueDiffPercent.ContentStringFormat = "Norm energy diff：{0:N1}%";
            this.LabelCalculatedValueDiff.ContentStringFormat = "Calc energy diff：{0:N3}kWh";
        }

        public void InitTimeStringFormat()
        {
            this.LabelNormalizedValueSemanticFirst.ContentStringFormat = "Norm time：{0:N0}s/trip";
            this.LabelNormalizedValueSemanticSecond.ContentStringFormat = "Norm time：{0:N0}s/trip";
            this.LabelCalculatedValueSemanticFirst.ContentStringFormat = "Calc time：{0:N0}s";
            this.LabelCalculatedValueSemanticSecond.ContentStringFormat = "Calc time：{0:N0}s";
            this.LabelNormalizedValueDiff.ContentStringFormat = "Norm time diff：{0:N0}s/trip";
            this.LabelNormalizedValueDiffPercent.ContentStringFormat = "Norm time diff：{0:N1}%";
            this.LabelCalculatedValueDiff.ContentStringFormat = "Calc time diff：{0:N0}s";
        }

        private void Button_Calculate(object sender, RoutedEventArgs e)
        {
            calculateValue();
            this.ParentViewModel.CreateCalculatedPlotModel(this.CalclateTripNumber);
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            this.ParentViewModel.CreatePlotModel();
        }

        private void calculateValue()
        {
            this.CalculatedValueSemanticFirst = this.NormalizedValueSemanticFirst * this.CalclateTripNumber;
            this.CalculatedValueSemanticSecond = this.NormalizedValueSemanticSecond * this.CalclateTripNumber;
            this.CalculatedValueDiff = Math.Abs(this.NormalizedValueSemanticFirst * this.CalclateTripNumber -
                this.NormalizedValueSemanticSecond * this.CalclateTripNumber);

            var expression = this.LabelCalculatedValueSemanticFirst.GetBindingExpression(Label.ContentProperty);
            expression.UpdateTarget();

            expression = this.LabelCalculatedValueSemanticSecond.GetBindingExpression(Label.ContentProperty);
            expression.UpdateTarget();

            expression = this.LabelCalculatedValueDiff.GetBindingExpression(Label.ContentProperty);
            expression.UpdateTarget();
        }
    }
}
