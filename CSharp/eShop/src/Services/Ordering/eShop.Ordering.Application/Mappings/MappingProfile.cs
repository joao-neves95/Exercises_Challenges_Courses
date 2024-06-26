using eShop.Ordering.Application.Features.Order.Commands.CheckoutOrder;
using eShop.Ordering.Application.Features.Order.Commands.UpdateOrder;
using eShop.Ordering.Application.Features.Order.Queries.GetOrdersList;
using eShop.Ordering.Domain.Entities;
using eShop.Shared.EventBus.Messages.Events;

using AutoMapper;

namespace eShop.Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();

            CreateMap<DataOrder, OrderDto>().ReverseMap();
            CreateMap<DataBillingInformation, BillingInformationDto>().ReverseMap();
            CreateMap<DataPaymentInformation, PaymentInformationDto>().ReverseMap();

            CreateMap<CheckoutOrderCommand, DataOrder>()
               .ConvertUsing((comm, _) => new DataOrder()
               {
                   UserName = comm.UserName,
                   TotalPrice = comm.TotalPrice,
                   BillingInformation = new DataBillingInformation()
                   {
                       AddressLine = comm.AddressLine,
                       Country = comm.Country,
                       EmailAddress = comm.EmailAddress,
                       FirstName = comm.FirstName,
                       LastName = comm.LastName,
                       State = comm.State,
                       ZipCode = comm.ZipCode,
                   },
                   PaymentInformation = new DataPaymentInformation()
                   {
                       CardName = comm.CardName,
                       CardNumber = comm.CardNumber,
                       CVV = comm.CVV,
                       Expiration = comm.Expiration,
                       PaymentMethod = comm.PaymentMethod,
                   },
               });

            CreateMap<UpdateOrderCommand, DataOrder>()
                .ConvertUsing((comm, _) => new DataOrder()
                {
                    Id = comm.Id,
                    UserName = comm.UserName,
                    TotalPrice = comm.TotalPrice,
                    BillingInformation = new DataBillingInformation()
                    {
                        AddressLine = comm.AddressLine,
                        Country = comm.Country,
                        EmailAddress = comm.EmailAddress,
                        FirstName = comm.FirstName,
                        LastName = comm.LastName,
                        State = comm.State,
                        ZipCode = comm.ZipCode,
                    },
                    PaymentInformation = new DataPaymentInformation()
                    {
                        CardName = comm.CardName,
                        CardNumber = comm.CardNumber,
                        CVV = comm.CVV,
                        Expiration = comm.Expiration,
                        PaymentMethod = comm.PaymentMethod,
                    },
                });
        }
    }
}
