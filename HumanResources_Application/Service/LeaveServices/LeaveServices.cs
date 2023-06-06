using AutoMapper;
using HumanResources_Application.Models.DTOs.LeaveDTOs;
using HumanResources_Application.Models.VMs.AdvanceVMs;
using HumanResources_Application.Models.VMs.LeaveTypeVMs;
using HumanResources_Application.Models.VMs.LeaveVMs;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using HumanResources_Domain.Repositories;
using HumanResources_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.LeaveServices
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppUserService _appUserService;



        public LeaveService(IMapper mapper, IAppUserRepository appUserRepository, IAppUserService appUserService, ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository)
        {

            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _appUserService = appUserService;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<bool> Create(CreateLeaveDTO model, string userName)
        {
            LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(model);
            leaveRequest.AppUserId = await _appUserService.GetUserId(userName);
            return await _leaveRequestRepository.Add(leaveRequest);
        }
        public async Task Delete(int id)
        {
            LeaveRequest leaveRequest = await _leaveRequestRepository.GetDefault(x => x.Id == id);
            if (leaveRequest != null && leaveRequest.ApproveStatus == ApproveStatus.InApproval)
            {
                leaveRequest.Status = Status.Passive;
                leaveRequest.DeleteDate = DateTime.Now;
                await _leaveRequestRepository.Delete(leaveRequest);
            }
        }
        public async Task<UpdateLeaveDTO> GetByID(int id)
        {
            LeaveRequest leaveRequest = await _leaveRequestRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateLeaveDTO>(leaveRequest);

            return model;
        }

        public async Task Update(UpdateLeaveDTO model)
        {
            LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(model);
            leaveRequest.CreateDate = DateTime.Now;
            if (leaveRequest != null)
            {
                await _leaveRequestRepository.Update(leaveRequest);
            }
        }

        public async Task<List<LeaveVM>> GetLeaves(Guid? id)
        {
            List<LeaveVM> leaves = (List<LeaveVM>)await _leaveRequestRepository.GetFilteredList(

              select: x => new LeaveVM()
              {
                  Id = x.Id,
                  FullName = x.AppUser.FullName,
                  StartDate = x.StartDate.ToShortDateString(),
                  EndDate = x.EndDate.ToShortDateString(),
                  LeaveDay = x.LeaveDay,
                  RequestComments = x.RequestComments,
                  ApproveStatus = x.ApproveStatus,
                  CreateDate = x.CreateDate.ToShortDateString(),
                  Status = x.Status,
                  AppUserId = x.AppUser.Id,
                  ExecutiveId = x.AppUser.ExecutiveId,
                  ExecutiveName = x.AppUser.Executive.FullName,
                  LeaveTypeId = x.LeaveType.Id,
                  LeaveTypeName = x.LeaveType.LeaveTypeName

              },
            where: x => (x.Status != Status.Passive) && (x.AppUser.CompanyId == id),
            orderby: x => x.OrderByDescending(x => x.CreateDate),
            include: x => x.Include(x => x.AppUser)
              );
            return leaves;


        }

        public async Task<List<LeaveVM>> GetPersonelLeaves(string userName)
        {
            var leaves = await _leaveRequestRepository.GetFilteredList(
            select: x => new LeaveVM()
            {
                Id = x.Id,
                StartDate = x.StartDate.ToShortDateString(),
                EndDate = x.EndDate.ToShortDateString(),
                LeaveDay = x.LeaveDay,
                RequestComments = x.RequestComments,
                ApproveStatus = x.ApproveStatus,
                CreateDate = x.CreateDate.ToShortDateString(),
                Status = x.Status,
                AppUserId = x.AppUser.Id,
                ExecutiveId = x.AppUser.ExecutiveId,
                ExecutiveName = x.AppUser.Executive.FullName,
                LeaveTypeId = x.LeaveType.Id,
                LeaveTypeName = x.LeaveType.LeaveTypeName
            },
                where: x => x.AppUser.UserName == userName && x.Status != Status.Passive,
                orderby: x => x.OrderByDescending(x => x.CreateDate),
                include: x => x.Include(x => x.AppUser));

            return (List<LeaveVM>)leaves;
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {

            List<LeaveTypeVM> leaveTypes = (List<LeaveTypeVM>)await _leaveTypeRepository.GetFilteredList(
                select: x => new LeaveTypeVM()
                {
                    Id = (int)x.Id,
                    LeaveTypeName = x.LeaveTypeName,
                },
                where: null,
                orderby: x => x.OrderBy(x => x.LeaveTypeName)
                );
            return leaveTypes;
        }

        public async Task<List<LeaveVM>> GetLeaveEmployee(Guid? id)
        {

            List<LeaveVM> leaves = (List<LeaveVM>)await _leaveRequestRepository.GetFilteredList(

              select: x => new LeaveVM()
              {
                  Id = x.Id,
                  FullName = x.AppUser.FullName,
                  StartDate = x.StartDate.ToShortDateString(),
                  EndDate = x.EndDate.ToShortDateString(),
                  LeaveDay = x.LeaveDay,
                  RequestComments = x.RequestComments,
                  ApproveStatus = x.ApproveStatus,
                  CreateDate = x.CreateDate.ToShortDateString(),
                  Status = x.Status,
                  AppUserId = x.AppUser.Id,
                  ExecutiveId = x.AppUser.ExecutiveId,
                  ExecutiveName = x.AppUser.Executive.FullName,
                  LeaveTypeId = x.LeaveType.Id,
                  LeaveTypeName = x.LeaveType.LeaveTypeName
              },
             where: x => (x.Status != Status.Passive) && (x.AppUser.ExecutiveId == id),
             orderby: x => x.OrderByDescending(x => x.CreateDate),
             include: x => x.Include(x => x.AppUser));
            return leaves;
        }

        public async Task Accept(int id)
        {
            LeaveRequest leave = await _leaveRequestRepository.GetDefault(x => x.Id == id);
            leave.ApproveStatus = ApproveStatus.Approved;
            await _leaveRequestRepository.Update(leave);
        }

        public async Task Reject(int id)
        {
            LeaveRequest leave = await _leaveRequestRepository.GetDefault(x => x.Id == id);
            leave.ApproveStatus = ApproveStatus.Denied;
            await _leaveRequestRepository.Update(leave);
        }

        public async Task<List<LeaveVM>> GetLeavesCompanyManagerAwatingApproval(Guid? id)
        {
            List<LeaveVM> leaves = (List<LeaveVM>)await _leaveRequestRepository.GetFilteredList(

              select: x => new LeaveVM()
              {
                  Id = x.Id,
                  FullName = x.AppUser.FullName,
                  StartDate = x.StartDate.ToShortDateString(),
                  EndDate = x.EndDate.ToShortDateString(),
                  LeaveDay = x.LeaveDay,
                  RequestComments = x.RequestComments,
                  ApproveStatus = x.ApproveStatus,
                  CreateDate = x.CreateDate.ToShortDateString(),
                  Status = x.Status,
                  AppUserId = x.AppUser.Id,
                  ExecutiveId = x.AppUser.ExecutiveId,
                  ExecutiveName = x.AppUser.Executive.FullName,
                  LeaveTypeId = x.LeaveType.Id,
                  LeaveTypeName = x.LeaveType.LeaveTypeName

              },
            where: x => (x.ApproveStatus == ApproveStatus.InApproval) && (x.Status != Status.Passive) && (x.AppUser.CompanyId == id),
            orderby: x => x.OrderByDescending(x => x.CreateDate),
            include: x => x.Include(x => x.AppUser)
              );
            return leaves;


        }

        public async Task<List<LeaveVM>> GetLeavesManagerAwatingApproval(Guid? id)
        {
            List<LeaveVM> leaves = (List<LeaveVM>)await _leaveRequestRepository.GetFilteredList(

               select: x => new LeaveVM()
               {
                   Id = x.Id,
                   FullName = x.AppUser.FullName,
                   StartDate = x.StartDate.ToShortDateString(),
                   EndDate = x.EndDate.ToShortDateString(),
                   LeaveDay = x.LeaveDay,
                   RequestComments = x.RequestComments,
                   ApproveStatus = x.ApproveStatus,
                   CreateDate = x.CreateDate.ToShortDateString(),
                   Status = x.Status,
                   AppUserId = x.AppUser.Id,
                   ExecutiveId = x.AppUser.ExecutiveId,
                   ExecutiveName = x.AppUser.Executive.FullName,
                   LeaveTypeId = x.LeaveType.Id,
                   LeaveTypeName = x.LeaveType.LeaveTypeName

               },
             where: x => (x.ApproveStatus == ApproveStatus.InApproval) && (x.Status != Status.Passive) && (x.AppUser.ExecutiveId == id),
             orderby: x => x.OrderByDescending(x => x.CreateDate),
             include: x => x.Include(x => x.AppUser)
               );
            return leaves;
        }
    }
}