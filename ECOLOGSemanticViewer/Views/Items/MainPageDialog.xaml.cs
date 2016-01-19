using ECOLOGSemanticViewer.Models.EcologModels;
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
    public partial class MainPageDialog : UserControl
    {
        public SemanticLink SemanticLink { get; set; }

        public MainPageDialog()
        {
            InitializeComponent();
        }

        private void Button_ShowDetail(object sender, RoutedEventArgs e)
        {
            SemanticDetailWindow window = new SemanticDetailWindow();
            window.DataContext = new SemanticDetailWindowViewModel() { SemanticLink = this.SemanticLink };

            window.Show();
        }
    }
}
