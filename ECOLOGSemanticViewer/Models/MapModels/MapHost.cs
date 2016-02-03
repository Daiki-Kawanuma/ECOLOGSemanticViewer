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
	[ComVisible( true )]
	public class MapHost : INotifyPropertyChanged
	{
        public MainMapPageViewModel MainMapPageViewModel { get; set; }

        public void OnInitCompleted()
        {
            //this.MainMapPageViewModel.SetSemanticLine();
        }

        public void OnLineClicked(int semanticLinkID)
        {
            this.MainMapPageViewModel.ShowDialog(semanticLinkID);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
