using ECOLOGSemanticViewer.Utils;
using ECOLOGSemanticViewer.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ECOLOGSemanticViewer.Utils
{
    public class DatabaseAccesserEcolog
    {
        public static string ConnectionString = "";
       
        /// <summary>
        /// DBにアクセスして、データを取得する
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <returns>取得されたデータ(DataTable形式)</returns>
        public static DataTable GetResult(string query)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, ConnectionString);

                try
                {
                    sqlConnection.Open();
                    //System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    command.CommandTimeout = 600;
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(dataTable);
                    // System.Windows.Forms.Cursor.Current = Cursors.Default;
                }
                catch (SqlException sqlException)
                {
                    LogWriter.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + sqlException.ToString());
                    // MessageBox.Show(sqlException.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return dataTable;
        }


        /// <summary>
        /// DBにアクセスして、クエリを実行する
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <returns></returns>
        public static bool ExecuteQuery(string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                bool isQueryComplete = false;
                try
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    command.ExecuteNonQuery();
                    isQueryComplete = true;
                }
                catch (SqlException sqlException)
                {
                    LogWriter.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + sqlException.ToString());
                    // MessageBox.Show(sqlException.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    sqlConnection.Close();
                }
                return isQueryComplete;
            }
        }

        /// <summary>
        /// DBにアクセスして、クエリを実行する
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <returns></returns>
        public static bool ExecuteQuery(string query, int timeout)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                bool isQueryComplete = false;
                try
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    command.CommandTimeout = timeout;

                    command.ExecuteNonQuery();
                    isQueryComplete = true;
                }
                catch (SqlException sqlException)
                {
                    LogWriter.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + sqlException.ToString());
                    // MessageBox.Show(sqlException.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    sqlConnection.Close();
                }
                return isQueryComplete;
            }
        }

        /// <summary>
        /// 画像データが存在するか確認する
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int CorrectedPictureDataChecker(string startTime, string endTime)
        {
            int countImages = 0;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                string query = "select COUNT(*) ";
                query += "from CORRECTED_PICTURE ";
                query += "where JST>= '" + startTime + "' ";
                query += "and JST <= '" + endTime + "' ";

                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader dataReader = null;

                try
                {
                    sqlConnection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        countImages = dataReader.GetInt32(0);
                    }
                }
                catch (SqlException sqlException)
                {
                    LogWriter.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + sqlException.ToString());
                }
                finally
                {
                    if (dataReader != null) 
                        dataReader.Close();

                    sqlConnection.Close();
                }
            }

            return countImages;
        }

        /// <summary>
        /// 画像データが存在するか確認する
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int CorrectedPictureDataChecker(string startTime, string endTime, bool useNexus7)
        {
            int countImages = 0;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                string query = "select COUNT(*) ";
                query += "from CORRECTED_PICTURE ";
                query += "where JST>= '" + startTime + "' ";
                query += "and JST <= '" + endTime + "' ";
                if (useNexus7)
                {
                    query += "and SENSOR_ID != 19 ";
                }
                else
                {
                    query += "and SENSOR_ID = 19 ";
                }


                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader dataReader = null;

                try
                {
                    sqlConnection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        countImages = dataReader.GetInt32(0);
                    }
                }
                catch (SqlException sqlException)
                {
                    LogWriter.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + sqlException.ToString());
                }
                finally
                {
                    if (dataReader != null) dataReader.Close();
                    sqlConnection.Close();
                }
            }

            return countImages;
        }

        /// <summary>
        /// DBにアクセスして、ドライバーデータのリストを取得する
        /// </summary>
        /// <returns>取得されたリスト</returns>
        public static Dictionary<string, int> GetDriver()
        {
            Dictionary<string, int> driverSets = new Dictionary<string, int>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string query = "select DRIVERS.NAME, DRIVERS.DRIVER_ID ";
                query += "from DRIVERS ";

                //匿名用クエリ(DEMO用)
                query = "select CONCAT('被験者',DRIVERS.DRIVER_ID) as NAME,DRIVERS.DRIVER_ID  ";
                query += "from DRIVERS ";

                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader dataReader = null;

                try
                {
                    sqlConnection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string driver = dataReader.GetInt32(1) + " [" + dataReader.GetString(0) + "]";
                        driverSets.Add(driver, dataReader.GetInt32(1));
                    }
                }
                catch (SqlException sqlException)
                {
                    LogWriter.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + sqlException.ToString());
                }
                finally
                {
                    if (dataReader != null) dataReader.Close();
                    sqlConnection.Close();
                }
            }

            return driverSets;
        }

        /// <summary>
        /// DBにアクセスして、センサデータのリストを取得する
        /// </summary>
        /// <returns>取得されたリスト</returns>
        public static Dictionary<string, int> GetSensor()
        {
            Dictionary<string, int> sensorSets = new Dictionary<string, int>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string query = "select SENSORS.SENSOR_MODEL, SENSORS.SENSOR_ID ";
                query += "from SENSORS ";

                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader dataReader = null;

                try
                {
                    sqlConnection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string driver_sensor = dataReader.GetInt32(1) + " [" + dataReader.GetString(0) + "]";
                        sensorSets.Add(driver_sensor, dataReader.GetInt32(1));
                    }
                }
                catch (SqlException sqlException)
                {
                    LogWriter.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + sqlException.ToString());
                }
                finally
                {
                    if (dataReader != null) dataReader.Close();
                    sqlConnection.Close();
                }
            }

            return sensorSets;
        }

        /// <summary>
        /// DBにアクセスして、カーデータのリストを取得する
        /// </summary>
        /// <returns>取得されたリスト</returns>
        public static Dictionary<string, int> GetCar()
        {
            Dictionary<string, int> carSets = new Dictionary<string, int>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string query = "select MODEL,CAR_ID ";
                query += "from CARS ";

                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader dataReader = null;

                try
                {
                    sqlConnection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string car = dataReader.GetInt32(1) + " [" + dataReader.GetString(0).Trim() + "]";
                        carSets.Add(car, dataReader.GetInt32(1));
                    }
                }
                catch (SqlException sqlException)
                {
                    LogWriter.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + sqlException.ToString());
                }
                finally
                {
                    if (dataReader != null) dataReader.Close();
                    sqlConnection.Close();
                }
            }

            return carSets;
        }

        /// <summary>
        /// DBにアクセスして、セマンティックリンクデータのリストを取得する
        /// </summary>
        /// <returns>取得されたリスト</returns>
        public static Dictionary<string, int> GetSemanticLink()
        {
            Dictionary<string, int> semanticLinkSets = new Dictionary<string, int>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string query = "select DISTINCT SEMANTICS, SEMANTIC_LINK_ID, DRIVER_ID ";
                query += "from SEMANTIC_LINKS ";

                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader dataReader = null;

                try
                {
                    sqlConnection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        semanticLinkSets.Add(dataReader.GetString(0), dataReader.GetInt32(1));
                    }
                }
                catch (SqlException se)
                {
                    LogWriter.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + se.ToString());
                }
                finally
                {
                    if (dataReader != null) dataReader.Close();
                    sqlConnection.Close();
                }

            }

            return semanticLinkSets;
        }

        /// <summary>
        /// DBにアクセスして、イベントデータのリストを取得する
        /// </summary>
        /// <returns>取得されたリスト</returns>
        public static Dictionary<string, int> GetEvent()
        {
            Dictionary<string, int> eventSets = new Dictionary<string, int>();

            string query = "select EVENT, EVENT_ID ";
            query += "from EVENT ";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand command;
                SqlDataReader dataReader = null;

                try
                {
                    command = new SqlCommand(query, sqlConnection);

                    sqlConnection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        eventSets.Add(dataReader.GetString(0), dataReader.GetInt32(1));
                    }
                }
                catch (SqlException sqlException)
                {
                    LogWriter.Write(System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + sqlException.ToString());
                }
                finally
                {
                    if (dataReader != null) dataReader.Close();
                    sqlConnection.Close();
                }
            }

            return eventSets;
        }
    }
    
}