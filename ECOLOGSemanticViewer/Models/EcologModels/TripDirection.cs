using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Collections.ObjectModel;

namespace ECOLOGSemanticViewer.Models.EcologModels
{
    public class TripDirection : NotificationObject
    {

        #region Direction変更通知プロパティ
        private string _Direction;

        public string Direction
        {
            get
            { return _Direction; }
            set
            { 
                if (_Direction == value)
                    return;
                _Direction = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public static List<TripDirection> CreateTripDirectionsAddAll()
        {

            var ret = new List<TripDirection>();

            ret.Add(new TripDirection() { Direction = "All" });

            ret.Add(new TripDirection() { Direction = "outward" });

            ret.Add(new TripDirection() { Direction = "homeward" });

            return ret;
        }

        public static List<TripDirection> CreateTripDirections()
        {

            var ret = new List<TripDirection>();

            ret.Add(new TripDirection() { Direction = "outward" });

            ret.Add(new TripDirection() { Direction = "homeward" });

            return ret;
        }
    }
}
