using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using ECOLOGSemanticViewer.Models.EcologModels;

namespace ECOLOGSemanticViewer.Models.GraphModels
{
    public class DetailCompareSeriesDatum : NotificationObject
    {
        public double X { get; set; }

        public double Y { get; set; }

        public static List<DetailCompareSeriesDatum> CreateTimeSpeedData(List<Ecolog> ecologs)
        {
            DateTime min = ecologs.Min(v => v.Jst);
            var ret = new List<DetailCompareSeriesDatum>();

            foreach (Ecolog ecolog in ecologs)
            {
                ret.Add(new DetailCompareSeriesDatum()
                {
                    X = (int) new TimeSpan(ecolog.Jst.Ticks - min.Ticks).TotalSeconds,
                    Y = ecolog.Speed
                });
            }

            return ret;
        }

        public static List<DetailCompareSeriesDatum> CreateDistanceSpeedData(List<Ecolog> ecologs)
        {
            DateTime min = ecologs.Min(v => v.Jst);
            var ret = new List<DetailCompareSeriesDatum>();

            double sumDistanceDifference = 0;

            foreach (Ecolog ecolog in ecologs)
            {
                ret.Add(new DetailCompareSeriesDatum()
                {
                    X = sumDistanceDifference,
                    Y = ecolog.Speed
                });

                sumDistanceDifference += ecolog.DistanceDifference;
            }

            return ret;
        }
    }
}
