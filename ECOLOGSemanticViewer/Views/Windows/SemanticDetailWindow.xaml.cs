using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.ViewModels.PageViewModels;
using ECOLOGSemanticViewer.ViewModels.WindowViewModels;
using ECOLOGSemanticViewer.Views.Pages;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ECOLOGSemanticViewer.Views.Windows
{
    /* 
     * ViewModelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedWeakEventListenerや
     * CollectionChangedWeakEventListenerを使うと便利です。独自イベントの場合はLivetWeakEventListenerが使用できます。
     * クローズ時などに、LivetCompositeDisposableに格納した各種イベントリスナをDisposeする事でイベントハンドラの開放が容易に行えます。
     *
     * WeakEventListenerなので明示的に開放せずともメモリリークは起こしませんが、できる限り明示的に開放するようにしましょう。
     */

    /// <summary>
    /// SemanticDetailWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SemanticDetailWindow : MetroWindow
    {

        public SemanticDetailWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as SemanticDetailWindowViewModel;
            if (context == null) { return; }

            if (context.EnergyPage == null)
            {
                context.EnergyPage = new DetailEnergyPage();
                context.EnergyPage.DataContext = new DetailEnergyPageViewModel(context.SemanticLink, context.TripDirection);
            }

            context.CurrentPage = context.EnergyPage;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as SemanticDetailWindowViewModel;
            if (context == null) { return; }

            if (context.TimePage == null)
            {
                context.TimePage = new DetailTimePage();
                context.TimePage.DataContext = new DetailTimePageViewModel(context.SemanticLink, context.TripDirection);
            }

            context.CurrentPage = context.TimePage;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as SemanticDetailWindowViewModel;
            if (context == null) { return; }

            if (context.HeatMapPage == null)
            {
                context.HeatMapPage = new DetailHeatMapPage();
                // TODO ここをMVVMに直す
                context.HeatMapPage.DataContext = new DetailHeatMapPageViewModel(context.SemanticLink, context.TripDirection, new List<Driver> { new Driver() { DriverId = 1 } }, new List<Car>() { new Car() { CarId = 1 }, new Car() { CarId = 3 } }, new List<Sensor>() { new Sensor() { SensorId = 12 } });
            }

            context.CurrentPage = context.HeatMapPage;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as SemanticDetailWindowViewModel;
            if (context == null) { return; }

            if (context.ComparePage == null)
            {
                context.ComparePage = new DetailComparePage();
                context.ComparePage.DataContext = new DetailComparePageViewModel(context.SemanticLink, context.TripDirection);
            }

            context.CurrentPage = context.ComparePage;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as SemanticDetailWindowViewModel;
            if (context == null) { return; }

            if (context.TripDetailPage == null)
            {
                context.TripDetailPage = new DetailTripDetailPage();
            }

            context.CurrentPage = context.TripDetailPage;
        }

    }
}