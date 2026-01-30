using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using System.Text.Json;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public QuizController(IWebHostEnvironment env)
        {
            _env = env;
        }

        private List<Question> LoadQuestions()
        {
            var path = Path.Combine(_env.ContentRootPath, "Data", "questions.json");

            if (!System.IO.File.Exists(path))
                return new List<Question>();

            var json = System.IO.File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<Question>>(json)
                   ?? new List<Question>();
        }

        public IActionResult Index()
        {
            return View(new QuizViewModel
            {
                Questions = LoadQuestions()
            });
        }

        [HttpPost]
        public IActionResult Submit(QuizViewModel model)
        {
            model.Questions = LoadQuestions();
            int score = 0;

            foreach (var q in model.Questions)
            {
                if (model.Answers.ContainsKey(q.Id) &&
                    model.Answers[q.Id] == q.CorrectIndex)
                {
                    score++;
                }
            }

            model.Score = score;
            return View("Result", model);
        }
    }
}
