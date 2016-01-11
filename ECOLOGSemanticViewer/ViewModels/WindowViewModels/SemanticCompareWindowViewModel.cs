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
using System.Windows.Controls;
using ECOLOGSemanticViewer.Views.Pages;

namespace ECOLOGSemanticViewer.ViewModels.WindowViewModels
{
    public class SemanticCompareWindowViewModel : ViewModel
    {
        #region CurrentPage変更通知プロパティ
        private Page _CurrentPage;

        public Page CurrentPage
        {
            get
            { return _CurrentPage; }
            set
            {
                if (_CurrentPage == value)
                    return;
                _CurrentPage = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public SemanticCompareWindowViewModel()
        {
            this.CurrentPage = new CompareEnergyPage();
        }

        public void Initialize()
        {
        }
    }
}
