using AutoMapper;
using HumanResources_Application.Models.DTOs.LeaveTypeDTOs;
using HumanResources_Application.Models.VMs.LeaveTypeVMs;
using HumanResources_Application.Service.CompanyServices;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using HumanResources_Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.LeaveTypeServices
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;

        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository, IMapper mapper, ICompanyService companyService)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _companyService = companyService;
        }


        public async Task<bool> Create(CreateLeaveTypeDTO model, string userName)
        {
            var leaveType = _mapper.Map<LeaveType>(model);
            leaveType.CompanyId = await _companyService.GetCompanyId(userName);
            return await _leaveTypeRepository.Add(leaveType);
        }

        public async Task Delete(int id)
        {
            LeaveType deletedLeaveType = await _leaveTypeRepository.GetDefault(x => x.Id == id);

            if (deletedLeaveType != null)
            {
                deletedLeaveType.Status = Status.Passive;
                deletedLeaveType.DeleteDate = DateTime.Now;
                await _leaveTypeRepository.Delete(deletedLeaveType);
            }
        }

        public async Task<UpdateLeaveTypeDTO> GetById(int id)
        {
            LeaveType leaveType = await _leaveTypeRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateLeaveTypeDTO>(leaveType);

            return model;
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes(Guid? id)
        {
            List<LeaveTypeVM> leaveTypes = (List<LeaveTypeVM>)await _leaveTypeRepository.GetFilteredList(

                           select: x => new LeaveTypeVM()
                           {
                               Id = x.Id,
                               LeaveTypeName = x.LeaveTypeName,
                               DefaultDays = x.DefaultDays,
                           },
                         where: x => (x.Status != Status.Passive) && (x.CompanyId == id),
                         orderby: x => x.OrderByDescending(x => x.LeaveTypeName)
                           );

            return leaveTypes;
        }

        public async Task Update(UpdateLeaveTypeDTO model, string userName)
        {
            var leaveType = _mapper.Map<LeaveType>(model);
            leaveType.CompanyId = await _companyService.GetCompanyId(userName);
            leaveType.CreateDate = DateTime.Now;
            await _leaveTypeRepository.Update(leaveType);
        }
    }
}
