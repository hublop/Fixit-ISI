using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Common.Services.Mail;
using Fixit.Application.Common.Services.Sms;
using Fixit.Application.Orders.Events.DirectOrderCreated;
using Fixit.EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Orders.Events.DistributedOrderSent
{
  public class DistributedOrderSentToContractorEventHandler : IIntegrationEventHandler<DistributedOrderSentToContractorEvent>
  {
      private readonly IFixitDbContext _dbContext;
      private readonly IMailService _mailService;
      private readonly ISmsService _smsService;

      public DistributedOrderSentToContractorEventHandler(IFixitDbContext dbContext, IMailService mailService, ISmsService smsService)
      {
          _dbContext = dbContext;
          _mailService = mailService;
          _smsService = smsService;
      }

      public async Task Handle(DistributedOrderSentToContractorEvent @event)
      {
          var order = await _dbContext.Orders
              .Include(x => x.Subcategory)
              .Include(x => x.Customer)
              .FirstOrDefaultAsync(x => x.Id == @event.OrderId);

          var contractor = await _dbContext.Contractors
              .SingleOrDefaultAsync(x => x.Id == @event.ContractorId);

          if (order == null)
          {
              return;
          }

          // Just example message...
          var message = $"Customer requested service from you. Customer email: " +
                        $"{order.Customer.Email}, " +
                        $"phone: {order.Customer.PhoneNumber}, " +
                        $"description: {order.Description}";

          await _mailService.SendEmailAsync(contractor.Email,
              $"Customer requested {order.Subcategory.Name} service.",
              message);

          await _smsService.SendSmsAsync(contractor.PhoneNumber, message);
      }
  }
}
