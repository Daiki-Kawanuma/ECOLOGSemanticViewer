using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ECOLOGSemanticViewer.Commands
{
	/// <summary>
	/// このクラスは、パラメータとして指定されたメソッドへの、コマンド ロジックの委譲を実現します。
	/// また、View が要素ツリーに含まれないオブジェクトを対象とした、コマンドのバインドもサポートします。
	/// </summary>
	/// <typeparam name="T">コマンドに使用されるデータの型。</typeparam>
	public class DelegateCommand< T > : ICommand
	{
		#region コンストラクタ

		/// <summary>
		/// コマンドの実行ロジックを指定して、DelegateCommand クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="executeMethod">コマンドの実行ロジックとなるメソッド。</param>
		/// <exception cref="ArgumentNullException">executeMethod が null 参照です。</exception>
		public DelegateCommand( Action< T > executeMethod )
			: this( executeMethod, null, false )
		{
		}

		/// <summary>
		/// コマンドの実行ロジック、および実行検証ロジックを指定して、DelegateCommand クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="executeMethod">コマンドの実行ロジックとなるメソッド。</param>
		/// <param name="canExecuteMethod">コマンドの実行検証ロジックとなるメソッド。</param>
		/// <exception cref="ArgumentNullException">executeMethod が null 参照です。</exception>
		public DelegateCommand( Action< T > executeMethod, Func< T, bool > canExecuteMethod )
			: this( executeMethod, canExecuteMethod, false )
		{
		}

		/// <summary>
		/// コマンドの実行ロジック、実行検証ロジック、および自動再要求のフラグを指定して、DelegateCommand クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="executeMethod">コマンドの実行ロジックとなるメソッド。</param>
		/// <param name="canExecuteMethod">コマンドの実行検証ロジックとなるメソッド。</param>
		/// <param name="isAutomaticRequeryDisabled">自動再要求を行う場合は true。それ以外の場合は false。</param>
		/// <exception cref="ArgumentNullException">executeMethod が null 参照です。</exception>
		public DelegateCommand( Action< T > executeMethod, Func< T, bool > canExecuteMethod, bool isAutomaticRequeryDisabled )
		{
			if( executeMethod == null ) { throw new ArgumentNullException( "executeMethod" ); }

			this._executeMethod              = executeMethod;
			this._canExecuteMethod           = canExecuteMethod;
			this._isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
		}

		#endregion

		#region 公開メソッド、プロパティ

		/// <summary>
		/// 現在の状態でこの DelegateCommand を実行できるかどうかを判断します。
		/// </summary>
		/// <param name="parameter">コマンドで使用されたデータ。コマンドにデータを渡す必要がない場合は、このオブジェクトを null 参照に設定できます。</param>
		/// <returns>現在のコマンドの対象に対して、コマンドを実行できる場合は true。それ以外の場合は false。</returns>
		public bool CanExecute( T parameter )
		{
			return ( this._canExecuteMethod == null ? true : this._canExecuteMethod( parameter ) );
		}

		/// <summary>
		/// 現在のコマンドの対象で DelegateCommand を実行します。
		/// </summary>
		/// <param name="parameter">コマンドで使用されたデータ。コマンドにデータを渡す必要がない場合は、このオブジェクトを null 参照に設定できます。</param>
		public void Execute( T parameter )
		{
			this._executeMethod( parameter );
		}

		/// <summary>
		/// CommandManager からの、このコマンドに対する自動再要求の有効・無効を、取得または設定する。
		/// </summary>
		public bool IsAutomaticRequeryDisabled
		{
			get
			{
				return _isAutomaticRequeryDisabled;
			}
			set
			{
				if( _isAutomaticRequeryDisabled != value )
				{
					if( value )
					{
						CommandManagerHelper.RemoveHandlersFromRequerySuggested( _canExecuteChangedHandlers );
					}
					else
					{
						CommandManagerHelper.AddHandlersToRequerySuggested( _canExecuteChangedHandlers );
					}
					_isAutomaticRequeryDisabled = value;
				}
			}
		}

		/// <summary>
		/// CanExecuteChanged イベントを発生させます。
		/// </summary>
		public void RaiseCanExecuteChanged()
		{
			this.OnCanExecuteChanged();
		}

		/// <summary>
		/// コマンドの実行可能状態が変更された時のイベント。
		/// </summary>
		protected virtual void OnCanExecuteChanged()
		{
			CommandManagerHelper.CallWeakReferenceHandlers( this._canExecuteChangedHandlers );
		}

		#endregion

		#region ICommand Members

		/// <summary>
		/// コマンドを実行するかどうかに影響するような変更があった場合に発生するイベントのハンドラ。
		/// </summary>
		public event EventHandler CanExecuteChanged
		{
			add
			{
				if( !this._isAutomaticRequeryDisabled )
				{
					CommandManager.RequerySuggested += value;
				}

				CommandManagerHelper.AddWeakReferenceHandler( ref this._canExecuteChangedHandlers, value, 2 );
			}
			remove
			{
				if( !this._isAutomaticRequeryDisabled )
				{
					CommandManager.RequerySuggested -= value;
				}

				CommandManagerHelper.RemoveWeakReferenceHandler( this._canExecuteChangedHandlers, value );
			}
		}

		/// <summary>
		/// 現在の状態でこの DelegateCommand を実行できるかどうかを判断します。
		/// </summary>
		/// <param name="parameter">コマンドで使用されたデータ。コマンドにデータを渡す必要がない場合は、このオブジェクトを null 参照に設定できます。</param>
		/// <returns>現在のコマンドの対象に対して、コマンドを実行できる場合は true。それ以外の場合は false。</returns>
		bool ICommand.CanExecute( object parameter )
		{
			// T が値型でかつ、parameter がセットされていない場合、CanExecute デリゲートが存在するなら false を返します。
			// そうでなければtrueを返します。
			//
			if( parameter == null && typeof( T ).IsValueType )
			{
				return ( this._canExecuteMethod == null );
			}
			
			return this.CanExecute( ( T )parameter );
		}

		/// <summary>
		/// 現在のコマンドの対象で DelegateCommand を実行します。
		/// </summary>
		/// <param name="parameter">コマンドで使用されたデータ。コマンドにデータを渡す必要がない場合は、このオブジェクトを null 参照に設定できます。</param>
		void ICommand.Execute( object parameter )
		{
			this.Execute( ( T )parameter );
		}

		#endregion

		#region フィールド

		/// <summary>
		/// コマンドの実行ロジックとなるメソッド。
		/// </summary>
		private readonly Action< T > _executeMethod = null;

		/// <summary>
		/// コマンドの実行検証ロジックとなるメソッド。
		/// </summary>
		private readonly Func< T, bool > _canExecuteMethod = null;

		/// <summary>
		///  CommandManager からの、このコマンドに対する自動再要求が有効である事を示す値。
		/// </summary>
		private bool _isAutomaticRequeryDisabled = false;

		/// <summary>
		/// コマンドの実行可能状態が変更された時の、イベント ハンドラへの参照リスト。
		/// </summary>
		private List< WeakReference > _canExecuteChangedHandlers;

		#endregion
	
	}
}
