using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.UI.Database
{
    /// <summary>
    ///
    /// </summary>
    internal class DataManager
    {
        private string _connectionString;
        private SqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataManager"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DataManager(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Executes the data reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> ExecuteDataReader<T>(SqlCommand command, Func<SqlDataReader, T> converter)
        {
            var result = new List<T>();
            command.Connection = _connection;
            try
            {
                _connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(converter(reader));
                }
            }
            catch (Exception e)
            { }
            finally
            {
                if (_connection.State != System.Data.ConnectionState.Closed)
                    _connection.Close();
            }

            return result;
        }
    }
}