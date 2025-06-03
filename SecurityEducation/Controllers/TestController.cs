using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using SecurityEducation.Services.Interfaces;
using SecurityEducation.ViewModels;

namespace SecurityEducation.Controllers
{
    public class TestController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ITestService _testService;
        private readonly IChapterService _chapterService;
        private readonly IEpisodeService _episodeService;
        

        public TestController(IQuestionService questionService, ITestService testService, IChapterService chapterService, IEpisodeService episodeService)
        {
            _questionService = questionService;
            _testService = testService;  
            _chapterService = chapterService;
            _episodeService = episodeService;
        }
                

        [HttpGet("/Test/Questions/{chapterId}/{episodeId}/{testId}")]
		public async Task<IActionResult> Questions(int chapterId, int episodeId, int testId)        
        {
            
            var model = await _questionService.GetQuestionsByTestId(testId);
            ViewBag.ChapterId = chapterId;
            ViewBag.EpisodeId = episodeId;
            return View(model);
        }

        [HttpGet("/Test/Examination/")]
        public async Task<IActionResult> Examination()
        {
            var model = await _questionService.GetAllQuestions();

          
            var random = new Random();
            model.Questions = model.Questions.OrderBy(q => random.Next()).Take(10).ToList();

            
            model.Answers = model.Answers
                .Where(a => model.Questions.Select(q => q.Id).Contains(a.QuestionId))
                .ToList();

            return View(model);
        }


        [HttpGet("/Test/TestStartPage/{chapterId}/{episodeId}")]
		public async Task<IActionResult> TestStartPage(int chapterId, int episodeId)
        {
            var model = await _testService.GetTestInfoByEpisodeId(episodeId);
            ViewBag.ChapterId = chapterId;
            return View(model);
        }
        [HttpGet("/Test/Result/{chapterId}/{episodeId}")]
		public async Task<IActionResult> Result(int chapterId, int episodeId)
		{
            InfoViewModel model = new InfoViewModel();
			var chapteriInfo = await _chapterService.GetChapterFromId(chapterId);
			var episodeInfo = await _episodeService.GetEpisodeById(episodeId, chapterId);
			model.Chapter = chapteriInfo;
			model.Episode = episodeInfo;
			return View(model);
		}

        [HttpGet("/Test/ExaminationResult/")]
        public async Task<IActionResult> ExaminationResult(int chapterId, int episodeId)
        {
            InfoViewModel model = new InfoViewModel();
			var chapteriInfo = await _chapterService.GetChapterFromId(chapterId);
			var episodeInfo = await _episodeService.GetEpisodeById(episodeId, chapterId);
			model.Chapter = chapteriInfo;
			model.Episode = episodeInfo;
			var chapterData = await _chapterService.GetEveryChapter();

			foreach (var chapter in chapterData.Chapters)
			{
				model.Chapters.Add(chapter);
				var episodeData = await _episodeService.GetEpisodesByChapterId(chapter.Id);
				foreach (var episodes in episodeData.Episodes)
				{
					model.Episodes.Add(episodes);
				}
			}

			return View(model);
		
        }

    }
}
