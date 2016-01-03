﻿using System;
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
using ECOLOGSemanticViewer.Views;
using System.Collections.ObjectModel;
using ECOLOGSemanticViewer.Views.Pages;
using ECOLOGSemanticViewer.Models.EcologModels;

namespace ECOLOGSemanticViewer.ViewModels.WindowViewModels
{
    public class MainWindowViewModel : ViewModel
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

        public MainWindowViewModel()
        {
            this.CurrentPage = new MainMapPage();
            testGenerator();
        }

        public void Initialize()
        {
        }

        private void testGenerator(){
            
            this.SemanticLinks = new List<SemanticLink>
            {
                new SemanticLink(){SemanticLinkId = 1, Semantics = "Semantics 1"},
                new SemanticLink(){SemanticLinkId = 2, Semantics = "Semantics 2"},
                new SemanticLink(){SemanticLinkId = 3, Semantics = "Semantics 3"},
                new SemanticLink(){SemanticLinkId = 4, Semantics = "Semantics 4"}
            };

            this.ExtractedSemanticLinks = new ObservableCollection<SemanticLink>();
        }

        #region SemanticLinks変更通知プロパティ
        private List<SemanticLink> _SemanticLinks;

        public List<SemanticLink> SemanticLinks
        {
            get
            { return _SemanticLinks; }
            set
            { 
                if (_SemanticLinks == value)
                    return;
                _SemanticLinks = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SelectedSemanticLink変更通知プロパティ
        private SemanticLink _SelectedSemanticLink;

        public SemanticLink SelectedSemanticLink
        {
            get
            { return _SelectedSemanticLink; }
            set
            { 
                if (_SelectedSemanticLink == value)
                    return;
                _SelectedSemanticLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ExtractedSeamanticLinks変更通知プロパティ
        private ObservableCollection<SemanticLink> _ExtractedSemanticLinks;

        public ObservableCollection<SemanticLink> ExtractedSemanticLinks
        {
            get
            { return _ExtractedSemanticLinks; }
            set
            { 
                if (_ExtractedSemanticLinks == value)
                    return;
                _ExtractedSemanticLinks = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
