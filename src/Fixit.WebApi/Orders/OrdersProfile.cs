using AutoMapper;
using Fixit.Application.Orders.Commands.AcceptOrder;
using Fixit.Application.Orders.Commands.CancelOrder;
using Fixit.Application.Orders.Commands.CreateDirectOrder;
using Fixit.Application.Orders.Commands.RejectOrder;
using Fixit.WebApi.Orders.DTOs;

namespace Fixit.WebApi.Orders
{
    public class OrdersProfile: Profile
    {
        public OrdersProfile()
        {
            CreateMap<CreateDirectOrderCommandDto, CreateDirectOrderCommand>();
            CreateMap<CancelOrderCommandDto, CancelOrderCommand>();
            CreateMap<AcceptOrderCommandDto, AcceptOrderCommand>();
            CreateMap<RejectOrderCommandDto, RejectOrderCommand>();
        }
    }
}
