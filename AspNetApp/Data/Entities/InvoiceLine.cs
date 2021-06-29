namespace WebApi.Data.Entities
{
    public class InvoiceLine
    {
        public long InvoiceLineId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public long InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}