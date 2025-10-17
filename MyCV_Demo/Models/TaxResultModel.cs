using System.Security.Permissions;

namespace MyCV_Demo.Models
{
    public class TaxResultModel
    {
        public string CountryCode { get; set; } = "UK";
        public int TaxYear { get; set; } = DateTime.UtcNow.Year;
        public double Tax { get; set; }
    }
}
