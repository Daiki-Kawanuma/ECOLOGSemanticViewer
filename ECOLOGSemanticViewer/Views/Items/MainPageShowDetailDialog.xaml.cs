using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.ViewModels.PageViewModels;
using ECOLOGSemanticViewer.ViewModels.WindowViewModels;
using ECOLOGSemanticViewer.Views.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Dialog.xaml の相互作用ロジック
    /// </summary>
    public partial class MainPageShowDetailDialog : UserControl
    {
        public SemanticLink SemanticLink { get; set; }

        public TripDirection TripDirection { get; set; }

        public AbstMainPageViewModel ViewModel;

        public MainPageShowDetailDialog()
        {
            InitializeComponent();
        }

        private void Button_ShowDetail(object sender, RoutedEventArgs e)
        {
            SemanticDetailWindow window = new SemanticDetailWindow();
            window.DataContext = new SemanticDetailWindowViewModel(this.SemanticLink, this.TripDirection);

            window.Show();
        }

        private void Button_CompareWith(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedSemanticLinks.Add(SemanticLink);
        }
    }
}
