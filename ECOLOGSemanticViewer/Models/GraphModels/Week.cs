using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ECOLOGSemanticViewer.Models
{
    public class Week : NotificationObject
    {

        #region Name変更通知プロパティ
        private string _Name;

        public string Name
        {
            get
            { return _Name; }
            set
            { 
                if (_Name == value)
                    return;
                _Name = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Number変更通知プロパティ
        private int _Number;

        public int Number
        {
            get
            { return _Number; }
            set
            { 
                if (_Number == value)
                    return;
                _Number = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public static List<Week> CreateWeek()
        {
            var ret = new List<Week>();

            for (int i = 1; i <= 53; i++)
            {
                ret.Add(new Week { Name = "第" + i + "週", Number = i});
            }

            return ret;
        }
    }
}
