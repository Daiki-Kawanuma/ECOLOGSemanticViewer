using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.Utils;
using Livet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOLOGSemanticViewer.Models.GraphModels
{
    class SemanticHistogramDatum : NotificationObject
    {
        public SemanticLink SemanticLink { get; set; }

        public TripDirection Direction { get; set; }

        public List<LevelAndValue> HistogramData { get; set; }

        public double MaxLevel { get; set; }

        public double MinLevel { get; set; }

        public double MedianLevel { get; set; }

        public double ModeLevel { get; set; }

        public double ClassWidth { get; set; }

        public List<LevelAndValue> UnderModeData { get; set; }

        public List<LevelAndValue> ModeData { get; set; }

        public List<LevelAndValue> UpperModeData { get; set; }

        private SemanticHistogramDatum()
        {

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

            return datum;
        }
    }
}
