using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCV_Demo.Models;
namespace MyCV_Demo.Controllers
{
    public class TaxController : Controller
    {
        // GET: TaxController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CalculatePage(TaxInputModel input)
        {
            if (!ModelState.IsValid) 
            {
                return View("Index", input);
            }
            var result = CalculateTax(input);
            return View("Calculated", result);
        }
        public TaxResultModel CalculateTax(TaxInputModel tm)
        {
            double tax = 0.0;
            double income = tm.AnnualIncome;
            double personalAllowance = 12570;

            if(income <= personalAllowance)
            {
                tax = 0.0;
            }
            else if (income <= 50270)
            {
                tax = (income - personalAllowance) * 0.20;
            }
            else if (income <= 125140)
            {
                tax = (50270 - personalAllowance) * 0.20 + (income - 50270) * 0.40;
            }
            else
            {
                tax = (50270 - personalAllowance) * 0.20 + (125140 - 50270) * 0.40 + (income - 125140) * 0.45;
            }
            return new TaxResultModel
            {
                Tax = tax
            };
        }

    }
}
