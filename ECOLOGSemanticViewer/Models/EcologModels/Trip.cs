using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Data;
using ECOLOGSemanticViewer.Utils;
using System.Collections.ObjectModel;

namespace ECOLOGSemanticViewer.Models.EcologModels
{
    public class Trip : NotificationObject
    {

        #region TripId変更通知プロパティ
        private int _TripId;

        public int TripId
        {
            get
            { return _TripId; }
            set
            { 
                if (_TripId == value)
                    return;
                _TripId = value;
                RaisePropertyChanged();
            }
        }
        #endregion

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

        #region CarId変更通知プロパティ
        private int _CarId;

        public int CarId
        {
            get
            { return _CarId; }
            set
            { 
                if (_CarId == value)
                    return;
                _CarId = value;
                RaisePropertyChanged();
            }
        }
        #endregion

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

        #region StartTime変更通知プロパティ
        private DateTime _StartTime;

        public DateTime StartTime
        {
            get
            { return _StartTime; }
            set
            { 
                if (_StartTime == value)
                    return;
                _StartTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EndTime変更通知プロパティ
        private DateTime _EndTime;

        public DateTime EndTime
        {
            get
            { return _EndTime; }
            set
            { 
                if (_EndTime == value)
                    return;
                _EndTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region StartLatitude変更通知プロパティ
        private double _StartLatitude;

        public double StartLatitude
        {
            get
            { return _StartLatitude; }
            set
            { 
                if (_StartLatitude == value)
                    return;
                _StartLatitude = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region StartLongitude変更通知プロパティ
        private double _StartLongitude;

        public double StartLongitude
        {
            get
            { return _StartLongitude; }
            set
            { 
                if (_StartLongitude == value)
                    return;
                _StartLongitude = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EndLatitude変更通知プロパティ
        private double _EndLatitude;

        public double EndLatitude
        {
            get
            { return _EndLatitude; }
            set
            { 
                if (_EndLatitude == value)
                    return;
                _EndLatitude = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EndLongitude変更通知プロパティ
        private float _EndLongitude;

        public float EndLongitude
        {
            get
            { return _EndLongitude; }
            set
            { 
                if (_EndLongitude == value)
                    return;
                _EndLongitude = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ConsumedEnergy変更通知プロパティ
        private float _ConsumedEnergy;

        public float ConsumedEnergy
        {
            get
            { return _ConsumedEnergy; }
            set
            { 
                if (_ConsumedEnergy == value)
                    return;
                _ConsumedEnergy = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region TripDirection変更通知プロパティ
        private string _TripDirection;

        public string TripDirection
        {
            get
            { return _TripDirection; }
            set
            { 
                if (_TripDirection == value)
                    return;
                _TripDirection = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Validation変更通知プロパティ
        private string _Validation;

        public string Validation
        {
            get
            { return _Validation; }
            set
            { 
                if (_Validation == value)
                    return;
                _Validation = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public static ObservableCollection<Trip> SelectTrips()
        {
            DataTable tripsTable = new DataTable();
            string query = "SELECT trip_id, driver_id, car_id, sensor_id, start_time, end_time, consumed_energy, trip_direction FROM trips";

            tripsTable = DatabaseAccesserEcolog.GetResult(query);

            var trips = new ObservableCollection<Trip>();

            for (int i = 0; i < tripsTable.Rows.Count; i++)
            {
                trips.Add(new Trip
                {
                    TripId = (int)tripsTable.Rows[i]["trip_id"],
                    DriverId = (int)tripsTable.Rows[i]["driver_id"],
                    CarId = (int)tripsTable.Rows[i]["car_id"],
                    SensorId = (int)tripsTable.Rows[i]["sensor_id"],
                    StartTime = (DateTime)tripsTable.Rows[i]["start_time"],
                    EndTime = (DateTime)tripsTable.Rows[i]["end_time"],
                    ConsumedEnergy = (tripsTable.Rows[i]["consumed_energy"] == DBNull.Value ? 0 : (float)tripsTable.Rows[i]["consumed_energy"]),
                    TripDirection = (tripsTable.Rows[i]["trip_direction"] == DBNull.Value ? null : (string)tripsTable.Rows[i]["trip_direction"])
                });
            }

            return trips;
        }

        public static ObservableCollection<Trip> SelectTrips(int driverId, int carId, int sensorId, string direction)
        {
            DataTable tripsTable = new DataTable();
            string query = "SELECT trip_id, driver_id, car_id, sensor_id, start_time, end_time, consumed_energy, trip_direction FROM trips WHERE trip_id >= 0";

            if (driverId != 0)
                query = query + " AND driver_id = " + driverId;
            if (carId != 0)
                query = query + " AND car_id = " + carId;
            if (sensorId != 0)
                query = query + " AND sensor_Id = " + sensorId;
            if (direction != "All")
                query = query + " AND trip_direction = '" + direction + "'";

            tripsTable = DatabaseAccesserEcolog.GetResult(query);

            var trips = new ObservableCollection<Trip>();

            for (int i = 0; i < tripsTable.Rows.Count; i++)
            {
                trips.Add(new Trip
                {
                    TripId = (int)tripsTable.Rows[i]["trip_id"],
                    DriverId = (int)tripsTable.Rows[i]["driver_id"],
                    CarId = (int)tripsTable.Rows[i]["car_id"],
                    SensorId = (int)tripsTable.Rows[i]["sensor_id"],
                    StartTime = (DateTime)tripsTable.Rows[i]["start_time"],
                    EndTime = (DateTime)tripsTable.Rows[i]["end_time"],
                    ConsumedEnergy = (tripsTable.Rows[i]["consumed_energy"] == DBNull.Value ? 0 : (float)tripsTable.Rows[i]["consumed_energy"]),
                    TripDirection = (tripsTable.Rows[i]["trip_direction"] == DBNull.Value ? null : (string)tripsTable.Rows[i]["trip_direction"])
                });
            }

            return trips;
        }
    }
}
