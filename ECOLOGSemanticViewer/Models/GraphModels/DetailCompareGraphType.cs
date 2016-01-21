using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace ECOLOGSemanticViewer.Models.GraphModels
{
    public class DetailCompareGraphType : NotificationObject
    {
        public enum TripCategory { TimeRepresentativeTrips, EnergyRepresentativeTrips}

        public enum GraphTypes { SpeedTransitionGraph, AccTransitionGraph, EnergyStackGraph}

        public enum Axes { Distance, Time}

        public static List<DetailCompareGraphType.TripCategory> GetAllTripCategories()
        {
            var ret = new List<DetailCompareGraphType.TripCategory>();

            ret.Add(TripCategory.EnergyRepresentativeTrips);
            ret.Add(TripCategory.TimeRepresentativeTrips);

            return ret;
        }

        public static List<DetailCompareGraphType.GraphTypes> GetAllGraphTypes()
        {
            var ret = new List<DetailCompareGraphType.GraphTypes>();

            ret.Add(GraphTypes.SpeedTransitionGraph);
            ret.Add(GraphTypes.AccTransitionGraph);
            ret.Add(GraphTypes.EnergyStackGraph);

            return ret;
        }

        public static List<DetailCompareGraphType.Axes> GetAllAxesTypes()
        {
            var ret = new List<DetailCompareGraphType.Axes>();

            ret.Add(Axes.Distance);
            ret.Add(Axes.Time);

            return ret;
        }
    }
}
