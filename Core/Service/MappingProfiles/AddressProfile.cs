using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.IdentityModule;
using Shared.DataTransferObject.IdentityDto;

namespace Service.MappingProfiles
{
    public class AddressProfile:Profile
    {
        public AddressProfile() {
            CreateMap<Address,AddressDto>().ReverseMap()
                .ForMember(A=>A.FistName,Option=>Option.MapFrom(src=>src.FirstName));
        }
    }
}
