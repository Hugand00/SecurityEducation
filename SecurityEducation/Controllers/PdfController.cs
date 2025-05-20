using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using SecurityEducation.Services;

namespace SecurityEducation.Controllers
{
	public class PdfController : Controller
	{
		public IActionResult GeneratePdf()
		{
			var document = new QuestService(); // din QuestPDF-klass

			byte[] pdfBytes = document.GeneratePdf();

			return File(pdfBytes, "application/pdf", "Intyg.pdf");
		}
	}
}
