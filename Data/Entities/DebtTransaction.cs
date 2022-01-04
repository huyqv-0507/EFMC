using System;
namespace Data.Entities
{
    public class DebtTransaction
    {
        public int DebtTransactionId { get; set; }
        public int PrescriptionId { get; set; }
        public string NamePayback { get; set; }
        public decimal MoneyPayback { get; set; }
        public DateTime DatePayback { get; set; }
    }
}
