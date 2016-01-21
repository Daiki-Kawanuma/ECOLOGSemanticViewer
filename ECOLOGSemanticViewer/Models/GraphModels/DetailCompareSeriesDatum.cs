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

        public double RollingLoss { get; set; }
        public double RegeneLoss { get; set; }
        public double AirLoss { get; set; }
        public double ConvertLoss { get; set; }

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

        public static List<DetailCompareSeriesDatum> CreateTimeAccData(List<Ecolog> ecologs)
        {
            DateTime min = ecologs.Min(v => v.Jst);
            var ret = new List<DetailCompareSeriesDatum>();

            double preSpeed = 0;

            foreach (Ecolog ecolog in ecologs)
            {
                ret.Add(new DetailCompareSeriesDatum()
                {
                    X = (int)new TimeSpan(ecolog.Jst.Ticks - min.Ticks).TotalSeconds,
                    Y = (ecolog.Speed - preSpeed) / 3.6 // m/s^2
                });

                preSpeed = ecolog.Speed;
            }

            return ret;
        }

        public static List<DetailCompareSeriesDatum> CreateDistanceAccData(List<Ecolog> ecologs)
        {
            var ret = new List<DetailCompareSeriesDatum>();

            double preSpeed = 0;
            double sumDistanceDifference = 0;

            foreach (Ecolog ecolog in ecologs)
            {
                ret.Add(new DetailCompareSeriesDatum()
                {
                    X = sumDistanceDifference,
                    Y = (ecolog.Speed - preSpeed) / 3.6 // m/s^2
                });

                preSpeed = ecolog.Speed;
                sumDistanceDifference += ecolog.DistanceDifference;
            }

            return ret;
        }

        public static List<DetailCompareSeriesDatum> CreateTimeStackData(List<Ecolog> ecologs)
        {
            DateTime min = ecologs.Min(v => v.Jst);
            var ret = new List<DetailCompareSeriesDatum>();

            double SumRollingLoss = 0;
            double SumRegeneLoss = 0;
            double SumAirLoss = 0;
            double SumConvertLoss = 0;

            for (int i = 0; i < ecologs.Count; i++)
            {
                SumRollingLoss += ecologs[i].EnergyByRollingResistance;
                SumRegeneLoss += Math.Abs(ecologs[i].RegeneLoss);
                SumAirLoss += ecologs[i].EnergyByAirResistance;
                SumConvertLoss += Math.Abs(ecologs[i].ConvertLoss);

                if(i % 5 == 0)
                    ret.Add(new DetailCompareSeriesDatum()
                    {
                        X = (int)new TimeSpan(ecologs[i].Jst.Ticks - min.Ticks).TotalSeconds,
                        RollingLoss = SumRollingLoss,
                        RegeneLoss = SumRegeneLoss,
                        AirLoss = SumAirLoss,
                        ConvertLoss = SumConvertLoss
                    });
            }

            return ret;
        }

        public static List<DetailCompareSeriesDatum> CreateDistanceStackData(List<Ecolog> ecologs)
        {
            double sumDistanceDifference = 0;
            var ret = new List<DetailCompareSeriesDatum>();

            double SumRollingLoss = 0;
            double SumRegeneLoss = 0;
            double SumAirLoss = 0;
            double SumConvertLoss = 0;
            for (int i = 0; i < ecologs.Count; i++ )
            {
                SumRollingLoss += ecologs[i].EnergyByRollingResistance;
                SumRegeneLoss += Math.Abs(ecologs[i].RegeneLoss);
                SumAirLoss += ecologs[i].EnergyByAirResistance;
                SumConvertLoss += Math.Abs(ecologs[i].ConvertLoss);

                if(i % 5 == 0)
                    ret.Add(new DetailCompareSeriesDatum()
                    {
                        X = sumDistanceDifference,
                        RollingLoss = SumRollingLoss,
                        RegeneLoss = SumRegeneLoss,
                        AirLoss = SumAirLoss,
                        ConvertLoss = SumConvertLoss
                    });

                sumDistanceDifference += ecologs[i].DistanceDifference;
            }

            return ret;
        }
    }
}
