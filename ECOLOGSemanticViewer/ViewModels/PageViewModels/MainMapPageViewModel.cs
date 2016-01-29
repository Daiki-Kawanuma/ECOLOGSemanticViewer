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
using ECOLOGSemanticViewer.Utils;
using System.Windows.Input;
using ECOLOGSemanticViewer.Commands;
using ECOLOGSemanticViewer.Models.MapModels;
using System.Collections.ObjectModel;
using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.Views.Items;
using MaterialDesignThemes.Wpf;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class MainMapPageViewModel : AbstMainPageViewModel
    {

        #region ExtractedSemanticLinks変更通知プロパティ
        private List<SemanticLink> _ExtractedSemanticLinks;

        public List<SemanticLink> ExtractedSemanticLinks
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

        public delegate void InvokeScript(string scriptName, params object[] args);

        public MapHost MapHost { get; private set; }

        public string Uri { get; set; }

        public InvokeScript invokeScript;

        #region INotifyPropertyChanged メンバ

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(params string[] names)
        {
            if (this.PropertyChanged != null)
            {
                foreach (string name in names)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }
        }

        #endregion

        public MainMapPageViewModel(List<SemanticLink> extractedSemanticLinks, TripDirection direction, InvokeScript script)
        {
            // TODO 戻す
            this.ExtractedSemanticLinks = extractedSemanticLinks;
            this.SelectedSemanticLinks = new ObservableCollection<SemanticLink>();
            this.TripDirection = direction;
            this.invokeScript = script;

            Initialize();
        }

        public void Initialize()
        {
            this.Uri = String.Format("file://{0}Resources\\index.html", AppDomain.CurrentDomain.BaseDirectory);

            this.MapHost = new MapHost() { 
                MainMapPageViewModel = this
            };

        }

        public void SetSemanticLine()
        {
            // test
            this.invokeScript("addLine", new object[] { 100, 35.681513, 139.765998, 35.691071, 139.699495 });

            foreach (SemanticLink semanticLink in this.ExtractedSemanticLinks)
            {
                for (int i = 0; i < semanticLink.Links.Count - 1; i++)
                {
                    this.invokeScript("addLine", 
                        new object[] { semanticLink.Links[i].Latitude, semanticLink.Links[i].Longitude, semanticLink.Links[i + 1].Latitude, semanticLink.Links[i + 1].Longitude });
                }
            }
        }

        public void ShowDialog(int semanticLinkId){

            SemanticLink semanticLink = this.ExtractedSemanticLinks.Where(v => v.SemanticLinkId == semanticLinkId).ElementAt(0);

            if (SelectedSemanticLinks.Count > 0)
            {
                showCompareDialog(semanticLink);
            }
            else
            {
                showDetailDialog(semanticLink);
            }  

        }

        private void showDetailDialog(SemanticLink semanticLink)
        {
            var dialog = new MainPageShowDetailDialog
            {
                Message = { Text = semanticLink.Semantics },
                TripDirection = this.TripDirection,
                SemanticLink = semanticLink,
                ViewModel = this
            };

            DialogHost.Show(dialog, "RootDialog");
        }

        private void showCompareDialog(SemanticLink semanticLink)
        {
            var dialog = new MainPageCompareDialog
            {
                Message = { Text = semanticLink.Semantics },
                TripDirection = this.TripDirection,
                SelectedSemanticLinks = this.SelectedSemanticLinks.ToList(),
                SemanticLink = semanticLink,
                ViewModel = this
            };

            DialogHost.Show(dialog, "RootDialog");
        }

    }
}
