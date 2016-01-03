using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECOLOGSemanticViewer.Models.MapModels
{
	/// <summary>
	/// Google Map 内のマーカーを表すクラスです。
	/// </summary>
	class Marker
	{
		/// <summary>
		/// 緯度を取得または設定します。
		/// </summary>
		public string Latitude { get; set; }

		/// <summary>
		/// 経度を取得または設定します。
		/// </summary>
		public string Longitude { get; set; }
	}
}
