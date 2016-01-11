using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using ECOLOGSemanticViewer.Models;
using ECOLOGSemanticViewer.Utils;
using System.Windows.Input;
using ECOLOGSemanticViewer.Commands;
using ECOLOGSemanticViewer.Models.MapModels;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class MainMapPageViewModel : AbstMainPageViewModel
    {
        public void Initialize()
        {
        }

        /// <summary>
		/// ブラウザのスクリプト実行を行う為のデリゲート。
		/// </summary>
		/// <param name="scriptName">実行するスクリプト関数の名前。</param>
		/// <param name="args">スクリプト関数に渡すパラメータ。</param>
		public delegate void InvokeScript( string scriptName, params object[] args );

		/// <summary>
		/// インスタンスを初期化します。
		/// </summary>
		/// <param name="uri">ブラウザに表示するページの URI。</param>
		/// <param name="invokScript">ブラウザのスクリプト実行を行う為のデリゲート。</param>
        public MainMapPageViewModel(string uri, InvokeScript invokScript)
		{
			this._invokeScript       = invokScript;
			this.Uri                 = uri;
			this.MapHost             = new MapHost();
			this.AddMarkerCommand    = new DelegateCommand( () => { this._invokeScript( "addMarker" ); } );
			this.RemoveMarkerCommand = new DelegateCommand( () => { this._invokeScript( "removeMarker" ); } );
			this.ShowMarkerCommand   = new DelegateCommand( () => { this._invokeScript( "showMarker" ); } );
			this.MoveMapCommand      = new DelegateCommand( () => { this._invokeScript( "moveMap", this.Address ); } );

			this.MapHost.PropertyChanged += new PropertyChangedEventHandler( OnMapHostPropertyChanged );
	
		}

		/// <summary>
		/// MapHost のプロパティが変更された時に発生するイベントです。
		/// </summary>
		/// <param name="sender">イベント発生元。</param>
		/// <param name="e">イベント データ。</param>
		private void OnMapHostPropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			if( this.PropertyChanged != null )
			{
				this.PropertyChanged( this, e );
			}
		}

		/// <summary>
		/// マーカーの追加コマンドを取得します。
		/// </summary>
		public ICommand AddMarkerCommand { get; private set; }

		/// <summary>
		/// マーカーの削除コマンドを取得します。
		/// </summary>
		public ICommand RemoveMarkerCommand { get; private set; }

		/// <summary>
		/// マーカーの表示を行うコマンドを取得します。
		/// </summary>
		public ICommand ShowMarkerCommand { get; private set; }

		/// <summary>
		/// 現在設定されている Address の位置にマップを移動させるコマンドを取得します。
		/// </summary>
		public ICommand MoveMapCommand { get; private set; }

		/// <summary>
		/// マーカーの位置へマップを移動させるコマンドを取得します。
		/// </summary>
		public ICommand MoveMapToMarkerCommand { get; private set; }

		/// <summary>
		/// マーカーの作成が可能である事を示す値を取得します。
		/// </summary>
		public bool CanAddMarker { get { return this.MapHost.CanAddMarker; } }

		/// <summary>
		/// マップ上でマーカーが選択されている事を示す値を取得します。
		/// </summary>
		public bool IsSelectedMarker { get { return this.MapHost.IsSelectedMarker; } }
		
		/// <summary>
		/// マップ上の緯度・経度を示す文字列を取得または設定します。
		/// </summary>
		public string Location { get { return this.MapHost.Location; } }

		/// <summary>
		/// マップ上のマーカーの緯度・経度を示す文字列を取得または設定します。
		/// </summary>
		public string MarkerLocation { get { return this.MapHost.MarkerLocation; } }

		/// <summary>
		///  マップの操作を行う JavaScript に関連付けられるオブジェクトを取得します。
		/// </summary>
		public MapHost MapHost { get; private set; }

		/// <summary>
		/// マップに指定する住所を取得または設定します。
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// ブラウザに表示するページの URI を取得します。
		/// </summary>
		public string Uri { get; private set; }

		/// <summary>
		/// ブラウザのスクリプト実行を行う為のデリゲート。
		/// </summary>
		private InvokeScript _invokeScript;

		#region INotifyPropertyChanged メンバ

		/// <summary>
		/// プロパティが変更された事を通知するイベント ハンドラ。
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// プロパティの変更を通知します。
		/// </summary>
		/// <param name="names">変更されたプロパティ名のコレクション。</param>
		protected void NotifyPropertyChanged( params string[] names )
		{
			if( this.PropertyChanged != null )
			{
				foreach( string name in names )
				{
					this.PropertyChanged( this, new PropertyChangedEventArgs( name ) );
				}
			}
		}

		#endregion
    }
}
