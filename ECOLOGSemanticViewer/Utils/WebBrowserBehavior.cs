using System;
using System.Windows.Controls;
using System.Windows;

namespace ECOLOGSemanticViewer.Utils
{
	/// <summary>
	/// WebBrowser クラス用の Source プロパティへの Binding 機能を提供する為のユーティリティ クラスです。
	/// </summary>
	public static class WebBrowserBehavior
	{

        #region Source 用の添付プロパティ設定

        /// <summary>
        /// SourceProperty を取得します。
        /// </summary>
        /// <param name="obj">依存プロパティ。</param>
        /// <returns>取得した値。</returns>
        public static string GetSource(DependencyObject obj)
        {
            return (string)obj.GetValue(SourceProperty);
        }

        /// <summary>
        /// SourceProperty を設定します。
        /// </summary>
        /// <param name="obj">依存プロパティ。</param>
        /// <param name="value">設定する値。</param>
        public static void SetSource(DependencyObject obj, string value)
        {
            obj.SetValue(SourceProperty, value);
        }

        /// <summary>
        /// SourceProperty が変更された時に発生します。
        /// </summary>
        /// <param name="o">依存プロパティ。</param>
        /// <param name="e">イベント データ。</param>
        public static void OnSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                string uri = e.NewValue as string;
                browser.Source = uri != null ? new Uri(uri) : null;
            }
        }

        /// <summary>
        /// Source プロパティへの Binding 機能を提供する為の依存プロパティです。
        /// </summary>
        public static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached("Source", typeof(string), typeof(WebBrowserBehavior), new UIPropertyMetadata(null, OnSourcePropertyChanged));

        #endregion

        #region ObjectForScripting 用の添付プロパティ設定

        /// <summary>
        /// ObjectForScriptingProperty を取得します。
        /// </summary>
        /// <param name="obj">依存プロパティ。</param>
        /// <returns>取得した値。</returns>
        public static string GetObjectForScripting(DependencyObject obj)
        {
            return (string)obj.GetValue(ObjectForScriptingProperty);
        }

        /// <summary>
        /// ObjectForScriptingProperty を設定します。
        /// </summary>
        /// <param name="obj">依存プロパティ。</param>
        /// <param name="value">設定する値。</param>
        public static void SetObjectForScripting(DependencyObject obj, string value)
        {
            obj.SetValue(ObjectForScriptingProperty, value);
        }

        /// <summary>
        /// ObjectForScriptingProperty が変更された時に発生します。
        /// </summary>
        /// <param name="o">依存プロパティ。</param>
        /// <param name="e">イベント データ。</param>
        public static void OnObjectForScriptingPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                browser.ObjectForScripting = e.NewValue;
            }
        }

        /// <summary>
        /// ObjectForScripting プロパティへの Binding 機能を提供する為の依存プロパティです。
        /// </summary>
        public static readonly DependencyProperty ObjectForScriptingProperty = DependencyProperty.RegisterAttached("ObjectForScripting", typeof(object), typeof(WebBrowserBehavior), new UIPropertyMetadata(OnObjectForScriptingPropertyChanged));

        #endregion
	}
}
