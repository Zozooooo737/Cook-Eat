namespace Cook_Eat.models
{
    public class DishPreparation
    {
        public int PreparationId { get; set; }
        public bool IsPublished { get; set; }
        public DateTime PreparationDate { get; set; }
        public DateTime PeremptionDate { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPerson { get; set; }
        public int DishId { get; set; }
    }
}