using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly SqLiteDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceController(SqLiteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InvoiceModel model)
        {
            var invoice = _mapper.Map<Invoice>(model);
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> Send([FromBody] InvoiceModel model)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://jjioncnu:TsAiLAZekeftpUN8iFeLFMoMdtjBssoy@rat.rmq2.cloudamqp.com/jjioncnu")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            const string queueName = "queue1";

            channel.QueueDeclare(queueName, false, false, true, null);

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
            
            var exchangeName = "";
            var routingKey = queueName;
            channel.BasicPublish(exchangeName, routingKey, null, data);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _context.Invoices.Include(c => c.Lines).ToListAsync();
            return Ok(invoices);
        }
    }
}