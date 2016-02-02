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
    /// DetailTripDetail.xaml の相互作用ロジック
    /// </summary>
    public partial class DetailTripDetailPage : Page
    {
        public DetailTripDetailPage()
        {
            InitializeComponent();
        }

        public void InvokeScript(string scriptName, params object[] args)
        {
            this.webBrowser.InvokeScript(scriptName, args);
        }
    }
}
