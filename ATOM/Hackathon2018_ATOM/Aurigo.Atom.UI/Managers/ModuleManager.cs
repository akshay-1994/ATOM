using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurigo.Atom.UI.Database;
using Aurigo.Atom.UI.DTO;
using Aurigo.Brix.Platform.BusinessLayer.AbstractModels;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;
using Aurigo.Brix.Platform.BusinessLayer.XmlForm_Framework;

namespace Aurigo.Atom.UI.Managers
{
    internal class ModuleManager
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// The data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// The command store
        /// </summary>
        private SqlCommandStore _commandStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleManager"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public ModuleManager(string connectionString)
        {
            _connectionString = connectionString;
            _dataManager = new DataManager(_connectionString);
            _commandStore = new SqlCommandStore();
        }

        /// <summary>
        /// Gets all modules.
        /// </summary>
        /// <returns></returns>
        public List<Module> GetAllModules()
        {
            var allModulesCommand = _commandStore.GetAllModulesCommand();

            var modules = _dataManager.ExecuteDataReader<Module>(allModulesCommand, x =>
            {
                return new Module
                {
                    ModuleId = x["ModuleId"].ToString(),
                    ModuleName = x["ModuleName"].ToString()
                };
            });

            return modules;
        }

        public BrixFormModel GetXmlForm(string moduleId)
        {
            return new BrixFormModel(moduleId, moduleId + ".xml", XMLType.Form);
        }
    }
}