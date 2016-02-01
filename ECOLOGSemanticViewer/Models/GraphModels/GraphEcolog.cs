using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Data;
using ECOLOGSemanticViewer.Utils;
using ECOLOGSemanticViewer.Models.EcologModels;

namespace ECOLOGSemanticViewer.Models
{
    public class GraphEcolog : NotificationObject
    {
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

        #region Speed変更通知プロパティ
        private float _Speed;

        public float Speed
        {
            get
            { return _Speed; }
            set
            {
                if (_Speed == value)
                    return;
                _Speed = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region TerrainAltitude変更通知プロパティ
        private float _TerrainAltitude;

        public float TerrainAltitude
        {
            get
            { return _TerrainAltitude; }
            set
            {
                if (_TerrainAltitude == value)
                    return;
                _TerrainAltitude = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region LongitudinalAcc変更通知プロパティ
        private float _LongitudinalAcc;

        public float LongitudinalAcc
        {
            get
            { return _LongitudinalAcc; }
            set
            {
                if (_LongitudinalAcc == value)
                    return;
                _LongitudinalAcc = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EnergyByAirResistancePlus変更通知プロパティ
        private float _EnergyByAirResistancePlus;

        public float EnergyByAirResistancePlus
        {
            get
            { return _EnergyByAirResistancePlus; }
            set
            {
                if (_EnergyByAirResistancePlus == value)
                    return;
                _EnergyByAirResistancePlus = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EnergyByAirResistanceMinus変更通知プロパティ
        private float _EnergyByAirResistanceMinus;

        public float EnergyByAirResistanceMinus
        {
            get
            { return _EnergyByAirResistanceMinus; }
            set
            { 
                if (_EnergyByAirResistanceMinus == value)
                    return;
                _EnergyByAirResistanceMinus = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EnergyByRollingResistancePlus変更通知プロパティ
        private float _EnergyByRollingResistancePlus;

        public float EnergyByRollingResistancePlus
        {
            get
            { return _EnergyByRollingResistancePlus; }
            set
            {
                if (_EnergyByRollingResistancePlus == value)
                    return;
                _EnergyByRollingResistancePlus = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EnergyByRollingResistanceMinus変更通知プロパティ
        private float _EnergyByRollingResistanceMinus;

        public float EnergyByRollingResistanceMinus
        {
            get
            { return _EnergyByRollingResistanceMinus; }
            set
            { 
                if (_EnergyByRollingResistanceMinus == value)
                    return;
                _EnergyByRollingResistanceMinus = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EnergyByClimbingResistance変更通知プロパティ
        private float _EnergyByClimbingResistance;

        public float EnergyByClimbingResistance
        {
            get
            { return _EnergyByClimbingResistance; }
            set
            {
                if (_EnergyByClimbingResistance == value)
                    return;
                _EnergyByClimbingResistance = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region EnergyByAccResistance変更通知プロパティ
        private float _EnergyByAccResistance;

        public float EnergyByAccResistance
        {
            get
            { return _EnergyByAccResistance; }
            set
            {
                if (_EnergyByAccResistance == value)
                    return;
                _EnergyByAccResistance = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ConvertLoss変更通知プロパティ
        private float _ConvertLoss;

        public float ConvertLoss
        {
            get
            { return _ConvertLoss; }
            set
            {
                if (_ConvertLoss == value)
                    return;
                _ConvertLoss = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region RegeneLoss変更通知プロパティ
        private float _RegeneLoss;

        public float RegeneLoss
        {
            get
            { return _RegeneLoss; }
            set
            {
                if (_RegeneLoss == value)
                    return;
                _RegeneLoss = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region RegeneEnergy変更通知プロパティ
        private float _RegeneEnergy;

        public float RegeneEnergy
        {
            get
            { return _RegeneEnergy; }
            set
            {
                if (_RegeneEnergy == value)
                    return;
                _RegeneEnergy = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region LostEnergy変更通知プロパティ
        private float _LostEnergy;

        public float LostEnergy
        {
            get
            { return _LostEnergy; }
            set
            {
                if (_LostEnergy == value)
                    return;
                _LostEnergy = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ConsumedElectricEnergy変更通知プロパティ
        private float _ConsumedElectricEnergy;

        public float ConsumedElectricEnergy
        {
            get
            { return _ConsumedElectricEnergy; }
            set
            {
                if (_ConsumedElectricEnergy == value)
                    return;
                _ConsumedElectricEnergy = value;
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

        public static List<GraphEcolog> ExtractGraphEcolog(int tripId)
        {
            var ret = new List<GraphEcolog>();

            DataTable ecologTable = new DataTable();

            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT");
            query.AppendLine("  jst,");
            query.AppendLine("  latitude,");
            query.AppendLine("  longitude,");
            query.AppendLine("  speed,");
            query.AppendLine("  terrain_altitude,");
            query.AppendLine("  longitudinal_acc,");
            query.AppendLine("  CASE WHEN consumed_electric_energy > 0 THEN energy_by_air_resistance * 3600 ELSE 0 END AS energy_by_air_resistance_plus,");
            query.AppendLine("  CASE WHEN consumed_electric_energy <= 0 THEN energy_by_air_resistance * 3600 ELSE 0 END AS energy_by_air_resistance_minus,");
            query.AppendLine("  CASE WHEN consumed_electric_energy > 0 THEN energy_by_rolling_resistance * 3600 ELSE 0 END AS energy_by_rolling_resistance_plus,");
            query.AppendLine("  CASE WHEN consumed_electric_energy <= 0 THEN energy_by_rolling_resistance * 3600 ELSE 0 END AS energy_by_rolling_resistance_minus,");
            query.AppendLine("  CASE WHEN energy_by_climbing_resistance > 0 THEN energy_by_climbing_resistance * 3600 ELSE 0 END AS energy_by_climbing_resistance,");
            query.AppendLine("  CASE WHEN consumed_electric_energy > 0 THEN energy_by_acc_resistance * 3600 ELSE 0 END AS energy_by_acc_resistance,");
            query.AppendLine("  convert_loss * 3600 AS convert_loss,");
            query.AppendLine("  CASE WHEN consumed_electric_energy <= 0 THEN regene_loss * 3600 ELSE 0 END AS regene_loss,");
            query.AppendLine("  CASE WHEN consumed_electric_energy <= 0 THEN regene_energy * 3600 ELSE 0 END AS regene_energy,");
            query.AppendLine("  lost_energy * 3600 AS lost_energy,");
            query.AppendLine("  consumed_electric_energy * 3600 AS consumed_electric_energy,");
            query.AppendLine("  link_id");
            query.AppendLine("FROM ecolog");
            query.AppendLine("WHERE trip_id = " + tripId);
            query.AppendLine("ORDER BY jst ASC");

            ecologTable = DatabaseAccesserEcolog.GetResult(query.ToString());

            for (int i = 0; i < ecologTable.Rows.Count; i++)
            {
                ret.Add(new GraphEcolog()
                {
                    Jst = (DateTime)ecologTable.Rows[i]["jst"],
                    Latitude = (double)ecologTable.Rows[i]["latitude"],
                    Longitude = (double)ecologTable.Rows[i]["longitude"],
                    Speed = (float)ecologTable.Rows[i]["speed"],
                    TerrainAltitude = (float)ecologTable.Rows[i]["terrain_altitude"],
                    LongitudinalAcc = (ecologTable.Rows[i]["longitudinal_acc"] == DBNull.Value ? -1 : (float)ecologTable.Rows[i]["longitudinal_acc"]),
                    EnergyByAirResistancePlus = (float)ecologTable.Rows[i]["energy_by_air_resistance_plus"],
                    EnergyByAirResistanceMinus = (float)ecologTable.Rows[i]["energy_by_air_resistance_minus"],
                    EnergyByRollingResistancePlus = (float)ecologTable.Rows[i]["energy_by_rolling_resistance_plus"],
                    EnergyByRollingResistanceMinus = (float)ecologTable.Rows[i]["energy_by_rolling_resistance_minus"],
                    EnergyByClimbingResistance = (float)ecologTable.Rows[i]["energy_by_climbing_resistance"],
                    EnergyByAccResistance = (ecologTable.Rows[i]["energy_by_acc_resistance"] == DBNull.Value ? -1 : (float)ecologTable.Rows[i]["energy_by_acc_resistance"]),
                    ConvertLoss = (float)ecologTable.Rows[i]["convert_loss"],
                    RegeneLoss = (float)ecologTable.Rows[i]["regene_loss"],
                    RegeneEnergy = (float)ecologTable.Rows[i]["regene_energy"],
                    LostEnergy = (float)ecologTable.Rows[i]["lost_energy"],
                    ConsumedElectricEnergy = (float)ecologTable.Rows[i]["consumed_electric_energy"],
                    LinkId = (ecologTable.Rows[i]["link_id"] == DBNull.Value ? null : (string)ecologTable.Rows[i]["link_id"]),
                });
            }

            return ret;
        }

        public static List<GraphEcolog> ExtractGraphEcolog(int tripId, SemanticLink link)
        {
            var ret = new List<GraphEcolog>();

            DataTable ecologTable = new DataTable();

            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT");
            query.AppendLine("  jst,");
            query.AppendLine("  latitude,");
            query.AppendLine("  longitude,");
            query.AppendLine("  speed,");
            query.AppendLine("  terrain_altitude,");
            query.AppendLine("  longitudinal_acc,");
            query.AppendLine("  CASE WHEN consumed_electric_energy > 0 THEN energy_by_air_resistance * 3600 ELSE 0 END AS energy_by_air_resistance_plus,");
            query.AppendLine("  CASE WHEN consumed_electric_energy <= 0 THEN energy_by_air_resistance * 3600 ELSE 0 END AS energy_by_air_resistance_minus,");
            query.AppendLine("  CASE WHEN consumed_electric_energy > 0 THEN energy_by_rolling_resistance * 3600 ELSE 0 END AS energy_by_rolling_resistance_plus,");
            query.AppendLine("  CASE WHEN consumed_electric_energy <= 0 THEN energy_by_rolling_resistance * 3600 ELSE 0 END AS energy_by_rolling_resistance_minus,");
            query.AppendLine("  CASE WHEN energy_by_climbing_resistance > 0 THEN energy_by_climbing_resistance * 3600 ELSE 0 END AS energy_by_climbing_resistance,");
            query.AppendLine("  CASE WHEN consumed_electric_energy > 0 THEN energy_by_acc_resistance * 3600 ELSE 0 END AS energy_by_acc_resistance,");
            query.AppendLine("  convert_loss * 3600 AS convert_loss,");
            query.AppendLine("  CASE WHEN consumed_electric_energy <= 0 THEN regene_loss * 3600 ELSE 0 END AS regene_loss,");
            query.AppendLine("  CASE WHEN consumed_electric_energy <= 0 THEN regene_energy * 3600 ELSE 0 END AS regene_energy,");
            query.AppendLine("  lost_energy * 3600 AS lost_energy,");
            query.AppendLine("  consumed_electric_energy * 3600 AS consumed_electric_energy,");
            query.AppendLine("  ecolog.link_id");
            query.AppendLine("FROM ecolog");
            query.AppendLine("INNER JOIN semantic_links");
            query.AppendLine("  ON ecolog.link_id = semantic_links.link_id");
            query.AppendLine("WHERE trip_id = " + tripId);
            query.AppendLine("  AND semantic_link_id = " + link.SemanticLinkId);
            query.AppendLine("ORDER BY jst ASC");

            Console.WriteLine(query.ToString());

            ecologTable = DatabaseAccesserEcolog.GetResult(query.ToString());

            for (int i = 0; i < ecologTable.Rows.Count; i++)
            {
                ret.Add(new GraphEcolog()
                {
                    Jst = (DateTime)ecologTable.Rows[i]["jst"],
                    Latitude = (double)ecologTable.Rows[i]["latitude"],
                    Longitude = (double)ecologTable.Rows[i]["longitude"],
                    Speed = (float)ecologTable.Rows[i]["speed"],
                    TerrainAltitude = (float)ecologTable.Rows[i]["terrain_altitude"],
                    LongitudinalAcc = (ecologTable.Rows[i]["longitudinal_acc"] == DBNull.Value ? -1 : (float)ecologTable.Rows[i]["longitudinal_acc"]),
                    EnergyByAirResistancePlus = (float)ecologTable.Rows[i]["energy_by_air_resistance_plus"],
                    EnergyByAirResistanceMinus = (float)ecologTable.Rows[i]["energy_by_air_resistance_minus"],
                    EnergyByRollingResistancePlus = (float)ecologTable.Rows[i]["energy_by_rolling_resistance_plus"],
                    EnergyByRollingResistanceMinus = (float)ecologTable.Rows[i]["energy_by_rolling_resistance_minus"],
                    EnergyByClimbingResistance = (float)ecologTable.Rows[i]["energy_by_climbing_resistance"],
                    EnergyByAccResistance = (ecologTable.Rows[i]["energy_by_acc_resistance"] == DBNull.Value ? -1 : (float)ecologTable.Rows[i]["energy_by_acc_resistance"]),
                    ConvertLoss = (float)ecologTable.Rows[i]["convert_loss"],
                    RegeneLoss = (float)ecologTable.Rows[i]["regene_loss"],
                    RegeneEnergy = (float)ecologTable.Rows[i]["regene_energy"],
                    LostEnergy = (float)ecologTable.Rows[i]["lost_energy"],
                    ConsumedElectricEnergy = (float)ecologTable.Rows[i]["consumed_electric_energy"],
                    LinkId = (ecologTable.Rows[i]["link_id"] == DBNull.Value ? null : (string)ecologTable.Rows[i]["link_id"]),
                });
            }

            return ret;
        }
    }
}
