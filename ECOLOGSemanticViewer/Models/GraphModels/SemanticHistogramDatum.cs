using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.Utils;
using Livet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOLOGSemanticViewer.Models.GraphModels
{
    public class SemanticHistogramDatum : NotificationObject
    {
        public SemanticLink SemanticLink { get; set; }

        public TripDirection Direction { get; set; }

        public int Number { get; set; }

        public double Sum { get; set; }

        public List<LevelAndValue> HistogramData { get; set; }

        public double MaxLevel { get; set; }

        public double MinLevel { get; set; }

        public double MedianLevel { get; set; }

        public double ModeLevel { get; set; }

        public double AvgLevel { get; set; }

        public double ClassWidth { get; set; }

        public List<LevelAndValue> UnderModeData { get; set; }

        public List<LevelAndValue> ModeData { get; set; }

        public List<LevelAndValue> UpperModeData { get; set; }

        public double DistUnderMode { get; set; }

        public double DistMode { get; set; }

        public double DistUpperMode { get; set; }

        public double CompMinMax { get; set; }

        public double CompMinMode { get; set; }

        public double CompModeMax { get; set; }

        private SemanticHistogramDatum()
        {

        }

        public static int GetEnergyMinTripID(SemanticLink semanticLink, TripDirection direction)
        {
            return DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMinOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("TripID"))
                .ElementAt(0);
        }

        public static int GetEnergyMedianTripID(SemanticLink semanticLink, TripDirection direction)
        {
            return DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMedianOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("TripID"))
                .ElementAt(0);
        }

        public static int GetEnergyMaxTripID(SemanticLink semanticLink, TripDirection direction)
        {
            return DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMaxOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("TripID"))
                .ElementAt(0);
        }

        public static int GetTimeMinTripID(SemanticLink semanticLink, TripDirection direction)
        {
            return DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMinOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("TripID"))
                .ElementAt(0);
        }

        public static int GetTimeMedianTripID(SemanticLink semanticLink, TripDirection direction)
        {
            return DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMedianOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("TripID"))
                .ElementAt(0);
        }

        public static int GetTimeMaxTripID(SemanticLink semanticLink, TripDirection direction)
        {
            return DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMaxOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("TripID"))
                .ElementAt(0);
        }

        public static SemanticHistogramDatum GetEnergyInstance(SemanticLink semanticLink, TripDirection direction)
        {
            SemanticHistogramDatum datum = new SemanticHistogramDatum();

            datum.SemanticLink = semanticLink;
            datum.Direction = direction;

            
            DataTable table = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyHistogramOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')");
            datum.HistogramData = new List<LevelAndValue>();
            foreach(DataRow row in table.Rows){
                datum.HistogramData.Add(new LevelAndValue() { Level = row.Field<double>("Level"), Value = row.Field<int>("Number") });
            }

            datum.MaxLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMaxOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Max"))
                .ElementAt(0);

            datum.MinLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMinOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Min"))
                .ElementAt(0);

            datum.MedianLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMedianOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Median"))
                .ElementAt(0);

            datum.AvgLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyAvgOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Avg"))
                .ElementAt(0);

            datum.ModeLevel = datum.HistogramData.First(v => v.Value.Equals(datum.HistogramData.Select(m => m.Value).Max())).Level;

            datum.HistogramData.Max(x => x.Level);

            datum.ClassWidth = (datum.MaxLevel - datum.MinLevel) / 10;

            datum.UnderModeData = datum.HistogramData
                .Where(v => v.Value <= datum.HistogramData.Max(x => x.Value) * 0.75)
                .Where(v => v.Level < datum.ModeLevel)
                .ToList();

            datum.ModeData = datum.HistogramData
                .Where(v => v.Value >= datum.HistogramData.Max(x => x.Value) * 0.75)
                .ToList();

            datum.UpperModeData = datum.HistogramData
                .Where(v => v.Value <= datum.HistogramData.Max(x => x.Value) * 0.75)
                .Where(v => v.Level > datum.ModeLevel)
                .ToList();

            datum.DistUnderMode = datum.UnderModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);
            datum.DistMode = datum.ModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);
            datum.DistUpperMode = datum.UpperModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);

            datum.CompMinMax = datum.MaxLevel * 100 / datum.MinLevel;
            datum.CompMinMode = datum.ModeLevel * 100 / datum.MinLevel;
            datum.CompModeMax = datum.MaxLevel * 100 / datum.ModeLevel;

            return datum;
        }

        public static SemanticHistogramDatum GetTimeInstance(SemanticLink semanticLink, TripDirection direction)
        {
            SemanticHistogramDatum datum = new SemanticHistogramDatum();

            datum.SemanticLink = semanticLink;
            datum.Direction = direction;

            DataTable table = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeHistogramOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')");
            datum.HistogramData = new List<LevelAndValue>();
            foreach (DataRow row in table.Rows)
            {
                datum.HistogramData.Add(new LevelAndValue() { Level = row.Field<int>("Level"), Value = row.Field<int>("Number") });
            }

            datum.MaxLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMaxOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Max"))
                .ElementAt(0);

            datum.MinLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMinOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Min"))
                .ElementAt(0);

            datum.MedianLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMedianOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Median"))
                .ElementAt(0);

            datum.AvgLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeAvgOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Avg"))
                .ElementAt(0);

            datum.ModeLevel = datum.HistogramData.First(v => v.Value.Equals(datum.HistogramData.Select(m => m.Value).Max())).Level;

            datum.HistogramData.Max(x => x.Level);

            datum.ClassWidth = (datum.MaxLevel - datum.MinLevel) / 10;

            datum.UnderModeData = datum.HistogramData
                .Where(v => v.Value <= datum.HistogramData.Max(x => x.Value) * 0.75)
                .Where(v => v.Level < datum.ModeLevel)
                .ToList();

            datum.ModeData = datum.HistogramData
                .Where(v => v.Value >= datum.HistogramData.Max(x => x.Value) * 0.75)
                .ToList();

            datum.UpperModeData = datum.HistogramData
                .Where(v => v.Value <= datum.HistogramData.Max(x => x.Value) * 0.75)
                .Where(v => v.Level > datum.ModeLevel)
                .ToList();

            datum.DistUnderMode = datum.UnderModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);
            datum.DistMode = datum.ModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);
            datum.DistUpperMode = datum.UpperModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);

            datum.CompMinMax = datum.MaxLevel * 100 / datum.MinLevel;
            datum.CompMinMode = datum.ModeLevel * 100 / datum.MinLevel;
            datum.CompModeMax = datum.MaxLevel * 100 / datum.ModeLevel;

            return datum;
        }

        public static SemanticHistogramDatum GetDistanceNormalizedEnergyInstance(SemanticLink semanticLink, TripDirection direction)
        {
            SemanticHistogramDatum datum = new SemanticHistogramDatum();

            datum.SemanticLink = semanticLink;
            datum.Direction = direction;


            DataTable table = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyHistogramOfSemanticLinkWithDistance(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')");
            datum.HistogramData = new List<LevelAndValue>();
            foreach (DataRow row in table.Rows)
            {
                datum.HistogramData.Add(new LevelAndValue() { Level = row.Field<double>("Level"), Value = row.Field<int>("Number") });
            }

            datum.MaxLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMaxOfSemanticLinkWithDistance(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Max"))
                .ElementAt(0);

            datum.MinLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMinOfSemanticLinkWithDistance(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Min"))
                .ElementAt(0);

            datum.MedianLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMedianOfSemanticLinkWithDistance(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Median"))
                .ElementAt(0);

            datum.AvgLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyAvgOfSemanticLinkWithDistance(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Avg"))
                .ElementAt(0);

            datum.ModeLevel = datum.HistogramData.First(v => v.Value.Equals(datum.HistogramData.Select(m => m.Value).Max())).Level;

            datum.HistogramData.Max(x => x.Level);

            datum.ClassWidth = (datum.MaxLevel - datum.MinLevel) / 10;

            datum.UnderModeData = datum.HistogramData
                .Where(v => v.Value <= datum.HistogramData.Max(x => x.Value) * 0.75)
                .Where(v => v.Level < datum.ModeLevel)
                .ToList();

            datum.ModeData = datum.HistogramData
                .Where(v => v.Value >= datum.HistogramData.Max(x => x.Value) * 0.75)
                .ToList();

            datum.UpperModeData = datum.HistogramData
                .Where(v => v.Value <= datum.HistogramData.Max(x => x.Value) * 0.75)
                .Where(v => v.Level > datum.ModeLevel)
                .ToList();

            datum.DistUnderMode = datum.UnderModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);
            datum.DistMode = datum.ModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);
            datum.DistUpperMode = datum.UpperModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);

            datum.CompMinMax = datum.MaxLevel * 100 / datum.MinLevel;
            datum.CompMinMode = datum.ModeLevel * 100 / datum.MinLevel;
            datum.CompModeMax = datum.MaxLevel * 100 / datum.ModeLevel;

            return datum;
        }

        public static SemanticHistogramDatum GetDistanceNormalizedTimeInstance(SemanticLink semanticLink, TripDirection direction)
        {
            SemanticHistogramDatum datum = new SemanticHistogramDatum();

            datum.SemanticLink = semanticLink;
            datum.Direction = direction;

            DataTable table = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeHistogramOfSemanticLinkWithDistance(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')");
            datum.HistogramData = new List<LevelAndValue>();
            foreach (DataRow row in table.Rows)
            {
                datum.HistogramData.Add(new LevelAndValue() { Level = row.Field<int>("Level"), Value = row.Field<int>("Number") });
            }

            datum.MaxLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMaxOfSemanticLinkWithDistance(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Max"))
                .ElementAt(0);

            datum.MinLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMinOfSemanticLinkWithDistance(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Min"))
                .ElementAt(0);

            datum.MedianLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMedianOfSemanticLinkWithDistance(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Median"))
                .ElementAt(0);

            datum.AvgLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeAvgOfSemanticLinkWithDistance(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Avg"))
                .ElementAt(0);

            datum.ModeLevel = datum.HistogramData.First(v => v.Value.Equals(datum.HistogramData.Select(m => m.Value).Max())).Level;

            datum.HistogramData.Max(x => x.Level);

            datum.ClassWidth = (datum.MaxLevel - datum.MinLevel) / 10;

            datum.UnderModeData = datum.HistogramData
                .Where(v => v.Value <= datum.HistogramData.Max(x => x.Value) * 0.75)
                .Where(v => v.Level < datum.ModeLevel)
                .ToList();

            datum.ModeData = datum.HistogramData
                .Where(v => v.Value >= datum.HistogramData.Max(x => x.Value) * 0.75)
                .ToList();

            datum.UpperModeData = datum.HistogramData
                .Where(v => v.Value <= datum.HistogramData.Max(x => x.Value) * 0.75)
                .Where(v => v.Level > datum.ModeLevel)
                .ToList();

            datum.DistUnderMode = datum.UnderModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);
            datum.DistMode = datum.ModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);
            datum.DistUpperMode = datum.UpperModeData.Sum(x => x.Value) * 100 / datum.HistogramData.Sum(x => x.Value);

            datum.CompMinMax = datum.MaxLevel * 100 / datum.MinLevel;
            datum.CompMinMode = datum.ModeLevel * 100 / datum.MinLevel;
            datum.CompModeMax = datum.MaxLevel * 100 / datum.ModeLevel;

            return datum;
        }

        public static SemanticHistogramDatum GetStackedEnergyInstance(SemanticLink semanticLink, TripDirection direction)
        {
            SemanticHistogramDatum datum = new SemanticHistogramDatum();

            datum.SemanticLink = semanticLink;
            datum.Direction = direction;

            DataTable table = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyStackOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')");
            datum.HistogramData = new List<LevelAndValue>();
            foreach (DataRow row in table.Rows)
            {
                datum.HistogramData.Add(new LevelAndValue() { Level = row.Field<double>("Level"), Value = row.Field<double>("Stack") });
            }

            datum.Sum = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergySumOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Sum"))
                .ElementAt(0);

            datum.Number = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyNumberOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Number"))
                .ElementAt(0);

            datum.MaxLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMaxOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Max"))
                .ElementAt(0);

            datum.MinLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMinOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Min"))
                .ElementAt(0);

            datum.ClassWidth = (datum.MaxLevel - datum.MinLevel) / 10;

            return datum;
        }

        public static SemanticHistogramDatum GetStackedTimeInstance(SemanticLink semanticLink, TripDirection direction)
        {
            SemanticHistogramDatum datum = new SemanticHistogramDatum();

            datum.SemanticLink = semanticLink;
            datum.Direction = direction;

            DataTable table = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeStackOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')");
            datum.HistogramData = new List<LevelAndValue>();
            foreach (DataRow row in table.Rows)
            {
                datum.HistogramData.Add(new LevelAndValue() { Level = row.Field<int>("Level"), Value = row.Field<int>("Stack") });
            }

            datum.Sum = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeSumOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Sum"))
                .ElementAt(0);

            datum.Number = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeNumberOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Number"))
                .ElementAt(0);

            datum.MaxLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMaxOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Max"))
                .ElementAt(0);

            datum.MinLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMinOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Min"))
                .ElementAt(0);

            datum.ClassWidth = (datum.MaxLevel - datum.MinLevel) / 10;

            return datum;
        }

        public static SemanticHistogramDatum GetNormalizedStackedEnergyInstance(SemanticLink semanticLink, TripDirection direction)
        {
            SemanticHistogramDatum datum = new SemanticHistogramDatum();

            datum.SemanticLink = semanticLink;
            datum.Direction = direction;

            datum.Number = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyNumberOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Number"))
                .ElementAt(0);

            datum.Sum = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergySumOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Sum"))
                .ElementAt(0);

            DataTable table = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyStackOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')");
            datum.HistogramData = new List<LevelAndValue>();
            foreach (DataRow row in table.Rows)
            {
                datum.HistogramData.Add(new LevelAndValue() { Level = row.Field<double>("Level"), Value = row.Field<double>("Stack") / datum.Number });
            }

            datum.MaxLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMaxOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Max"))
                .ElementAt(0);

            datum.MinLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedEnergyMinOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<double>("Min"))
                .ElementAt(0);

            datum.ClassWidth = (datum.MaxLevel - datum.MinLevel) / 10;

            return datum;
        }

        public static SemanticHistogramDatum GetNormalizedStackedTimeInstance(SemanticLink semanticLink, TripDirection direction)
        {
            SemanticHistogramDatum datum = new SemanticHistogramDatum();

            datum.SemanticLink = semanticLink;
            datum.Direction = direction;

            datum.Sum = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeSumOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
               .AsEnumerable()
               .Select(x => x.Field<int>("Sum"))
               .ElementAt(0);

            datum.Number = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeNumberOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Number"))
                .ElementAt(0);

            DataTable table = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeStackOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')");
            datum.HistogramData = new List<LevelAndValue>();
            foreach (DataRow row in table.Rows)
            {
                datum.HistogramData.Add(new LevelAndValue() { Level = row.Field<int>("Level"), Value = row.Field<int>("Stack") / datum.Number });
            }

            datum.MaxLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMaxOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Max"))
                .ElementAt(0);

            datum.MinLevel = DatabaseAccesserEcolog.GetResult("SELECT * FROM funcNormalizedTimeMinOfSemanticLink(" + semanticLink.SemanticLinkId + ", '" + direction.Direction + "')")
                .AsEnumerable()
                .Select(x => x.Field<int>("Min"))
                .ElementAt(0);

            datum.ClassWidth = (datum.MaxLevel - datum.MinLevel) / 10;

            return datum;
        }
    }
}
