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
using ECOLOGSemanticViewer.Views.Pages;
using System.Windows.Controls;
using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.ViewModels.PageViewModels;
using System.Diagnostics;

namespace ECOLOGSemanticViewer.ViewModels.WindowViewModels
{
    public class SemanticDetailWindowViewModel : ViewModel
    {

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

        #region SemanticLink変更通知プロパティ
        private SemanticLink _SemanticLink;

        public SemanticLink SemanticLink
        {
            get
            { return _SemanticLink; }
            set
            { 
                if (_SemanticLink == value)
                    return;
                _SemanticLink = value;
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

        public DetailEnergyPage EnergyPage;
        public DetailTimePage TimePage;
        public DetailHeatMapPage HeatMapPage;
        public DetailComparePage ComparePage;
        public DetailTripDetailPage TripDetailPage;

        public SemanticDetailWindowViewModel()
        {
        }

        public SemanticDetailWindowViewModel(SemanticLink link, TripDirection direction)
        {
            this.SemanticLink = link;
            this.TripDirection = direction;

            Initialize();
        }

        public void Initialize()
        {
            this.EnergyPage = new DetailEnergyPage();
            this.EnergyPage.DataContext = new DetailEnergyPageViewModel(this.SemanticLink, this.TripDirection);

            this.TimePage = new DetailTimePage();
            this.TimePage.DataContext = new DetailTimePageViewModel(this.SemanticLink, this.TripDirection);

            this.HeatMapPage = new DetailHeatMapPage();
            // TODO ここをMVVMに直す
            this.HeatMapPage.DataContext = new DetailHeatMapPageViewModel(this.SemanticLink, this.TripDirection, new List<Driver> { new Driver() { DriverId = 1 } }, new List<Car>() { new Car() { CarId = 1 }, new Car() { CarId = 3 } }, new List<Sensor>() { new Sensor() { SensorId = 12 } });

            this.ComparePage = new DetailComparePage();
            this.ComparePage.DataContext = new DetailComparePageViewModel(this.SemanticLink, this.TripDirection);

            this.TripDetailPage = new DetailTripDetailPage();
            
            this.CurrentPage = this.EnergyPage;
        }
    }
}
