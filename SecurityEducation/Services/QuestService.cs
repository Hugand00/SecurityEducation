using System;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
using SecurityEducation.Dtos;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace SecurityEducation.Services
{
	
	public class QuestService : IDocument
	{
		private readonly byte[] imageData;
		private readonly string _name;
		private readonly int _amountOfStars;
		private readonly List<ChapterDto> _chapters;
		private readonly List<EpisodeDto> _episodes;
		
		
		private readonly DateOnly _date;
		public QuestService(string name, int amountOfStars, string chapters, string episodes)
		{
			_name = name;
			_amountOfStars = amountOfStars;
			_chapters = JsonSerializer.Deserialize<List<ChapterDto>>(chapters) ?? new List<ChapterDto>();
			_episodes = JsonSerializer.Deserialize<List<EpisodeDto>>(episodes) ?? new List<EpisodeDto>();
			_date = DateOnly.FromDateTime(DateTime.Now);

			var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Kottemedbådetummarupp.png");
			imageData = File.ReadAllBytes(imagePath);
		
		}
		public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
		public List<string> GetEpisodeNames(int chapterId, List<EpisodeDto> episodes)
		{
			List<string> result = new List<string>();
			foreach (var episode in episodes)
			{
				if(episode.ChapterId == chapterId)
				{
					result.Add(episode.Name);
				}
			}
			return result;
		}

		public void Compose(IDocumentContainer container)
		{
			container.Page(page =>
			{
				page.Size(PageSizes.A4);
				page.Margin(0);
				page.PageColor(Colors.BlueGrey.Lighten3); 

				
				page.Content()
					.Padding(10)
					.Border(2)
					.BorderColor(Colors.Grey.Darken2)
					.Background(Colors.White) 
					.Column(column =>
					{
						column.Spacing(10);

						column.Item().AlignCenter().Text("Intyg")
							.FontSize(24).SemiBold().FontColor(Colors.Blue.Darken2);

						column.Item().PaddingTop(20).AlignCenter().Text($"{_name} ")
							.FontSize(20);
						column.Item().AlignCenter().Text("har klarat utbildningen inom cyber-, informationssäkerhet och dataskydd samt AI")
							.FontSize(14).Italic().AlignCenter();
						column.Item().PaddingTop(2).AlignCenter().Text($"Utfärdat: {_date:dd MMMM yyyy}")
						.FontSize(14).FontColor(Colors.Black);



						column.Item().PaddingTop(30).AlignCenter().Text($"Totalt antal stjärnor: {_amountOfStars}/{GetAllStarsAmount()}")
							.FontSize(16).FontColor(Colors.Black);

						column.Item().PaddingTop(200).PaddingLeft(10).Text("Utbildningen har bestått av följande delar:").SemiBold().FontSize(14);

						column.Item().PaddingLeft(10).Grid(grid =>
						{
							int columnsCount = _chapters.Count;
							
							grid.Columns(columnsCount);

							foreach (var chapter in _chapters)
							{
								List<string> episodeNames = GetEpisodeNames(chapter.Id, _episodes);

								grid.Item().AlignCenter().Column(col =>
								{
									col.Spacing(10);
									col.Item().MinHeight(50).Text(chapter.Name).Italic().FontSize(14);
									col.Item().Column(col2 =>
									{
										col2.Spacing(5);
										foreach (var episode in episodeNames)
										{
											col2.Item().Text(episode).FontSize(11).Italic();
										}
									});
								});
							}
						});


					});

				page.Footer()
	.AlignCenter()
	.Column(column =>
	{
		column.Item().PaddingBottom(2).Text("Säkerhetsutbildning")
			.FontSize(12).Italic().AlignCenter();
	});
			});
		}
		public byte[] GeneratePdfBytes()
		{
			using var stream = new MemoryStream();
			Document.Create(Compose).GeneratePdf(stream);
			return stream.ToArray();
		}
		public int GetAllStarsAmount()
		{
			int episodeStars = 0;
			foreach (var item in _episodes)
			{
				episodeStars += 5;
			}
			episodeStars += 5;
			return episodeStars;
		}


	}
	

	}

