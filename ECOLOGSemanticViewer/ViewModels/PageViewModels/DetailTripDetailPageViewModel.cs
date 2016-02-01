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
using ECOLOGSemanticViewer.Models.GraphModels;
using System.Windows.Media.Imaging;

namespace ECOLOGSemanticViewer.ViewModels.PageViewModels
{
    public class DetailTripDetailPageViewModel : ViewModel
    {
        private static readonly int GraphIndexLength = 61;
        private static readonly int GraphIndexLengthHalf = 30;

        public SemanticLink SemanticLink { get; set; }
        public TripDirection TripDirection { get; set; }
        public String Uri { get; set; }
        public MapHostTripDetail MapHost { get; private set; }

        public InvokeScript invokeScript;

        public delegate void InvokeScript(string scriptName, params object[] args);

        public int TripID { get; set; }

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

                setCurrentIndexData();

                RaisePropertyChanged();
            }
        }
        #endregion

        #region SliderMaximum変更通知プロパティ
        private int _SliderMaximum;

        public int SliderMaximum
        {
            get
            { return _SliderMaximum; }
            set
            {
                if (_SliderMaximum == value)
                    return;
                _SliderMaximum = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CurrentEcolog変更通知プロパティ
        private GraphEcolog _CurrentEcolog;

        public GraphEcolog CurrentEcolog
        {
            get
            { return _CurrentEcolog; }
            set
            {
                if (_CurrentEcolog == value)
                    return;
                _CurrentEcolog = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CurrentImage変更通知プロパティ
        private BitmapImage _CurrentImage;

        public BitmapImage CurrentImage
        {
            get
            { return _CurrentImage; }
            set
            {
                if (_CurrentImage == value)
                    return;


                _CurrentImage = null;
                GC.Collect();

                _CurrentImage = value;

                RaisePropertyChanged();
            }
        }
        #endregion

        #region DisplayedGraphEcologs変更通知プロパティ
        private List<GraphEcolog> _DisplayedGraphEcologs;

        public List<GraphEcolog> DisplayedGraphEcologs
        {
            get
            { return _DisplayedGraphEcologs; }
            set
            {
                if (_DisplayedGraphEcologs == value)
                    return;
                _DisplayedGraphEcologs = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public List<GraphEcolog> GraphEcologs { get; set; }

        public List<PhotographicImage> PhotographicImages { get; set; }

        public DetailTripDetailPageViewModel()
        {
        }

        public DetailTripDetailPageViewModel(SemanticLink link, TripDirection direction, InvokeScript script)
        {
            this.SemanticLink = link;
            this.TripDirection = direction;
            this.invokeScript = script;

            Initialize();
        }

        public void Initialize()
        {
            this.Uri = String.Format("file://{0}Resources\\index.html", AppDomain.CurrentDomain.BaseDirectory);

            //await Task.Run(() =>
            //{
            this.TripID = getTripID();
            GraphEcologs = GraphEcolog.ExtractGraphEcolog(this.TripID, this.SemanticLink);
            this.CurrentImage = PhotographicImage.CreatePhotographicImage(this.TripID, this.GraphEcologs[0].Jst).ImageSource;
            //});

            this.SliderMaximum = this.GraphEcologs.Count - 1;
            this.CurrentIndex = 0;
            this.CurrentEcolog = this.GraphEcologs[0];

            DisplayedGraphEcologs = new List<GraphEcolog>();
            for (int i = 0; i < GraphIndexLength; i++)
            {
                DisplayedGraphEcologs.Add(GraphEcologs[i]);
            }

            this.MapHost = new MapHostTripDetail() { PageViewModel = this };
        }

        private int getTripID()
        {
            switch (SelectedComboBoxIndex)
            {
                case 0:
                    return SemanticHistogramDatum.GetEnergyMinTripID(this.SemanticLink, this.TripDirection);
                case 1:
                    return SemanticHistogramDatum.GetEnergyMedianTripID(this.SemanticLink, this.TripDirection);
                case 2:
                    return SemanticHistogramDatum.GetEnergyMaxTripID(this.SemanticLink, this.TripDirection);
                case 3:
                    return SemanticHistogramDatum.GetTimeMinTripID(this.SemanticLink, this.TripDirection);
                case 4:
                    return SemanticHistogramDatum.GetTimeMedianTripID(this.SemanticLink, this.TripDirection);
                case 5:
                    return SemanticHistogramDatum.GetTimeMedianTripID(this.SemanticLink, this.TripDirection);
                default:
                    return -1;
            }
        }

        public void SetCircle()
        {
            foreach (GraphEcolog graphEcolog in this.GraphEcologs)
            {
                this.invokeScript("addCircle", new object[] { graphEcolog.Latitude, graphEcolog.Longitude });
            }
        }

        private void setCurrentIndexData()
        {
            this.CurrentEcolog = this.GraphEcologs[this.CurrentIndex];

            this.CurrentImage = PhotographicImage.CreatePhotographicImage(this.TripID, this.CurrentEcolog.Jst).ImageSource;

            this.DisplayedGraphEcologs = setCurrentGraph(this.CurrentIndex);

            this.invokeScript("moveMap", new object[] { CurrentEcolog.Latitude, CurrentEcolog.Longitude });
            this.invokeScript("moveCurrentCircle", new object[] { CurrentEcolog.Latitude, CurrentEcolog.Longitude });
        }

        private List<GraphEcolog> setCurrentGraph(int index)
        {
            var ret = new List<GraphEcolog>();

            for (int i = 0; i < GraphIndexLength; i++)
            {
                if (index - GraphIndexLengthHalf + i >= 0 && index - GraphIndexLengthHalf + i <= SliderMaximum)
                {
                    ret.Add(GraphEcologs[index - GraphIndexLengthHalf + i]);
                }
                else
                {
                    ret.Add(new GraphEcolog());
                }
            }

            return ret;
        }
    }
}
