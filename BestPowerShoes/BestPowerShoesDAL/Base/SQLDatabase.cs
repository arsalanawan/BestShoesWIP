using log4net;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestPowerShoesDAL.Base
{
    /// <summary>
    /// Baseclass for the database connection and interaction 
    /// <c>Version 1.0</c>
    /// </summary>
    public class SQLDatabase
    {
        /// <summary>
        /// Connection information class
        /// <c>Version 1.0</c>
        /// </summary>
        public class Connection
        {
            private static SqlConnection connection;

            /// <value>
            /// Internal method to get and set the connection information to the <seealso>ConnectionClass</seealso>
            /// <c>Version 1.0</c>
            /// </value>
            protected static SqlConnection DBConnection
            {
                get { return new SqlConnection(ConnectionString); }
                set { connection = value; }
            }

            /// <value>
            /// Private method to get the connection string.
            /// <c>Version 1.0</c>
            /// </value>
            protected static string ConnectionString
            {
                get { return DBConstants.Constants.CONNECTION_STRING; }
            }

            /// <summary>
            /// Protected Method to get connection string based on connection name.
            /// </summary>
            /// <param name="strValue">Contains Connection name to get the connection string</param>
            /// <returns>Returns connection string based on name of the connection.</returns>
            /// <c>Version 2.0</c>
            protected static string GetConnectionString(string strValue)
            {
                return DBConstants.Constants.GetConnectionString(strValue);
            }

            /// <summary>
            /// Protected Method to get sqlconnection based on connection name.
            /// </summary>
            /// <param name="strValue">Contains Connection name to get the connection string</param>
            /// <returns>Returns sqlconnection string based on name of the connection.</returns>
            /// <c>Version 2.0</c>
            protected static SqlConnection GetDBConnection(string strValue)
            {
                string strConnectionString = GetConnectionString(strValue);
                return new SqlConnection(strConnectionString);
            }
        }

        /// <summary>
        /// Customised class for executing the database commands using the connection
        /// </summary>
        /// <c>Version 1.0</c>
        public class Execute : Connection
        {
            private static ILog logger = LogManager.GetLogger(typeof(Execute));

            /// <summary>
            /// Method for executing a sql command using the 
            /// ConnectionClass connection and returns a dataset
            /// </summary>
            /// <param name="objValue">Sql command value that contains command text and command type information.</param>
            /// <returns><c>DataSet</c>, result of the command executed.</returns>
            /// <c>Version 1.0</c>
            public static DataSet ExecuteDataset(SqlCommand objValue)
            {
                try
                {
                    logger.Debug("ExecuteDataset " + objValue.CommandText);
                    DataSet ds = new DataSet();
                    objValue.Connection = Connection.DBConnection;
                    SqlDataAdapter da = new SqlDataAdapter(objValue);
                    da.Fill(ds);
                    return ds;
                }
                catch (Exception objException)
                {
                    logger.Error("Error executing " + objValue.CommandText + ": " + objException.ToString());
                    throw new Exception(string.Concat("Error executing the ", objValue.CommandText,
                        " in Execute.ExecuteDataSet(SQLCommand) method."), objException);
                }
            }

            /// <summary>
            /// Overloaded Method for executing a sql command using the 
            /// ConnectionClass connection and returns a dataset
            /// </summary>
            /// <param name="strValue">Contains connection name to get the connection string.</param>
            /// <param name="objValue">Sql command value that contains command text and command type information.</param>
            /// <returns><c>DataSet</c>, result of the command executed.</returns>
            /// <c>Version 1.0</c>
            public static DataSet ExecuteDataset(string strValue, SqlCommand objValue)
            {
                try
                {
                    logger.Debug("ExecuteDataset CDSCode -->" + strValue + " CommandText --> " + objValue.CommandText);
                    DataSet ds = new DataSet();
                    objValue.Connection = Connection.GetDBConnection(strValue);
                    SqlDataAdapter da = new SqlDataAdapter(objValue);
                    da.Fill(ds);
                    return ds;
                }
                catch (Exception objException)
                {
                    logger.Error("Error executing " + objValue.CommandText + ": " + objException.ToString());
                    throw new Exception(string.Concat("Error executing the ", objValue.CommandText,
                        " in Execute.ExecuteDataSet(SQLCommand) method."), objException);
                }
            }

            /// <summary>
            /// Overloaded method for executing a command string passed as 
            /// argument using the ConnectionClass connection and returns a dataset
            /// </summary>
            /// <param name="strValue">string value containing command text</param>
            /// <returns><c>DataSet</c>, result of the command executed.</returns>
            /// <c>Version 1.0</c>
            public static DataSet ExecuteDataset(string strValue)
            {
                try
                {
                    logger.Debug("ExecuteDataset " + strValue);
                    SqlDataAdapter da;
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(strValue, Connection.DBConnection.ConnectionString);
                    da.Fill(ds);
                    return ds;
                }
                catch (Exception objException)
                {
                    logger.Error("Error executing " + strValue + ": " + objException.ToString());
                    throw new Exception(string.Concat("Error executing the ", strValue,
                        " in Execute.ExecuteDataSet(string) method."), objException);
                }
            }

            /// <summary>
            /// Overloaded method for executing a command string passed as 
            /// argument using the ConnectionClass connection and returns a dataset
            /// </summary>
            /// <param name="strValue">Contains connection name to get the connection string.</param>
            /// <param name="strValue1">string value containing command text</param>
            /// <returns><c>DataSet</c>, result of the command executed.</returns>
            /// <c>Version 1.0</c>
            public static DataSet ExecuteDataset(string strValue, string strValue1)
            {
                try
                {
                    logger.Debug("ExecuteDataset CDSCode -->" + strValue + " CommandText --> " + strValue1);
                    SqlDataAdapter da;
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(strValue1, Connection.GetConnectionString(strValue));
                    da.Fill(ds);
                    return ds;
                }
                catch (Exception objException)
                {
                    logger.Error("Error executing " + strValue + ": " + objException.ToString());
                    throw new Exception(string.Concat("Error executing the ", strValue1,
                        " in Execute.ExecuteDataSet(string) method."), objException);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cmd"></param>
            /// <returns></returns>
            public static DataTable ReadTable(SqlCommand cmd)
            {
                DataTable dt = new DataTable();
                SqlDataReader reader = null;
                try
                {
                    cmd.Connection = Connection.DBConnection;
                    cmd.Connection.Open();
                    reader = cmd.ExecuteReader();
                    int fieldc = reader.FieldCount;
                    for (int i = 0; i < fieldc; i++)
                    {
                        DataColumn dc = new DataColumn(reader.GetName(i), reader.GetFieldType(i));
                        dt.Columns.Add(dc);
                    }
                    while (reader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < fieldc; i++)
                        {
                            dr[i] = reader[i];
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
                finally
                {
                    if (reader != null) reader.Close();
                }
                return dt;
            }

            /// <summary>
            /// Method for executing an INSERT/UPDATE/DELETE statements passed as 
            /// sql command using the ConnectionClass connection
            /// </summary>
            /// <param name="objValue">Sql command value that contains command text and command type information.</param>
            /// <c>Version 1.0</c>
            public static bool ExecuteNonQuery(SqlCommand objValue)
            {
                try
                {
                    logger.Debug("ExecuteNonQuery " + objValue.CommandText);
                    objValue.Connection = Connection.DBConnection;
                    objValue.Connection.Open();
                    objValue.ExecuteNonQuery();
                    objValue.Connection.Close();
                    return true;
                }
                catch (Exception objException)
                {
                    logger.Error("Error executing " + objValue.CommandText + ": " + objException.ToString());
                    throw new Exception(string.Concat("Error executing the ", objValue.CommandText,
                        " in Execute.ExecuteNonQuery(SqlCommand) method."), objException);
                }
                return false;
            }

            /// <summary>
            /// Overloaded Method for executing an INSERT/UPDATE/DELETE statements passed as 
            /// sql command using the ConnectionClass connection
            /// </summary>
            /// <param name="objValue">Sql command value that contains command text and command type information.</param>
            /// <param name="strValue">Contains connection name to get the connection string.</param>
            /// <c>Version 1.0</c>
            public static void ExecuteNonQuery(string strValue, SqlCommand objValue)
            {
                try
                {
                    logger.Debug("ExecuteNonQuery CDSCode -->" + strValue + " CommandText --> " + objValue.CommandText);
                    objValue.Connection = Connection.GetDBConnection(strValue);
                    objValue.Connection.Open();
                    objValue.ExecuteNonQuery();
                    objValue.Connection.Close();
                }
                catch (Exception objException)
                {
                    logger.Error("Error executing " + objValue.CommandText + ": " + objException.ToString());
                    throw new Exception(string.Concat("Error executing the ", objValue.CommandText,
                        " in Execute.ExecuteNonQuery(SqlCommand) method."), objException);
                }
            }

            /// <summary>
            /// Method for executing an sql command passed as argument and will return 
            /// the first row first column element of the returned result set
            /// </summary>
            /// <param name="objValue">Sql command value that contains command text and command type information.</param>
            /// <returns><c>object</c>, containing the first row first column value of the result obtained.</returns>
            /// <c>Version 1.0</c>
            public static object ExecuteScalar(SqlCommand objValue)
            {
                try
                {
                    logger.Debug("ExecuteScalar " + objValue.CommandText);
                    objValue.Connection = Connection.DBConnection;
                    objValue.Connection.Open();
                    object returnValue;
                    returnValue = objValue.ExecuteScalar();
                    objValue.Connection.Close();
                    return returnValue;
                }
                catch (Exception objException)
                {
                    logger.Error("Error executing " + objValue.CommandText + ": " + objException.ToString());
                    throw new Exception(string.Concat("Error executing the ", objValue.CommandText,
                        " in Execute.ExecuteScalar(SqlCommand) method."), objException);
                }
            }

            /// <summary>
            /// Overloaded Method for executing an sql command passed as argument and will return 
            /// the first row first column element of the returned result set
            /// </summary>
            /// <param name="objValue">Sql command value that contains command text and command type information.</param>
            /// <param name="strValue">Contains connection name to get the connection string.</param>
            /// <returns><c>object</c>, containing the first row first column value of the result obtained.</returns>
            /// <c>Version 1.0</c>
            public static object ExecuteScalar(string strValue, SqlCommand objValue)
            {
                try
                {
                    logger.Debug("ExecuteScalar CDSCode -->" + strValue + " CommandText --> " + objValue.CommandText);
                    objValue.Connection = Connection.GetDBConnection(strValue);
                    objValue.Connection.Open();
                    object returnValue;
                    returnValue = objValue.ExecuteScalar();
                    objValue.Connection.Close();
                    return returnValue;
                }
                catch (Exception objException)
                {
                    logger.Error("Error executing CDSCode -->" + strValue + "  CommandText --> " + objValue.CommandText + ": " + objException.ToString());
                    throw new Exception(string.Concat("Error executing the ", objValue.CommandText,
                        " in Execute.ExecuteScalar(SqlCommand) method."), objException);
                }
            }

            /// <summary>
            /// Method for executing the update statement prepared from the CommandBuilder for an adapter. This method will return true if 
            /// the update completes without errors.
            /// </summary>
            /// <param name="strValue">Query for which the update statement is prepared and executed.</param>
            /// <param name="dsValue">Data which needs to be updated to the database.</param>
            /// <returns><c>bool</c>, true if update completes without errors.</returns>
            /// <c>Version 1.0</c>
            public static bool ExecuteUpdate(string strValue, DataSet dsValue)
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(strValue, SQLDatabase.Connection.DBConnection.ConnectionString);
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.Update(dsValue.Tables[0]);
                    return true;
                }
                catch (Exception objException)
                {
                    throw new Exception(string.Concat("Error in Execute.ExecuteUpdate(" + strValue + ") method."), objException);
                }
            }

            /// <summary>
            /// Overloaded Method for executing the update statement prepared from the CommandBuilder for an adapter. This method will return true if 
            /// the update completes without errors.
            /// </summary>
            /// <param name="strValue">Contains connection name to get the connection string.</param>
            /// <param name="strValue1">Query for which the update statement is prepared and executed.</param>
            /// <param name="dsValue">Data which needs to be updated to the database.</param>
            /// <returns><c>bool</c>, true if update completes without errors.</returns>
            /// <c>Version 1.0</c>
            public static bool ExecuteUpdate(string strValue, string strValue1, DataSet dsValue)
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(strValue1, SQLDatabase.Connection.GetConnectionString(strValue));
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.Update(dsValue.Tables[0]);
                    return true;
                }
                catch (Exception objException)
                {
                    throw new Exception(string.Concat("Error in Execute.ExecuteUpdate(" + strValue1 + ") method."), objException);
                }
            }

            /// <summary>
            /// Public method to get the command object for the current database connection.
            /// </summary>
            /// <returns><c>SqlCommand</c>, command object for the database connection.</returns>
            /// <c>Version 1.0</c>
            public static SqlCommand GetCommandObject()
            {
                SqlCommand objSqlCommand = new SqlCommand("", DBConnection);
                return objSqlCommand;
            }

            /// <summary>
            /// Public method to get the command object for the current database connection.
            /// </summary>
            /// <param name="strValue">Contains connection name to get the connection string.</param>
            /// <returns><c>SqlCommand</c>, command object for the database connection.</returns>
            /// <c>Version 1.0</c>
            public static SqlCommand GetCommandObject(string strValue)
            {
                SqlCommand objSqlCommand = new SqlCommand("", Connection.GetDBConnection(strValue));
                return objSqlCommand;
            }
        }
    }
}
