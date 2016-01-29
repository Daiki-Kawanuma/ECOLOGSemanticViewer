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
using ECOLOGSemanticViewer.Models.MapModels;
using ECOLOGSemanticViewer.Models.EcologModels;
using System.Threading.Tasks;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class DetailTripDetailPageViewModel : ViewModel
    {
        public SemanticLink SemanticLink { get; set; }
        public TripDirection TripDirection { get; set; }
        public String Uri { get; set; }
        public MapHost MapHost { get; private set; }

        public InvokeScript invokeScript;

        public delegate void InvokeScript(string scriptName, params object[] args);

        #region SelectedComboBoxIndex変更通知プロパティ
        private int _SelectedComboBoxIndex;

        public int SelectedComboBoxIndex
        {
            get
            { return _SelectedComboBoxIndex; }
            set
            { 
                if (_SelectedComboBoxIndex == value)
                    return;
                _SelectedComboBoxIndex = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CurrentIndex変更通知プロパティ
        private int _CurrentIndex;

        public int CurrentIndex
        {
            get
            { return _CurrentIndex; }
            set
            { 
                if (_CurrentIndex == value)
                    return;
                _CurrentIndex = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public List<GraphEcolog> GraphEcologs { get; set; }

        public List<PhotographicImage> PhotographicImages { get; set; }

        public DetailTripDetailPageViewModel(SemanticLink link, TripDirection direction, InvokeScript script)
        {
            this.SemanticLink = link;
            this.TripDirection = direction;
            this.invokeScript = script;

            Initialize();
        }

        public async void Initialize()
        {
            this.Uri = String.Format("file://{0}Resources\\index.html", AppDomain.CurrentDomain.BaseDirectory);

            await Task.Run(() =>
            {
                this.EnergyHistogramDatum = SemanticHistogramDatum.GetEnergyInstance(this.SemanticLink, this.TripDirection);
            });
        }
    }
}
