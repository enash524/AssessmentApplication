namespace AssessmentApplication.Data.Dto
{
    public class SalesOrderDetailDto
    {
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public short OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal { get; set; }
    }
}
