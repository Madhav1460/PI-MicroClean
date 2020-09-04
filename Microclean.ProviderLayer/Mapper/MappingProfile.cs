using AutoMapper;
using Microclean.CommandQueryLayer.Commands;
using Microclean.DataModel.Models;
using Microclean.ProviderLayer.Handlers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.ProviderLayer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            _ = CreateMap<FranchiseeItSelfRegistrationCommand, Tbluser>().ReverseMap();
            _ = CreateMap<UpdateUserCommand, Tbluser>().ReverseMap();
            //_ = CreateMap<CreateNewUserCommand, Tbluser>().ReverseMap();
        }
    }
}
