using System.Data.SqlClient;

namespace Aurigo.Atom.UI.Database
{
    /// <summary>
    ///
    /// </summary>
    internal class SqlCommandStore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCommandStore"/> class.
        /// </summary>
        public SqlCommandStore()
        {
        }

        /// <summary>
        /// Gets all modules command.
        /// </summary>
        /// <returns></returns>
        public SqlCommand GetAllModulesCommand()
        {
            var sqlText = @"SELECT *
                            FROM MODMGMTModules
                            WHERE IsActive=1 AND NavigateUrl='xmlform' AND ParentModuleId='ENTPRSE'";

            return new SqlCommand(sqlText);
        }
    }
}