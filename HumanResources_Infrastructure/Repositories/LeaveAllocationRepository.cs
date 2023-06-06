using HumanResources_Domain.Entities;
using HumanResources_Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Infrastructure.Repositories
{
    internal class LeaveAllocationRepository : BaseRepository<LeaveAllocation>, ILeaveAllocationRepository
    {

        public LeaveAllocationRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
