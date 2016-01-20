using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ECOLOGSemanticViewer.Models.GraphModels
{
    public class DetailCompareGraphType : NotificationObject
    {
        public static enum GraphTypes { SpeedTransitionGraph, AccTransitionGraph, EnergyStackGraph}

        public static enum Axes { Distance, Time}

        public GraphTypes GraphType { get; set; }
        public Axes Axis { get; set; }

        public static List<DetailCompareGraphType> GetAllGraphTypes()
        {
            var ret = new List<DetailCompareGraphType>();

            ret.Add(new DetailCompareGraphType() { GraphType = GraphTypes.SpeedTransitionGraph });
            ret.Add(new DetailCompareGraphType() { GraphType = GraphTypes.AccTransitionGraph });
            ret.Add(new DetailCompareGraphType() { GraphType = GraphTypes.EnergyStackGraph });

            return ret;
        }

        public static List<DetailCompareGraphType> GetAllAxesTypes()
        {
            var ret = new List<DetailCompareGraphType>();

            ret.Add(new DetailCompareGraphType() { Axis = Axes.Distance });
            ret.Add(new DetailCompareGraphType() { Axis = Axes.Time });

            return ret;
        }
    }
}
