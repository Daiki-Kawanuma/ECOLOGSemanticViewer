using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.ViewModels.PageViewModels;
using ECOLOGSemanticViewer.ViewModels.WindowViewModels;
using ECOLOGSemanticViewer.Views.Pages;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void Button_Click_Map(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }


            MainMapPage page = new MainMapPage();
            page.DataContext = new MainMapPageViewModel(context.ExtractedSemanticLinks.ToList(), context.TripDirection, page.InvokeScript);

            context.CurrentPage = page;
        }

        private void Button_Click_Energy(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }

            MainEnergyPage page = new MainEnergyPage();
            page.DataContext = new MainEnergyPageViewModel(context.ExtractedSemanticLinks.ToList(), context.TripDirection);
            //page.DataContext = new TestMainEnergyPageViewModel();

            context.CurrentPage = page;
        }

        private void Button_Click_Time(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }

            MainTimePage page = new MainTimePage();
            page.DataContext = new MainTimePageViewModel(context.ExtractedSemanticLinks.ToList(), context.TripDirection);

            context.CurrentPage = page;
        }

        private void Button_Click_Outward(object sender, RoutedEventArgs e)
        {
            this.ButtonOutward.Foreground = Brushes.Orange;
            this.ButtonHomeward.ClearValue(Button.ForegroundProperty);
            
            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }

            if (context.TripDirection != null)
            {
                context.TripDirection.Direction = "outward";
            }
        }

        private void Button_Click_Homeward(object sender, RoutedEventArgs e)
        {
            this.ButtonOutward.ClearValue(Button.ForegroundProperty);
            this.ButtonHomeward.Foreground = Brushes.Orange;

            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }

            if (context.TripDirection != null)
            {
                context.TripDirection.Direction = "homeward";
            }
        }

        private void Button_Click_OutwardLinks(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }

            context.ExtractedSemanticLinks = new ObservableCollection<SemanticLink>(SemanticLink.GetDefaultOutwardSemanticLinks());
        }

        private void Button_Click_HomewardLinks(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }

            context.ExtractedSemanticLinks = new ObservableCollection<SemanticLink>(SemanticLink.GetDefaultHomewardSemanticLinks());
        }

        private void Button_Click_AddLink(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }

            if (context.SelectedSemanticLink != null)
            {
                context.ExtractedSemanticLinks.Add(context.SelectedSemanticLink);
            }
        }

        private void Button_Click_ShowDetailWindow(object sender, RoutedEventArgs e)
        {
            SemanticDetailWindow window = new SemanticDetailWindow();
            //SemanticCompareWindow window = new SemanticCompareWindow();
            window.Show();
        }
    }
}
