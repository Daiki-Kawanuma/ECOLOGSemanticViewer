using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using ECOLOGSemanticViewer.ViewModels.PageViewModels;

namespace ECOLOGSemanticViewer.Models.MapModels
{
    /// <summary>
    /// Google Map の操作を行う JavaScript に関連付けられるクラスです。
    /// </summary>
    [ComVisible(true)]
    public class MapHostTripDetail : INotifyPropertyChanged
    {
        public DetailTripDetailPageViewModel PageViewModel { get; set; }

        public void OnInitCompleted()
        {
            this.PageViewModel.SetCircle();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
