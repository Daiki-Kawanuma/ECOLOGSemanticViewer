using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ECOLOGSemanticViewer.Models
{
    public class ChargeDischarge : NotificationObject
    {

        #region Hour変更通知プロパティ
        private Hour _Hour;

        public Hour Hour
        {
            get
            { return _Hour; }
            set
            { 
                if (_Hour == value)
                    return;
                _Hour = value;
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

        public static List<ChargeDischarge> CreateChargeDischargeSeries()
        {
            List<Hour> hour = Hour.CreateHour();
            List<ChargeDischarge> ret = new List<ChargeDischarge>();

            for (int i = 0; i < hour.Count; i++)
            {
                ret.Add(new ChargeDischarge
                {
                    Hour = hour[i],
                    Value = 0
                });
            }

            return ret;
        }
    }
}
