using ECOLOGSemanticViewer.ViewModels.WindowViewModels;
using ECOLOGSemanticViewer.Views.Pages;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }

            context.CurrentPage = new MainMapPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }

            context.CurrentPage = new MainEnergyPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }

            context.CurrentPage = new MainTimePage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            if (context == null) { return; }

            if (context.SelectedSemanticLink != null)
            {
                Console.WriteLine("Add called ...");
                context.ExtractedSemanticLinks.Add(context.SelectedSemanticLink);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //SemanticDetailWindow window = new SemanticDetailWindow();
            SemanticCompareWindow window = new SemanticCompareWindow();
            window.Show();
        }
    }
}
