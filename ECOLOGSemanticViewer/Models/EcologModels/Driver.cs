using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Collections.ObjectModel;
using System.Data;
using ECOLOGSemanticViewer.Utils;

namespace ECOLOGSemanticViewer.Models.EcologModels
{
    public class Driver : NotificationObject
    {

        #region DriverId変更通知プロパティ
        private int _DriverId;

        public int DriverId
        {
            get
            { return _DriverId; }
            set
            {
                if (_DriverId == value)
                    return;
                _DriverId = value;
                RaisePropertyChanged();
            }
        }
        #endregion

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

        public static List<Driver> CreateDriversAddAll()
        {

            var ret = new List<Driver>();

            DataTable driverTable = new DataTable();
            string query = "SELECT * FROM drivers";
            driverTable = DatabaseAccesserEcolog.GetResult(query);

            ret.Add(new Driver { DriverId = 0, Name = "All" });
            for (int i = 0; i < driverTable.Rows.Count; i++)
            {
                ret.Add(
                    new Driver()
                    {
                        DriverId = (int)driverTable.Rows[i]["driver_id"],
                        Name = (string)driverTable.Rows[i]["name"]
                    }
                );
            }

            return ret;
        }

        public static List<Driver> CreateDrivers()
        {

            var ret = new List<Driver>();

            DataTable driverTable = new DataTable();
            string query = "SELECT * FROM drivers";
            driverTable = DatabaseAccesserEcolog.GetResult(query);

            for (int i = 0; i < driverTable.Rows.Count; i++)
            {
                ret.Add(
                    new Driver()
                    {
                        DriverId = (int)driverTable.Rows[i]["driver_id"],
                        Name = (string)driverTable.Rows[i]["name"]
                    }
                );
            }

            return ret;
        }
    }
}
