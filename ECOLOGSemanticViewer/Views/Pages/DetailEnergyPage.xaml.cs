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
    /// DetailEnergyPage.xaml の相互作用ロジック
    /// </summary>
    public partial class DetailEnergyPage : Page
    {
        public DetailEnergyPage()
        {
            InitializeComponent();
        }

        private void clearButtonCalar()
        {
            this.ButtonMin.Foreground = SystemColors.ControlDarkDarkBrush;
        }

        private void Button_Click_Min(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as DetailEnergyPageViewModel ;
            if (context == null) { return; }

            context.SetMinAnnotation();
        }

        private void Button_Click_Mode(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();
            this.ButtonMode.Foreground = Brushes.Orange;
            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetModeAnnotation();
        }

        private void Button_Click_Median(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetMedianAnnotation();
        }

        private void Button_Click_Max(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetMaxAnnotation();
        }

        private void Button_Click_UnderMode(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetUnderModeAnnotation();
        }

        private void Button_Click_DistMode(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetDistModeAnnotation();
        }

        private void Button_Click_UpperMode(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetUpperModeAnnotation();
        }
    }
}
