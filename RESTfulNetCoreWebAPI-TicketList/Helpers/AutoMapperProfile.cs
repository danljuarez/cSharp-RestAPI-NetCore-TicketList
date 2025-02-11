using AutoMapper;
using RESTfulNetCoreWebAPI_TicketList.Models;
using RESTfulNetCoreWebAPI_TicketList.Models.Request;
using System.Diagnostics.CodeAnalysis;

namespace RESTfulNetCoreWebAPI_TicketList.Helpers
{
    public class AutoMapperProfile : Profile
    {
        [ExcludeFromCodeCoverage]
        public AutoMapperProfile()
        {
            CreateMap<TicketInputDTO, Ticket>();
        }
    }
}
