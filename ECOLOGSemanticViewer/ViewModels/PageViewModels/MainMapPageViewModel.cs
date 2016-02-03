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
using System.Threading.Tasks;

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
            this.ExtractedSemanticLinks = extractedSemanticLinks;
            this.SelectedSemanticLinks = new ObservableCollection<SemanticLink>();
            this.TripDirection = direction;
            this.invokeScript = script;

            Initialize();
        }

        public void Initialize()
        {
            //await Task.Run(() =>
            //{
            SetLinks();
            //});

            this.Uri = String.Format("file://{0}Resources\\index.html", AppDomain.CurrentDomain.BaseDirectory);

            this.MapHost = new MapHost()
            {
                MainMapPageViewModel = this
            };
        }

        public void SetLinks()
        {
            foreach (SemanticLink semanticLink in this.ExtractedSemanticLinks)
            {
                semanticLink.SetLinks();
            }
        }

        public void SetSemanticLine()
        {
            foreach (SemanticLink semanticLink in this.ExtractedSemanticLinks)
            {
                for (int i = 0; i < semanticLink.Links.Count - 1; i++)
                {
                    this.invokeScript("addCircle",
                        new object[] { semanticLink.Links[i].Latitude, semanticLink.Links[i].Longitude });

                    if (semanticLink.Links[i].LinkId.Equals(semanticLink.Links[i + 1].LinkId))

                        this.invokeScript("addLine",
                            new object[] { semanticLink.SemanticLinkId, 
                            semanticLink.Links[i].Latitude, 
                            semanticLink.Links[i].Longitude, 
                            semanticLink.Links[i + 1].Latitude, 
                            semanticLink.Links[i + 1].Longitude });
                }
            }

            Console.WriteLine("LAT:" + this.ExtractedSemanticLinks.Average(v => v.Links.Average(l => l.Latitude)));
            Console.WriteLine("LONG: " + this.ExtractedSemanticLinks.Average(v => v.Links.Average(l => l.Longitude)));

            this.invokeScript("moveMap",
                new object[] { 
                    this.ExtractedSemanticLinks.Average(v => v.Links.Average( l => l.Latitude)),
                    this.ExtractedSemanticLinks.Average(v => v.Links.Average( l => l.Longitude))
                });
        }

        public void ShowDialog(int semanticLinkId)
        {

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
