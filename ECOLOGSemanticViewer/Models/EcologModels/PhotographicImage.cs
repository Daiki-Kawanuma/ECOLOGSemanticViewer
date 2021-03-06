﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.ObjectModel;
using System.Data;
using ECOLOGSemanticViewer.Utils;

namespace ECOLOGSemanticViewer.Models
{
    public class PhotographicImage : NotificationObject
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

        #region Jst変更通知プロパティ
        private DateTime _Jst;

        public DateTime Jst
        {
            get
            { return _Jst; }
            set
            { 
                if (_Jst == value)
                    return;
                _Jst = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public Byte[] ImageSource;

        public static BitmapImage ByteToImageSource(byte[] byteAttay)
        {
            if (byteAttay.Length == 0)
                return null;
           
            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream memoryStream = new MemoryStream(byteAttay);
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();

            bitmapImage.Freeze();
            memoryStream.Dispose();

            return bitmapImage;
        }

        public static PhotographicImage CreatePhotographicImage(int tripId, DateTime jst)
        {

            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("  picture.driver_id,");
            query.AppendLine("  picture.car_id,");
            query.AppendLine("  picture.sensor_id,");
            query.AppendLine("  picture.jst,");
            query.AppendLine("  case when picture is null then LAG(picture) over (order by selected_ecolog.jst) else picture end as picture");

            query.AppendLine("FROM (");
            query.AppendLine("  SELECT ecolog.*");
            query.AppendLine("  FROM ecolog as ecolog");
            query.AppendLine("  WHERE ecolog.trip_id = " + tripId);
            query.AppendLine("  AND jst = '" + jst + "'");
            query.AppendLine("  ) AS selected_ecolog");

            query.AppendLine("LEFT JOIN corrected_picture AS picture");
            query.AppendLine("ON selected_ecolog.driver_id = picture.driver_id");
            query.AppendLine("  AND selected_ecolog.jst = picture.jst");
            query.AppendLine("  AND picture.sensor_id = 19");

            query.AppendLine("ORDER BY selected_ecolog.jst");

            DataTable pictureTable;
            pictureTable = DatabaseAccesserEcolog.GetResult(query.ToString());

            return new PhotographicImage()
                {
                    DriverId = (pictureTable.Rows[0]["driver_id"] == DBNull.Value ? -1 : (int)pictureTable.Rows[0]["driver_id"]),
                    CarId = (pictureTable.Rows[0]["car_id"] == DBNull.Value ? -1 : (int)pictureTable.Rows[0]["car_id"]),
                    SensorId = (pictureTable.Rows[0]["sensor_id"] == DBNull.Value ? -1 : (int)pictureTable.Rows[0]["sensor_id"]),
                    Jst = (pictureTable.Rows[0]["jst"] == DBNull.Value ? new DateTime() : (DateTime)pictureTable.Rows[0]["jst"]),
                    ImageSource = (pictureTable.Rows[0]["picture"] == DBNull.Value ? new Byte[0] : (Byte[])pictureTable.Rows[0]["picture"])
                };
        }
    }
}
