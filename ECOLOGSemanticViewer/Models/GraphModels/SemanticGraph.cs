using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using ECOLOGSemanticViewer.Models.EcologModels;
using OxyPlot.Series;

namespace ECOLOGSemanticViewer.Models.GraphModels
{
    public class SemanticGraph : NotificationObject
    {

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

        #region SeriesVisibility変更通知プロパティ
        private bool _SeriesVisibility;

        public bool SeriesVisibility
        {
            get
            { return _SeriesVisibility; }
            set
            { 
                if (_SeriesVisibility == value)
                    return;
                _SeriesVisibility = value;

                if (this.Series != null)
                {
                    Series.IsVisible = value;
                    Series.PlotModel.InvalidatePlot(true);
                }

                RaisePropertyChanged();
            }
        }
        #endregion

        #region Series変更通知プロパティ
        private Series _Series;

        public Series Series
        {
            get
            { return _Series; }
            set
            {
                if (_Series == value)
                    return;
                _Series = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
