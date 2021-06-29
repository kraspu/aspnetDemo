using AutoMapper;
using WebApi.Controllers;
using WebApi.Data.Entities;
using WebApi.Models;

namespace WebApi.Infrastructure
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<InvoiceModel, Invoice>();
            CreateMap<InvoiceLineModel, InvoiceLine>();
        }
    }
}
