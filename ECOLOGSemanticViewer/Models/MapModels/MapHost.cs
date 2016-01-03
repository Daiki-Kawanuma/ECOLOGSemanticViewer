using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.Generic;
using System;

namespace ECOLOGSemanticViewer.Models.MapModels
{
	/// <summary>
	/// Google Map の操作を行う JavaScript に関連付けられるクラスです。
	/// </summary>
	[ComVisible( true )]
	public class MapHost : INotifyPropertyChanged
	{
		/// <summary>
		/// マップの移動が行われた時、スクリプト側から呼び出されます。
		/// </summary>
		/// <param name="latitude">緯度。</param>
		/// <param name="longitude">経度。</param>
		public void OnMapMoved( string latitude, string longitude )
		{
			this._location = MapHost.MakeLocationInfo( latitude, longitude );
			this.NotifyPropertyChanged( "Location" );
		}

		/// <summary>
		/// マーカーが追加された時、スクリプト側から呼び出されます。
		/// </summary>
		/// <param name="id">マーカーの識別子。</param>
		/// <param name="latitude">緯度。</param>
		/// <param name="longitude">経度。</param>
		public void OnMarkerAdded( int id, string latitude, string longitude )
		{
			this._markers.Add( id, new Marker() { Latitude = latitude, Longitude = longitude } );
			this._markerLocation = MapHost.MakeLocationInfo( latitude, longitude );

			if( this._markers.Count == MapHost.MarkerMax )
			{
				this._canAddMarker = false;
				this.NotifyPropertyChanged( "MarkerLocation", "CanAddMarker" );
			}
			else
			{
				this.NotifyPropertyChanged( "MarkerLocation" );
			}
		}

		/// <summary>
		/// マーカーの選択状態が変更された時、スクリプト側から呼び出されます。
		/// マーカーの選択は常に単体のみとなります。
		/// </summary>
		/// <param name="id">選択されたマーカーの識別子。選択されているものがない場合は -1。</param>
		public void OnMarkerSelectionChanged( int id )
		{
			this._currentMarkerId = id;

			if( id == -1 )
			{
				this._markerLocation = String.Empty;
				this.NotifyPropertyChanged( "MarkerLocation", "IsSelectedMarker" );
			}
			else
			{
				Marker marker;
				if( this._markers.TryGetValue( id, out marker ) )
				{
					this._markerLocation = MapHost.MakeLocationInfo( marker.Latitude, marker.Longitude );
					this.NotifyPropertyChanged( "MarkerLocation", "IsSelectedMarker" );
				}
			}
		}

		/// <summary>
		/// マーカーが移動された時、スクリプト側から呼び出されます。
		/// </summary>
		/// <param name="id">マーカーの識別子。</param>
		/// <param name="latitude">緯度。</param>
		/// <param name="longitude">経度。</param>
		public void OnMarkerMoved( int id, string latitude, string longitude )
		{
			Marker marker;
			if( this._markers.TryGetValue( id, out marker ) )
			{
				marker.Latitude  = latitude;
				marker.Longitude = longitude;

				if( this._currentMarkerId == id )
				{
					this._markerLocation = MapHost.MakeLocationInfo( latitude, longitude );
					this.NotifyPropertyChanged( "MarkerLocation" );
				}
			}
		}

		/// <summary>
		/// マーカーが削除された時、スクリプト側から呼び出されます。
		/// </summary>
		/// <param name="id">マーカーの識別子。</param>
		public void OnMarkerRemoved( int id )
		{
			this._markers.Remove( id );

			if( this._markers.Count == ( MapHost.MarkerMax - 1 ) )
			{
				this._canAddMarker = true;
				this.NotifyPropertyChanged( "MarkerLocation", "CanAddMarker" );
			}
		}

		/// <summary>
		/// 緯度と経度から、座標情報を表す文字列を作成します。
		/// </summary>
		/// <param name="latitude">緯度。</param>
		/// <param name="longitude">経度。</param>
		/// <returns>座標情報を表す文字列。</returns>
		private static string MakeLocationInfo( string latitude, string longitude )
		{
			return String.Format( "緯度 = {0}、経度 = {1}", latitude, longitude ); ;
		}

		/// <summary>
		/// マーカーの作成が可能である事を示す値を取得します。
		/// </summary>
		public bool CanAddMarker { get { return this._canAddMarker; } }

		/// <summary>
		/// マップ上でマーカーが選択されている事を示す値を取得します。
		/// </summary>
		public bool IsSelectedMarker { get { return ( this._currentMarkerId != MapHost.UnselectMarkerId ); } }

		/// <summary>
		/// マップ上の緯度・経度を示す文字列を取得または設定します。
		/// </summary>
		public string Location { get { return this._location; } }

		/// <summary>
		/// マップ上のマーカーの緯度・経度を示す文字列を取得または設定します。
		/// </summary>
		public string MarkerLocation { get { return this._markerLocation; } }

		/// <summary>
		/// マーカーが未選択である事を示す識別子。
		/// </summary>
		private const int UnselectMarkerId = -1;

		/// <summary>
		/// マーカーの最大作成数。
		/// </summary>
		private const int MarkerMax = 3;

		/// <summary>
		/// マーカーの作成が可能である事を示す値。
		/// </summary>
		private bool _canAddMarker = true;

		/// <summary>
		/// マップの中央移動座標。
		/// </summary>
		private string _location;

		/// <summary>
		/// マップ上のマーカー座標。
		/// </summary>
		private string _markerLocation;

		/// <summary>
		/// 選択されているマーカーの識別子。未選択の場合は UnselectMarkerId となる。
		/// </summary>
		private int _currentMarkerId = MapHost.UnselectMarkerId;

		/// <summary>
		/// ID をキーとするマーカーのディクショナリ。
		/// </summary>
		private Dictionary< int, Marker > _markers = new Dictionary< int, Marker >();

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
