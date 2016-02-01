using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ECOLOGSemanticViewer.Models.EcologModels
{
    public class Link : NotificationObject
    {
        #region Num変更通知プロパティ
        private int _Num;

        public int Num
        {
            get
            { return _Num; }
            set
            { 
                if (_Num == value)
                    return;
                _Num = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region LinkId変更通知プロパティ
        private string _LinkId;

        public string LinkId
        {
            get
            { return _LinkId; }
            set
            { 
                if (_LinkId == value)
                    return;
                _LinkId = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Latitude変更通知プロパティ
        private double _Latitude;

        public double Latitude
        {
            get
            { return _Latitude; }
            set
            { 
                if (_Latitude == value)
                    return;
                _Latitude = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Longitude変更通知プロパティ
        private double _Longitude;

        public double Longitude
        {
            get
            { return _Longitude; }
            set
            { 
                if (_Longitude == value)
                    return;
                _Longitude = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region NodeId変更通知プロパティ
        private String _NodeId;

        public String NodeId
        {
            get
            { return _NodeId; }
            set
            {
                if (_NodeId == value)
                    return;
                _NodeId = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Direction変更通知プロパティ
        private int? _Direction;

        public int? Direction
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
    }
}
