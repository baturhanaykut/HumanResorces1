using HumanResources_Application.Models.VMs.DepartmentVMs;
using HumanResources_Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<DepartmentVM>> GetDepartments()
        {

            List<DepartmentVM> departments = (List<DepartmentVM>)await _departmentRepository.GetFilteredList(
                select: x => new DepartmentVM()
                {
                    Id = (int)x.Id,
                    Name = x.Name,
                },
                where: null,
                orderby: x => x.OrderBy(x => x.Name)
                );
            return departments;

        }
    }
}
