using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
namespace SecurityEducation.Services
{
	
	public class QuestService : IDocument
	{
		private readonly byte[] imageData;
		public QuestService()
		{
			var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Kottemedbådetummarupp.png");
			imageData = File.ReadAllBytes(imagePath);
		}
		public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

		
		public void Compose(IDocumentContainer container)
		{
			container.Page(page =>
			{
				page.Size(PageSizes.A4);
				page.Margin(2, Unit.Centimetre);
				page.PageColor(Colors.White);
				page.DefaultTextStyle(x => x.FontSize(18));

				page.Content()
					.Column(column =>
					{
						column.Item().Text("Grattis du har klarat utbildningen!")
						.SemiBold().FontSize(28);
						column.Item().Text("Nedan följer ditt intyg");

						column.Item().Image(imageData);

						column.Item().Text("Du har klarat av denna utbildning och har nu förjänat detta intyg som ett bevis på dina kunskaper");
						
					});
			});
		}
	}
}
