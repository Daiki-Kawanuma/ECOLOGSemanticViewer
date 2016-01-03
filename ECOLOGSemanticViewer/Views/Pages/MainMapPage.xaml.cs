using ECOLOGSemanticViewer.ViewModels;
using ECOLOGSemanticViewer.ViewModels.PageViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// MainMapPage.xaml の相互作用ロジック
    /// </summary>
    public partial class MainMapPage : Page
    {
        /// <summary>
		/// インスタンスを初期化します。
		/// </summary>
        public MainMapPage()
		{
			
            string uri       = String.Format( "file://{0}Resources\\index.html", AppDomain.CurrentDomain.BaseDirectory );
            this.DataContext = new MainMapPageViewModel(uri, InvokeScript);
            this.InitializeComponent();
		}

		/// <summary>
		/// ブラウザ オブジェクトに読み込まれているスクリプトを実行します。
		/// </summary>
		/// <param name="scriptName">実行するスクリプト関数の名前。</param>
		/// <param name="args">スクリプト関数に渡すパラメータ。</param>
		public void InvokeScript( string scriptName, params object[] args )
		{
			this._webBrowser.InvokeScript( scriptName, args );
		}
	}
}
