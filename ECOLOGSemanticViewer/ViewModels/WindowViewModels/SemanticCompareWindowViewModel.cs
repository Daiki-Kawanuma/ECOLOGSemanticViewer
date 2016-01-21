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
using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.ViewModels.PageViewModels;

namespace ECOLOGSemanticViewer.ViewModels.WindowViewModels
{
    public class SemanticCompareWindowViewModel : ViewModel
    {
        #region SelectedSemanticLinks変更通知プロパティ
        private List<SemanticLink> _SelectedSemanticLinks;

        public List<SemanticLink> SelectedSemanticLinks
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

        #region TripDirection変更通知プロパティ
        private TripDirection _TripDirection;

        public TripDirection TripDirection
        {
            get
            { return _TripDirection; }
            set
            { 
                if (_TripDirection == value)
                    return;
                _TripDirection = value;
                RaisePropertyChanged();
            }
        }
        #endregion

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

        public SemanticCompareWindowViewModel(List<SemanticLink> selectedSemanticLink, TripDirection direction)
        {
            this.SelectedSemanticLinks = selectedSemanticLink;
            this.TripDirection = direction;

            CompareEnergyPage page = new CompareEnergyPage();
            page.DataContext = new CompareEnergyPageViewModel(SelectedSemanticLinks, TripDirection);

            this.CurrentPage = page;
        }

        public void Initialize()
        {

        }
    }
}
