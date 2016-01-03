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
    public class Car : NotificationObject
    {

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

        #region Model変更通知プロパティ
        private string _Model;

        public string Model
        {
            get
            { return _Model; }
            set
            {
                if (_Model == value)
                    return;
                _Model = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Battery変更通知プロパティ
        private float _Battery;

        public float Battery
        {
            get
            { return _Battery; }
            set
            {
                if (_Battery == value)
                    return;
                _Battery = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Weight変更通知プロパティ
        private float _Weight;

        public float Weight
        {
            get
            { return _Weight; }
            set
            {
                if (_Weight == value)
                    return;
                _Weight = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region TireRadius変更通知プロパティ
        private float _TireRadius;

        public float TireRadius
        {
            get
            { return _TireRadius; }
            set
            {
                if (_TireRadius == value)
                    return;
                _TireRadius = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ReductionRatio変更通知プロパティ
        private float _ReductionRatio;

        public float ReductionRatio
        {
            get
            { return _ReductionRatio; }
            set
            {
                if (_ReductionRatio == value)
                    return;
                _ReductionRatio = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region CdValue変更通知プロパティ
        private float _CdValue;

        public float CdValue
        {
            get
            { return _CdValue; }
            set
            {
                if (_CdValue == value)
                    return;
                _CdValue = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region FrontalProjectedArea変更通知プロパティ
        private float _FrontalProjectedArea;

        public float FrontalProjectedArea
        {
            get
            { return _FrontalProjectedArea; }
            set
            {
                if (_FrontalProjectedArea == value)
                    return;
                _FrontalProjectedArea = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public static List<Car> CreateCarsAddAll()
        {
            var ret = new List<Car>();

            DataTable carTable = new DataTable();
            string query = "SELECT * FROM cars";
            carTable = DatabaseAccesserEcolog.GetResult(query);

            ret.Add(new Car() { CarId = 0, Model = "All"});
            for (int i = 0; i < carTable.Rows.Count; i++)
            {
                ret.Add(new Car()
                    {
                        CarId = (int)carTable.Rows[i]["car_id"],
                        Model = (string)carTable.Rows[i]["model"],
                        Battery = (float)carTable.Rows[i]["battery"],
                        Weight = (float)carTable.Rows[i]["weight"],
                        TireRadius = (float)carTable.Rows[i]["tire_radius"],
                        ReductionRatio = (float)carTable.Rows[i]["reduction_ratio"],
                        CdValue = (float)carTable.Rows[i]["cd_value"],
                        FrontalProjectedArea = (float)carTable.Rows[i]["frontal_projected_area"]
                    });
            }

            return ret;
        }

        public static List<Car> CreateCars()
        {
            var ret = new List<Car>();

            DataTable carTable = new DataTable();
            string query = "SELECT * FROM cars";
            carTable = DatabaseAccesserEcolog.GetResult(query);

            for (int i = 0; i < carTable.Rows.Count; i++)
            {
                ret.Add(new Car()
                {
                    CarId = (int)carTable.Rows[i]["car_id"],
                    Model = (string)carTable.Rows[i]["model"],
                    Battery = (float)carTable.Rows[i]["battery"],
                    Weight = (float)carTable.Rows[i]["weight"],
                    TireRadius = (float)carTable.Rows[i]["tire_radius"],
                    ReductionRatio = (float)carTable.Rows[i]["reduction_ratio"],
                    CdValue = (float)carTable.Rows[i]["cd_value"],
                    FrontalProjectedArea = (float)carTable.Rows[i]["frontal_projected_area"]
                });
            }

            return ret;
        }
    }
}
