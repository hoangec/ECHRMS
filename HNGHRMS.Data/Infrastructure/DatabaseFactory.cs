using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Data.Models;
namespace HNGHRMS.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private HngHrmsEntities dataContext;
        public HngHrmsEntities Get()
        {
            return dataContext ?? (dataContext = new HngHrmsEntities());
        }

        protected override void DisposeCore()
        {
            if(dataContext != null)
            {
                dataContext.Dispose();
            }
        }
    }
}
