using System;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
namespace SecurityEducation.Services
{
	
	public class QuestService : IDocument
	{
		private readonly byte[] imageData;
		private readonly string _name;
		private readonly int _amountOfStars;
		private readonly List<string> _chapters;
		private readonly DateOnly _date;
		public QuestService(string name, int amountOfStars, List<string> chapters)
		{
			_name = name;
			_amountOfStars = amountOfStars;
			_chapters = chapters;
			_date = DateOnly.FromDateTime(DateTime.Now);

			var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Kottemedbådetummarupp.png");
			imageData = File.ReadAllBytes(imagePath);
		}
		public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

		
		public void Compose(IDocumentContainer container)
		{
			container.Page(page =>
			{
				page.Size(PageSizes.A4);
				page.Margin(0);
				page.PageColor(Colors.BlueGrey.Lighten3); // Diplombakgrund

				page.Content()
					.Padding(30)
					.Border(3)
					.BorderColor(Colors.Grey.Darken2)
					.Background(Colors.White) // Vitt "papper" i mitten
					.Column(column =>
					{
						column.Spacing(20);

						column.Item().AlignCenter().Text("Intyg")
							.FontSize(36).SemiBold().FontColor(Colors.Blue.Darken2);

						column.Item().AlignCenter().Text($"{_name} har klarat denna utbildning inom cyber, informationssäkerhet och dataskydd samt AI")
							.FontSize(18).Italic().AlignCenter();

						column.Item().AlignCenter().Container().MaxWidth(300).Image(imageData).FitWidth();

						column.Item().AlignCenter().Text($"Totalt antal stjärnor: {_amountOfStars}/65")
							.FontSize(16).FontColor(Colors.Grey.Darken2);

						column.Item().PaddingTop(20).PaddingLeft(10).Text("Utbildningen har bestått av följande delar:").SemiBold().FontSize(18);

						column.Item().PaddingLeft(10).Row(row =>
						{
							row.Spacing(20);
							foreach (var chapterName in _chapters)
							{
								row.AutoItem().Text(chapterName).Italic().FontSize(16);
							}
						});

						column.Item().PaddingTop(40).AlignCenter().Text($"Utfärdat: {_date:dd MMMM yyyy}")
							.FontSize(14).FontColor(Colors.Grey.Darken1);
					});

				page.Footer()
					.AlignCenter()
					.Text("Security Education Academy").FontSize(12).Italic();
			});
		}
		public byte[] GeneratePdfBytes()
		{
			using var stream = new MemoryStream();
			Document.Create(Compose).GeneratePdf(stream);
			return stream.ToArray();
		}
	}
}
