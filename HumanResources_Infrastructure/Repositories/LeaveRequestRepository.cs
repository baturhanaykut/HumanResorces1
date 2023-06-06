﻿using HumanResources_Domain.Entities;
using HumanResources_Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Infrastructure.Repositories
{
    public class LeaveRequestRepository : BaseRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
