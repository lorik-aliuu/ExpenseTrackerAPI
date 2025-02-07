using AutoMapper;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Dtos;
using Microsoft.AspNetCore.Components.Web;
using System.Linq.Expressions;

namespace ExpenseTrackerAPI.Services.MappingProfiler
{
    public class MappingProfiler: Profile
    {

        public MappingProfiler()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
