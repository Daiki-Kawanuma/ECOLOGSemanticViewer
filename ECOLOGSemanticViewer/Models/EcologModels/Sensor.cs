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
    public class Sensor : NotificationObject
    {


        #region SensorId変更通知プロパティ
        private int _SensorId;

        public int SensorId
        {
            get
            { return _SensorId; }
            set
            { 
                if (_SensorId == value)
                    return;
                _SensorId = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SensorModel変更通知プロパティ
        private string _SensorModel;

        public string SensorModel
        {
            get
            { return _SensorModel; }
            set
            { 
                if (_SensorModel == value)
                    return;
                _SensorModel = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Brand変更通知プロパティ
        private string _Brand;

        public string Brand
        {
            get
            { return _Brand; }
            set
            { 
                if (_Brand == value)
                    return;
                _Brand = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region OsVersion変更通知プロパティ
        private float _OsVersion;

        public float OsVersion
        {
            get
            { return _OsVersion; }
            set
            { 
                if (_OsVersion == value)
                    return;
                _OsVersion = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Ordinal変更通知プロパティ
        private int _Ordinal;

        public int Ordinal
        {
            get
            { return _Ordinal; }
            set
            {
                if (_Ordinal == value)
                    return;
                _Ordinal = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public static List<Sensor> CreateSensorsAddAll()
        {

            var ret = new List<Sensor>();

            DataTable sensorTable = new DataTable();
            string query = "SELECT * FROM sensors";
            sensorTable = DatabaseAccesserEcolog.GetResult(query);

            ret.Add(new Sensor { SensorId = 0, SensorModel = "All" });
            for (int i = 0; i < sensorTable.Rows.Count; i++)
            {
                ret.Add(new Sensor
                    {
                        SensorId = (int)sensorTable.Rows[i]["sensor_id"],
                        SensorModel = (string)sensorTable.Rows[i]["sensor_model"],
                        Brand = (sensorTable.Rows[i]["brand"] == DBNull.Value ? null : (string)sensorTable.Rows[i]["brand"]),
                        OsVersion = (sensorTable.Rows[i]["os_version"] == DBNull.Value ? 0 : (float)sensorTable.Rows[i]["os_version"]),
                        Ordinal = (sensorTable.Rows[i]["ordinal"] == DBNull.Value ? 0 : (int)sensorTable.Rows[i]["ordinal"])
                    });
            }
            
            return ret;
        }

        public static List<Sensor> CreateSensors()
        {

            var ret = new List<Sensor>();

            DataTable sensorTable = new DataTable();
            string query = "SELECT * FROM sensors";
            sensorTable = DatabaseAccesserEcolog.GetResult(query);

            for (int i = 0; i < sensorTable.Rows.Count; i++)
            {
                ret.Add(new Sensor
                {
                    SensorId = (int)sensorTable.Rows[i]["sensor_id"],
                    SensorModel = (string)sensorTable.Rows[i]["sensor_model"],
                    Brand = (sensorTable.Rows[i]["brand"] == DBNull.Value ? null : (string)sensorTable.Rows[i]["brand"]),
                    OsVersion = (sensorTable.Rows[i]["os_version"] == DBNull.Value ? 0 : (float)sensorTable.Rows[i]["os_version"]),
                    Ordinal = (sensorTable.Rows[i]["ordinal"] == DBNull.Value ? 0 : (int)sensorTable.Rows[i]["ordinal"])
                });
            }

            return ret;
        }
    }
}
