using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Data.Models;
namespace HNGHRMS.Data.Infrastructure
{
   public class UnitOfWork : IUnitOfWork
    {
       private readonly IDatabaseFactory databaseFactory;
       private HngHrmsEntities dataContext;
       public UnitOfWork(IDatabaseFactory databaseFactory)
       {
           this.databaseFactory = databaseFactory;

       }
       protected HngHrmsEntities DataContext
       {
           get { return dataContext ?? (dataContext = databaseFactory.Get()); }
       }

       public void Commit()
       {
           DataContext.Commit();
       }
    }
}
