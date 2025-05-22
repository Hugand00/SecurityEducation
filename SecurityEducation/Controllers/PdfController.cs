using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using SecurityEducation.Dtos;
using SecurityEducation.Services;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace SecurityEducation.Controllers
{
	public class PdfController : Controller
	{
		
		public IActionResult GeneratePdf(string Name, int AmountOfStars, string Chapters, string Episodes)
		{
			
			var questService = new QuestService(Name, AmountOfStars, Chapters, Episodes); 

			byte[] pdfBytes = questService.GeneratePdfBytes(); ;

			return File(pdfBytes, "application/pdf", "Intyg.pdf");
		}
	}
}
