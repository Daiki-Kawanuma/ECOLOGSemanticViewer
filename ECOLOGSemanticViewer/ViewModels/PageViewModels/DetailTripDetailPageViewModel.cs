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

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class DetailTripDetailPageViewModel : ViewModel
    {
        // TODO 変更
        #region Uri変更通知プロパティ
        private string _Uri;

        public string Uri
        {
            get
            { return _Uri; }
            set
            {
                if (_Uri == value)
                    return;
                _Uri = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public DetailTripDetailPageViewModel()
        {
            this.Uri = String.Format("file://{0}Resources\\index.html", AppDomain.CurrentDomain.BaseDirectory);
        }

        public void Initialize()
        {

        }
    }
}
