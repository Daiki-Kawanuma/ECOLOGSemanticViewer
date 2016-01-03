﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ECOLOGSemanticViewer.Models
{
    public class Hour : NotificationObject
    {
        private static readonly string[] names = { "00:00～00:59", "01:00～01:59", "02:00～02:59", "03:00～03:59", "04:00～04:59", "05:00～05:59",
                                                 "06:00～06:59", "07:00～07:59", "08:00～08:59", "09:00～09:59", "10:00～10:59", "11:00～11:59",
                                                 "12:00～12:59", "13:00～13:59", "14:00～14:59", "15:00～15:59", "16:00～16:59", "17:00～17:59",
                                                 "18:00～18:59", "19:00～19:59", "20:00～20:59", "21:00～21:59", "22:00～22:59", "23:00～23:59"};

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

        public static List<Hour> CreateHour(){

            var ret = new List<Hour>();

            for (int i = 0; i < 24; i++)
            {
                ret.Add(new Hour { Name = names[i], Number = i });
            }

            return ret;
        }
    }
}
