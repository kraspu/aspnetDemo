using System;
using System.Collections.Generic;

namespace WebApi.Data.Entities
{
    public class Invoice
    {
        public long InvoiceId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Supplier { get; set; }
        public ICollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
    }
}