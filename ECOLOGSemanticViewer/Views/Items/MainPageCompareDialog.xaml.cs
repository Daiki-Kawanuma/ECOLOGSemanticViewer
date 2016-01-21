using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.ViewModels.PageViewModels;
using ECOLOGSemanticViewer.ViewModels.WindowViewModels;
using ECOLOGSemanticViewer.Views.Windows;
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
    /// MainPageCompareDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class MainPageCompareDialog : UserControl
    {
        public SemanticLink SemanticLink { get; set; }

        public List<SemanticLink> SelectedSemanticLinks { get; set; }

        public TripDirection TripDirection { get; set; }

        public AbstMainPageViewModel ViewModel;

        public MainPageCompareDialog()
        {
            InitializeComponent();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedSemanticLinks.Add(SemanticLink);
        }

        private void Button_Compare(object sender, RoutedEventArgs e)
        {
            SelectedSemanticLinks.Add(SemanticLink);

            SemanticCompareWindow window = new SemanticCompareWindow();
            window.DataContext = new SemanticCompareWindowViewModel(SelectedSemanticLinks, TripDirection);

            window.Show();
        }
    }
}
