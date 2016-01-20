using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Data;
using ECOLOGSemanticViewer.Utils;
using OxyPlot.Series;

namespace ECOLOGSemanticViewer.Models.EcologModels
{
    public class SemanticLink : NotificationObject
    {

        #region SemanticLinkId変更通知プロパティ
        private int _SemanticLinkId;

        public int SemanticLinkId
        {
            get
            { return _SemanticLinkId; }
            set
            { 
                if (_SemanticLinkId == value)
                    return;
                _SemanticLinkId = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region DriverId変更通知プロパティ
        private int _DriverId;

        public int DriverId
        {
            get
            { return _DriverId; }
            set
            { 
                if (_DriverId == value)
                    return;
                _DriverId = value;
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

        #region Semantics変更通知プロパティ
        private string _Semantics;

        public string Semantics
        {
            get
            { return _Semantics; }
            set
            { 
                if (_Semantics == value)
                    return;
                _Semantics = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public static List<SemanticLink> GetAllSemanticLinks(){

            var ret = new List<SemanticLink>();

            DataTable semanticLinkTable = new DataTable();

            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT");
            query.AppendLine("  DISTINCT semantic_link_id,");
            query.AppendLine("  driver_id,");
            query.AppendLine("  semantics");
            query.AppendLine("FROM semantic_links");
            query.AppendLine("ORDER BY semantic_link_id");

            semanticLinkTable = DatabaseAccesserEcolog.GetResult(query.ToString());

            for (int i = 0; i < semanticLinkTable.Rows.Count; i++)
            {
                ret.Add(new SemanticLink()
                {
                    SemanticLinkId = (int)semanticLinkTable.Rows[i]["semantic_link_id"],
                    DriverId = (int)semanticLinkTable.Rows[i]["driver_id"],
                    Semantics = (string)semanticLinkTable.Rows[i]["semantics"],
                });
            }

            return ret;
        }

        public static List<SemanticLink> GetDefaultOutwardSemanticLinks()
        {

            var ret = new List<SemanticLink>();

            DataTable semanticLinkTable = new DataTable();

            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT");
            query.AppendLine("  DISTINCT SemanticLinkID,");
            query.AppendLine("  DriverID,");
            query.AppendLine("  Semantics");
            query.AppendLine("FROM default_Outward_SemanticLinks");
            query.AppendLine("ORDER BY SemanticLinkID");

            semanticLinkTable = DatabaseAccesserEcolog.GetResult(query.ToString());
            Console.WriteLine("O COUNT: " + semanticLinkTable.Rows.Count);

            for (int i = 0; i < semanticLinkTable.Rows.Count; i++)
            {
                ret.Add(new SemanticLink()
                {
                    SemanticLinkId = (int)semanticLinkTable.Rows[i]["SemanticLinkID"],
                    DriverId = (int)semanticLinkTable.Rows[i]["DriverID"],
                    Semantics = (string)semanticLinkTable.Rows[i]["Semantics"],
                });
            }

            return ret;
        }

        public static List<SemanticLink> GetDefaultHomewardSemanticLinks()
        {

            var ret = new List<SemanticLink>();

            DataTable semanticLinkTable = new DataTable();

            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT");
            query.AppendLine("  DISTINCT SemanticLinkID,");
            query.AppendLine("  DriverID,");
            query.AppendLine("  semantics");
            query.AppendLine("FROM default_Homeward_SemanticLinks");
            query.AppendLine("ORDER BY SemanticLinkID");

            semanticLinkTable = DatabaseAccesserEcolog.GetResult(query.ToString());
            Console.WriteLine("H COUNT: " + semanticLinkTable.Rows.Count);

            for (int i = 0; i < semanticLinkTable.Rows.Count; i++)
            {
                ret.Add(new SemanticLink()
                {
                    SemanticLinkId = (int)semanticLinkTable.Rows[i]["SemanticLinkID"],
                    DriverId = (int)semanticLinkTable.Rows[i]["DriverID"],
                    Semantics = (string)semanticLinkTable.Rows[i]["Semantics"],
                });
            }

            return ret;
        }

        public static List<SemanticLink> GetTestSemanticLinks()
        {
            var ret = new List<SemanticLink>();

            DataTable semanticLinkTable = new DataTable();

            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT");
            query.AppendLine("  DISTINCT SemanticLinkID,");
            query.AppendLine("  DriverID,");
            query.AppendLine("  semantics");
            query.AppendLine("FROM default_Outward_SemanticLinks");
            query.AppendLine("WHERE SemanticLinkID = 187");
            query.AppendLine("ORDER BY SemanticLinkID");

            semanticLinkTable = DatabaseAccesserEcolog.GetResult(query.ToString());
            Console.WriteLine("H COUNT: " + semanticLinkTable.Rows.Count);

            for (int i = 0; i < semanticLinkTable.Rows.Count; i++)
            {
                ret.Add(new SemanticLink()
                {
                    SemanticLinkId = (int)semanticLinkTable.Rows[i]["SemanticLinkID"],
                    DriverId = (int)semanticLinkTable.Rows[i]["DriverID"],
                    Semantics = (string)semanticLinkTable.Rows[i]["Semantics"],
                });
            }

            return ret;
        }
    }
}
