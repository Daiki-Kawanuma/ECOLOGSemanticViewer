using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ECOLOGSemanticViewer.Models
{
    public class Month : NotificationObject
    {
        private static readonly string[] names = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};

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

        public static List<Month> CreateMonth()
        {
            var ret = new List<Month>();

            for (int i = 0; i < 12; i++)
            {
                ret.Add(new Month { Name = names[i], Number = i + 1 });
            }

            return ret;
        }
    }
}
