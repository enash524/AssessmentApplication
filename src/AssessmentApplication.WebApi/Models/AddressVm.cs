namespace AssessmentApplication.WebApi.Models
{
    public class AddressVm
    {
        public int AddressId { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string StateOrProvinceCode { get; set; }

        public string PostalCode { get; set; }
    }
}
