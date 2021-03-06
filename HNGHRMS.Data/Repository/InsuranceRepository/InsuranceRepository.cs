﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Data.Infrastructure;

namespace HNGHRMS.Data.Repository
{
    public class InsuranceRepository: RepositoryBase<Insurance> , IInsuranceRepository
    {
        public InsuranceRepository(IDatabaseFactory databaseFactory) : base(databaseFactory) { }
    }
}
