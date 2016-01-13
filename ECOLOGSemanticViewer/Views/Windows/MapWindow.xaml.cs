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
    /// MapWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MapWindow : MetroWindow
    {
        public MapWindow()
        {

            string uri = String.Format("file://{0}Resources\\SemanticLink.html", AppDomain.CurrentDomain.BaseDirectory);
            this.DataContext = new MapWindowViewModel(uri);
            InitializeComponent();
        }

        class MapWindowViewModel
        {
            /// <summary>
            /// インスタンスを初期化します。
            /// </summary>
            /// <param name="uri">ブラウザに表示するページの URI。</param>
            public MapWindowViewModel(string uri)
            {
                this.Uri = uri;
            }

            public void Initialize()
            {
            }

            /// <summary>
            /// Google Map に指定する住所を取得または設定します。
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// ブラウザに表示するページの URI を取得します。
            /// </summary>
            public string Uri { get; private set; }
        }
    }
}