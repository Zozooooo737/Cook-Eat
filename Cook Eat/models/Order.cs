namespace Cook_Eat.models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TransactionId { get; set; }
        public int AddressId { get; set; }
        public int PreparationId { get; set; }
    }
}