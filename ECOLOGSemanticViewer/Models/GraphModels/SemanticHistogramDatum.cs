using ECOLOGSemanticViewer.Models.EcologModels;
using ECOLOGSemanticViewer.Utils;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOLOGSemanticViewer.Models.GraphModels
{
    class SemanticHistogramDatum : NotificationObject
    {
        public SemanticLink SemanticLink { get; set; }

        public List<LevelAndValue> HistogramData { get; set; }

        public double MaxLevel { get; set; }

        public double MinLevel { get; set; }

        public double MedianLevel { get; set; }

        public double ModeLevel { get; set; }

        public double ClassWidth { get; set; }

        public double UnderModepercent { get; set; }

        public double ModePercent { get; set; }

        public double UpperModePercent { get; set; }
    }
}
