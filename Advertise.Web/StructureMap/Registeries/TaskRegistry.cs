using Advertise.Core.Managers.Transaction;
using StructureMap;

namespace Advertise.Web.StructureMap.Registeries
{
    public class TaskRegistry : Registry
    {
        #region Public Constructors

        public TaskRegistry()
        {
            Scan(scan =>
            {
                scan.Assembly("Advertise.Service");
                scan.AddAllTypesOf<IRunAfterEachRequest>();
                scan.AddAllTypesOf<IRunAtInit>();
                scan.AddAllTypesOf<IRunAtStartUp>();
                scan.AddAllTypesOf<IRunOnEachRequest>();
                scan.AddAllTypesOf<IRunOnError>();
            });
        }

        #endregion Public Constructors
    }
}