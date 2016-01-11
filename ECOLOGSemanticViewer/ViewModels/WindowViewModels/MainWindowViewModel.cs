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
using ECOLOGSemanticViewer.Views;
using System.Collections.ObjectModel;
using ECOLOGSemanticViewer.Views.Pages;
using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.ViewModels.PageViewModels;

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
            var context = this.CurrentPage.DataContext as MainMapPageViewModel;
            context.SelectedSemanticLinks = this.ExtractedSemanticLinks;

            this.SemanticLinks = SemanticLink.GetDefaultOutwardSemanticLinks();
            this.ExtractedSemanticLinks = new ObservableCollection<SemanticLink>(SemanticLink.GetDefaultOutwardSemanticLinks());

            
            //TODO clear this function
            // testGenerator();
        }

        public void Initialize()
        {
        }

        // TODO clear this function
        private void testGenerator(){
            
            this.SemanticLinks = new List<SemanticLink>
            {
                new SemanticLink(){SemanticLinkId = 187, Semantics = "自宅～綾瀬市役所前"},
                new SemanticLink(){SemanticLinkId = 188, Semantics = "綾瀬市役所前～与蔵山下"},
                new SemanticLink(){SemanticLinkId = 189, Semantics = "与蔵山下～代官二丁目"},
                new SemanticLink(){SemanticLinkId = 190, Semantics = "代官二丁目～福田入口"},
                new SemanticLink(){SemanticLinkId = 191, Semantics = "福田入口～下和田"},
                new SemanticLink(){SemanticLinkId = 191, Semantics = "下和田～いちょう小学校入口"},
            };

            this.ExtractedSemanticLinks = new ObservableCollection<SemanticLink>();
            foreach(SemanticLink link in this.SemanticLinks){
                this.ExtractedSemanticLinks.Add(link);
            }

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
