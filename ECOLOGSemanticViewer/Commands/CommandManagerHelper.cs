using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ECOLOGSemanticViewer.Commands
{
	/// <summary>
	/// このクラスは CommandManager に対する、弱参照を使うことでメモリリークを防ぐ助けとなるメソッドを含んでいます。
	/// </summary>
	class CommandManagerHelper
	{
		/// <summary>
		/// コマンドのイベント ハンドラ コレクションに対する、呼び出しを実行します。
		/// </summary>
		/// <param name="handlers">コマンドのイベント ハンドラ コレクション。</param>
		public static void CallWeakReferenceHandlers( List< WeakReference > handlers )
		{
			if( handlers != null )
			{
				// 我々が私への配列を読んでいる間にハンドラはそれの変更を引き起こしうるので、
				// それらを呼び出す前にハンドラのスナップショットをとります。
				//
				EventHandler[] callees = new EventHandler[ handlers.Count ];
				int count = 0;

				for( int i = handlers.Count - 1; i >= 0; i-- )
				{
					WeakReference reference = handlers[ i ];
					EventHandler handler = reference.Target as EventHandler;
					if( handler == null )
					{
						// 収集されたハンドラを削除します
						handlers.RemoveAt( i );
					}
					else
					{
						callees[ count ] = handler;
						count++;
					}
				}

				// スナップショットをとったハンドラを呼びます
				for( int i = 0; i < count; i++ )
				{
					EventHandler handler = callees[ i ];
					handler( null, EventArgs.Empty );
				}
			}
		}

		/// <summary>
		/// コマンドの実行可能状態が変更された時に呼び出される、イベント ハンドラを登録します。
		/// </summary>
		/// <param name="handlers">コマンドのイベント ハンドラ コレクション。</param>
		public static void AddHandlersToRequerySuggested( List< WeakReference > handlers )
		{
			if( handlers != null )
			{
				foreach( WeakReference handlerRef in handlers )
				{
					EventHandler handler = handlerRef.Target as EventHandler;
					if( handler != null )
					{
						CommandManager.RequerySuggested += handler;
					}
				}
			}
		}

		/// <summary>
		/// コマンドのイベント ハンドラを、弱参照リストへ登録します。
		/// </summary>
		/// <param name="handlers">コマンドのイベント ハンドラのリスト。</param>
		/// <param name="handler">コマンドのイベント ハンドラ。</param>
		public static void AddWeakReferenceHandler( ref List< WeakReference > handlers, EventHandler handler )
		{
			AddWeakReferenceHandler( ref handlers, handler, -1 );
		}

		/// <summary>
		/// コマンドのイベント ハンドラを、弱参照リストへ登録します。
		/// </summary>
		/// <param name="handlers">コマンドのイベント ハンドラのリスト。</param>
		/// <param name="handler">コマンドのイベント ハンドラ。</param>
		/// <param name="defaultListSize">初期状態のリストの要素数。</param>
		public static void AddWeakReferenceHandler( ref List<WeakReference> handlers, EventHandler handler, int defaultListSize )
		{
			if( handlers == null )
			{
				handlers = ( defaultListSize > 0 ? new List< WeakReference >( defaultListSize ) : new List< WeakReference >() );
			}

			handlers.Add( new WeakReference( handler ) );
		}

		/// <summary>
		/// コマンドのハンドラ ハンドラの登録を解除します。
		/// </summary>
		/// <param name="handlers">コマンドのイベント ハンドラのリスト。</param>
		public static void RemoveHandlersFromRequerySuggested( List< WeakReference > handlers )
		{
			if( handlers != null )
			{
				foreach( WeakReference handlerRef in handlers )
				{
					EventHandler handler = handlerRef.Target as EventHandler;
					if( handler != null )
					{
						CommandManager.RequerySuggested -= handler;
					}
				}
			}
		}

		/// <summary>
		/// コマンドのイベント ハンドラについて、弱参照リストの登録を解除します。
		/// </summary>
		/// <param name="handlers">コマンドのイベント ハンドラのリスト。</param>
		/// <param name="handler">コマンドのイベント ハンドラ。</param>
		public static void RemoveWeakReferenceHandler( List< WeakReference > handlers, EventHandler handler )
		{
			if( handlers != null )
			{
				for( int i = handlers.Count - 1; i >= 0; --i )
				{
					WeakReference	reference       = handlers[ i ];
					EventHandler	existingHandler = reference.Target as EventHandler;
					
					if( ( existingHandler == null ) || ( existingHandler == handler ) )
					{
						// 削除されるハンドラに加えて、収集された古いハンドラを削除します
						handlers.RemoveAt( i );
					}
				}
			}
		}
	}
}
