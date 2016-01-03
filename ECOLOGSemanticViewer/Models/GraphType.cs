using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ECOLOGSemanticViewer.Models
{
    public class GraphType : NotificationObject
    {
        public const int Histogram = 1;
        public const int Scattergram = 2;
        public const int ColumnGraph = 3;

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

        public static List<GraphType> CreateGraphTypes()
        {
            var ret = new List<GraphType>();

            ret.Add(new GraphType { Name = "Histogram", Number = Histogram });
            ret.Add(new GraphType { Name = "Scattergram", Number = Scattergram });
            ret.Add(new GraphType { Name = "Column graph", Number = ColumnGraph });

            return ret;
        }
    }
}
