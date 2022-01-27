using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PresciptionTextSharp.Models;
using PresciptionTextSharp.PdfCreators;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PresciptionTextSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            var doctor = new Doctor
            {
                Name = "JUSTIN SAYA MD",
                Address = "4809 N ARMENIA AVENUE 220 TAMPA, FL 33603",
                Phone = "(813)445-7342",
                Fax = "(813)445 - 7340",
                DEA = "#FS3175545",
                NPI = "#1093940041",
                LICENSE = "#109185"
            };

            var patient = new Patient
            {
                Name = "GRZESKOWIAK, SUSAN",
                Address = "2048 3RD ST E SAINT PAUL, MN 55119",
                PhoneNumber = "(612) 210-2250 (Cell)",
                DOB = "01/11/1960 (61 yrs.)",
                Gender = "F",
                MRN = " ",
                Acct = "15807",
                Date = "01/04/2022"
            };

            var presciptionsList = new List<Presciption>
            {
              new Presciption {SIG=" Progesterone 150mg IR capsule (Hypo-allergenic Formulation, Sensitivity)",
                  Directions = "take one capsule by mouth before bed*medically necessary due to patient sensitivity",
                  Type = "Routine",
                  DX = "E29.1; SHIP GROUND; SIG WAIVED; *EMP*",
                  Pharmacist = "Use this information for uninsured patients.",
                  ID = "704-370-0006 rxBIN:016151 PCN:BNRX GRP:AMD",
              }
            };
            var pdfFile = PatientDefyPdf.GeneratePdf(doctor, patient, presciptionsList);
            return File(pdfFile, "application/pdf");
        }
    }
}
