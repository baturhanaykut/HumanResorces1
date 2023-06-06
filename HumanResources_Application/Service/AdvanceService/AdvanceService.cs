using AutoMapper;
using HumanResources_Application.Models.DTOs.AdvanceDTO;
using HumanResources_Application.Models.VMs.AdvanceVMs;
using HumanResources_Application.Service.AppUserServices;
using HumanResources_Domain.Entities;
using HumanResources_Domain.Enums;
using HumanResources_Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.AdvanceService
{
    public class AdvanceService : IAdvanceService
    {
        private readonly IAdvanceRepository _advanceRepository;
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppUserService _appUserService;



        public AdvanceService(IAdvanceRepository advanceRepository, IMapper mapper, IAppUserRepository appUserRepository, IAppUserService appUserService)
        {
            _advanceRepository = advanceRepository;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _appUserService = appUserService;
        }
        public async Task<bool> Create(CreateAdvanceDTO model, string userName)
        {
            Advance advance = _mapper.Map<Advance>(model);
            advance.AppUserId = await _appUserService.GetUserId(userName);
            return await _advanceRepository.Add(advance);
        }
        public async Task Delete(int id)
        {
            Advance deletedAdvance = await _advanceRepository.GetDefault(x => x.Id == id);
            if (deletedAdvance != null && deletedAdvance.ApproveStatus == ApproveStatus.InApproval)
            {
                deletedAdvance.Status = Status.Passive;
                deletedAdvance.DeleteDate = DateTime.Now;
                await _advanceRepository.Delete(deletedAdvance);
            }
        }
        public async Task Update(UpdateAdvanceDTO model)
        {
            var advance = _mapper.Map<Advance>(model);
            advance.CreateDate = DateTime.Now;
            if (advance != null)
            {
                await _advanceRepository.Update(advance);
            }
        }
        public async Task<UpdateAdvanceDTO> GetByID(int id)
        {
            Advance advance = await _advanceRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateAdvanceDTO>(advance);

            return model;
        }
        public async Task<List<AdvanceVM>> GetAdvances(Guid? id)
        {

            List<AdvanceVM> advances = (List<AdvanceVM>)await _advanceRepository.GetFilteredList(

              select: x => new AdvanceVM()
              {
                  Id = x.Id,
                  FullName = x.AppUser.FullName,
                  AdvanceAmount = x.AdvanceAmount,
                  ApproveStatus = x.ApproveStatus,
                  CreateDate = x.CreateDate.ToShortDateString(),
                  Status = x.Status,
                  Explanation = x.Explanation,
                  PaymentDueDate = x.PaymentDueDate.ToShortDateString(),
                  ExecutiveId = x.AppUser.ExecutiveId,
                  ExecutiveName = x.AppUser.Executive.FullName,
              },
             where: x => (x.Status != Status.Passive) && (x.AppUser.ExecutiveId == id),
             orderby: x => x.OrderByDescending(x => x.CreateDate),
             include: x => x.Include(x => x.AppUser));
            return advances;
        }

        public async Task<List<AdvanceVM>> GetAdvancesCompanyManager(Guid? id)
        {

            List<AdvanceVM> advances = (List<AdvanceVM>)await _advanceRepository.GetFilteredList(

              select: x => new AdvanceVM()
              {
                  Id = x.Id,
                  FullName = x.AppUser.FullName,
                  AdvanceAmount = x.AdvanceAmount,
                  ApproveStatus = x.ApproveStatus,
                  CreateDate = x.CreateDate.ToShortDateString(),
                  Status = x.Status,
                  Explanation = x.Explanation,
                  PaymentDueDate = x.PaymentDueDate.ToShortDateString(),
                  ExecutiveId = x.AppUser.ExecutiveId,
                  ExecutiveName = x.AppUser.Executive.FullName,

              },
             where: x => (x.Status != Status.Passive) && (x.AppUser.CompanyId == id),
             orderby: x => x.OrderByDescending(x => x.CreateDate),
             include: x => x.Include(x => x.AppUser));
            return advances;
        }

        public async Task<AdvanceVM> GetAdvanceDetails(int id)
        {
            var advance = await _advanceRepository.GetFiltredFirstOrDefault(
                select: x => new AdvanceVM()
                {
                    Id = x.Id,
                    Explanation = x.Explanation,

                    ApproveStatus = x.ApproveStatus,
                    CreateDate = x.CreateDate.ToShortDateString(),
                    Status = x.Status,
                    AppUserId = x.AppUser.Id,


                },
                where: (x => x.Id == id),
                orderby: null,
                include: x => x.Include(x => x.AppUser));

            return advance;
        }
        public async Task<List<AdvanceVM>> GetPersonelAdvances(string userName)
        {
            var advances = await _advanceRepository.GetFilteredList(
            select: x => new AdvanceVM()
            {
                Id = x.Id,
                AdvanceAmount = x.AdvanceAmount,
                ApproveStatus = x.ApproveStatus,
                CreateDate = x.CreateDate.ToShortDateString(),
                Status = x.Status,
                Explanation = x.Explanation,
                PaymentDueDate = x.PaymentDueDate.ToShortDateString(),
                ExecutiveId = x.AppUser.ExecutiveId,
                ExecutiveName = x.AppUser.Executive.FullName,
            },
                where: x => x.AppUser.UserName == userName && x.Status != Status.Passive,
                orderby: x => x.OrderByDescending(x => x.CreateDate),
                include: x => x.Include(x => x.AppUser));

            return (List<AdvanceVM>)advances;

        }

        public async Task Accept(int id)
        {
            Advance advance = await _advanceRepository.GetDefault(x => x.Id == id);
            advance.ApproveStatus = ApproveStatus.Approved;
            await _advanceRepository.Update(advance);
        }

        public async Task Reject(int id)
        {
            Advance advance = await _advanceRepository.GetDefault(x => x.Id == id);
            advance.ApproveStatus = ApproveStatus.Denied;
            await _advanceRepository.Update(advance);
        }

        public async Task<List<AdvanceVM>> GetAdvancesCompanyManagerAwatingApproval(Guid? id)
        {

            List<AdvanceVM> advances = (List<AdvanceVM>)await _advanceRepository.GetFilteredList(

              select: x => new AdvanceVM()
              {
                  Id = x.Id,
                  FullName = x.AppUser.FullName,
                  AdvanceAmount = x.AdvanceAmount,
                  ApproveStatus = x.ApproveStatus,
                  CreateDate = x.CreateDate.ToShortDateString(),
                  Status = x.Status,
                  Explanation = x.Explanation,
                  PaymentDueDate = x.PaymentDueDate.ToShortDateString(),
                  ExecutiveId = x.AppUser.ExecutiveId,
                  ExecutiveName = x.AppUser.Executive.FullName,

              },
             where: x => (x.ApproveStatus == ApproveStatus.InApproval) && (x.Status != Status.Passive) && (x.AppUser.CompanyId == id),
             orderby: x => x.OrderByDescending(x => x.CreateDate),
             include: x => x.Include(x => x.AppUser));
            return advances;
        }

        public async Task<List<AdvanceVM>> GetAdvancesManagerAwatingApproval(Guid? id)
        {

            List<AdvanceVM> advances = (List<AdvanceVM>)await _advanceRepository.GetFilteredList(

              select: x => new AdvanceVM()
              {
                  Id = x.Id,
                  FullName = x.AppUser.FullName,
                  AdvanceAmount = x.AdvanceAmount,
                  ApproveStatus = x.ApproveStatus,
                  CreateDate = x.CreateDate.ToShortDateString(),
                  Status = x.Status,
                  Explanation = x.Explanation,
                  PaymentDueDate = x.PaymentDueDate.ToShortDateString(),
                  ExecutiveId = x.AppUser.ExecutiveId,
                  ExecutiveName = x.AppUser.Executive.FullName,

              },
             where: x => (x.ApproveStatus == ApproveStatus.InApproval) && (x.Status != Status.Passive) && (x.AppUser.ExecutiveId == id),
             orderby: x => x.OrderByDescending(x => x.CreateDate),
             include: x => x.Include(x => x.AppUser));
            return advances;
        }

    }
}
