using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ECOLOGSemanticViewer.Models
{
    public class Histogram : NotificationObject
    {

        #region Level変更通知プロパティ
        private double _Level;

        public double Level
        {
            get
            { return _Level; }
            set
            { 
                if (_Level == value)
                    return;
                _Level = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Value変更通知プロパティ
        private double _Value;

        public double Value
        {
            get
            { return _Value; }
            set
            {
                if (_Value == value)
                    return;
                _Value = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}