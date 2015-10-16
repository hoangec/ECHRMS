using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Data.Models;

namespace HNGHRMS.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        HngHrmsEntities Get();

    }
}
