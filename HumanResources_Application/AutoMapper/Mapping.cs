using AutoMapper;
using HumanResources_Application.Models.DTOs.AdvanceDTO;
using HumanResources_Application.Models.DTOs.AppUserDTOs;
using HumanResources_Application.Models.DTOs.CompanyDTOs;
using HumanResources_Application.Models.DTOs.ExpenseDTOs;
using HumanResources_Application.Models.DTOs.ExpenseTypeDTOs;
using HumanResources_Application.Models.DTOs.LeaveDTOs;
using HumanResources_Application.Models.DTOs.LeaveTypeDTOs;
using HumanResources_Application.Models.VMs.AddressVMs;
using HumanResources_Application.Models.VMs.AdvanceVMs;
using HumanResources_Application.Models.VMs.AppUserVMs;
using HumanResources_Application.Models.VMs.CompanyVMS;
using HumanResources_Application.Models.VMs.DepartmentVMs;
using HumanResources_Application.Models.VMs.ExpenseTypeVMs;
using HumanResources_Application.Models.VMs.ExpenseVMs;
using HumanResources_Application.Models.VMs.LeaveTypeVMs;
using HumanResources_Application.Models.VMs.LeaveVMs;
using HumanResources_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, UpdateProfileDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, CreateEmployeeDTO>().ReverseMap();

            CreateMap<AppUser, EmployeeVM>().ReverseMap();

            CreateMap<Department, DepartmentVM>().ReverseMap();

            CreateMap<City, CityVM>().ReverseMap();
            CreateMap<City, DistrictVM>().ReverseMap();

            CreateMap<Advance, AdvanceVM>().ReverseMap();
            CreateMap<Advance, CreateAdvanceDTO>().ReverseMap();
            CreateMap<Advance, UpdateAdvanceDTO>().ReverseMap();

            CreateMap<Company, RegisterDTO>().ReverseMap();
            CreateMap<Company, CompanyUpdateDTO>().ReverseMap();
            CreateMap<CompanyVM, CompanyUpdateDTO>().ReverseMap();
            CreateMap<Company, CompanyVM>().ReverseMap();


            CreateMap<Expense, ExpenseVM>().ReverseMap();
            CreateMap<Expense, CreateExpenseDTO>().ReverseMap();
            CreateMap<Expense, UpdateExpenseDTO>().ReverseMap();

            CreateMap<ExpenseType, ExpenseTypeVM>().ReverseMap();
            CreateMap<ExpenseType, CreateExpenseTypeDTO>().ReverseMap();
            CreateMap<ExpenseType, UpdateExpenseTypeDTO>().ReverseMap();


            CreateMap<LeaveRequest, LeaveVM>().ReverseMap();
            CreateMap<LeaveRequest, CreateLeaveDTO>().ReverseMap();
            CreateMap<LeaveRequest, UpdateLeaveDTO>().ReverseMap();

            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveType, CreateLeaveTypeDTO>().ReverseMap();
            CreateMap<LeaveType, UpdateLeaveTypeDTO>().ReverseMap();



        }
    }
}
