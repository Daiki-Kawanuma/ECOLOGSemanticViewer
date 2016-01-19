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
using System.Collections.ObjectModel;
using ECOLOGSemanticViewer.Models.EcologModels;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public abstract class AbstMainPageViewModel : ViewModel
    {

        #region SelectedSemanticLinks変更通知プロパティ
        private ObservableCollection<SemanticLink> _SelectedSemanticLinks;

        public ObservableCollection<SemanticLink> SelectedSemanticLinks
        {
            get
            { return _SelectedSemanticLinks; }
            set
            { 
                if (_SelectedSemanticLinks == value)
                    return;
                _SelectedSemanticLinks = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
