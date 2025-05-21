using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using SecurityEducation.Services;
using System.Security.Cryptography.X509Certificates;

namespace SecurityEducation.Controllers
{
	public class PdfController : Controller
	{
		public IActionResult GeneratePdf(string Name, int AmountOfStars, string Chapters)
		{
			var chapters = Chapters.Split(",").ToList();
			var questService = new QuestService(Name, AmountOfStars, chapters); 

			byte[] pdfBytes = questService.GeneratePdfBytes(); ;

			return File(pdfBytes, "application/pdf", "Intyg.pdf");
		}
	}
}
