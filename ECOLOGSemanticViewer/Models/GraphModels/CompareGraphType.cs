using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ECOLOGSemanticViewer.Models.GraphModels
{
    public class CompareGraphType : NotificationObject
    {
        public enum GraphTypes { HistogramGraph, DistanceNormalizedHistogram, StackGraph, NormalizedStackGraph }

        public static List<CompareGraphType.GraphTypes> GetAllGraphTypes()
        {
            var ret = new List<CompareGraphType.GraphTypes>();

            ret.Add(GraphTypes.HistogramGraph);
            ret.Add(GraphTypes.DistanceNormalizedHistogram);
            ret.Add(GraphTypes.StackGraph);
            ret.Add(GraphTypes.NormalizedStackGraph);

            return ret;
        }
    }
}
