using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ECOLOGSemanticViewer.Commands
{
	/// <summary>
	/// このクラスは、パラメータとして指定されたメソッドへの、コマンド ロジックの委譲を実現します。
	/// また、View が要素ツリーに含まれないオブジェクトを対象とした、コマンドのバインドもサポートします。
	/// </summary>
	public class DelegateCommand : ICommand
	{
		#region コンストラクタ

		/// <summary>
		/// コマンドの実行ロジックを指定して、DelegateCommand クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="executeMethod">コマンドの実行ロジックとなるメソッド。</param>
		/// <exception cref="ArgumentNullException">executeMethod が null 参照です。</exception>
		public DelegateCommand( Action executeMethod )
			: this( executeMethod, null, false )
		{
		}

		/// <summary>
		/// コマンドの実行ロジック、および実行検証ロジックを指定して、DelegateCommand クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="executeMethod">コマンドの実行ロジックとなるメソッド。</param>
		/// <param name="canExecuteMethod">コマンドの実行検証ロジックとなるメソッド。</param>
		/// <exception cref="ArgumentNullException">executeMethod が null 参照です。</exception>
		public DelegateCommand( Action executeMethod, Func< bool > canExecuteMethod )
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
		public DelegateCommand( Action executeMethod, Func< bool > canExecuteMethod, bool isAutomaticRequeryDisabled )
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
		/// <returns>現在のコマンドの対象に対して、コマンドを実行できる場合は true。それ以外の場合は false。</returns>
		public bool CanExecute()
		{
			return ( this._canExecuteMethod == null ? true : this._canExecuteMethod() ); 
		}

		/// <summary>
		/// 現在のコマンドの対象で DelegateCommand を実行します。
		/// </summary>
		public void Execute()
		{
			this._executeMethod();
		}

		/// <summary>
		/// CommandManager からの、このコマンドに対する自動再要求の有効・無効を、取得または設定する。
		/// </summary>
		public bool IsAutomaticRequeryDisabled
		{
			get
			{
				return this._isAutomaticRequeryDisabled;
			}
			set
			{
				if( this._isAutomaticRequeryDisabled != value )
				{
					if( value )
					{
						CommandManagerHelper.RemoveHandlersFromRequerySuggested( this._canExecuteChangedHandlers );
					}
					else
					{
						CommandManagerHelper.AddHandlersToRequerySuggested( this._canExecuteChangedHandlers );
					}
					
					this._isAutomaticRequeryDisabled = value;
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

		#region ICommand メンバ

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
			return this.CanExecute();
		}

		/// <summary>
		/// 現在のコマンドの対象で DelegateCommand を実行します。
		/// </summary>
		/// <param name="parameter">コマンドで使用されたデータ。コマンドにデータを渡す必要がない場合は、このオブジェクトを null 参照に設定できます。</param>
		void ICommand.Execute( object parameter )
		{
			this.Execute();
		}

		#endregion

		#region フィールド

		/// <summary>
		/// コマンドの実行ロジックとなるメソッド。
		/// </summary>
		private readonly Action _executeMethod = null;

		/// <summary>
		/// コマンドの実行検証ロジックとなるメソッド。
		/// </summary>
		private readonly Func< bool > _canExecuteMethod = null;
		
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