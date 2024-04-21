using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace BackendLab01;


public class QuizModel : PageModel
{
    private readonly IQuizUserService _userService;

    private readonly ILogger _logger;
    public QuizModel(IQuizUserService userService, ILogger<QuizModel> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    [BindProperty]
    public string Question { get; set; }
    [BindProperty]
    public List<string> Answers { get; set; }

    [BindProperty]
    public String UserAnswer { get; set; }

    [BindProperty]
    public int QuizId { get; set; }

    [BindProperty]
    public int ItemId { get; set; }

    private int answers;

    public void OnGet(int quizId, int itemId)
    {
        QuizId = quizId;
        ItemId = itemId;
        var quiz = _userService.FindQuizById(quizId);
        var quizItem = quiz?.Items[itemId - 1];
        Question = quizItem?.Question;
        Answers = new List<string>();
        if (quizItem is not null)
        {
            Answers.AddRange(quizItem?.IncorrectAnswers);
            Answers.Add(quizItem?.CorrectAnswer);
        }
    }

    public IActionResult OnPost()
    {
        _userService.SaveUserAnswerForQuiz(QuizId, 0, ItemId, UserAnswer);
        if (_userService.FindQuizById(QuizId).Items.Count == ItemId)
        {
            var correctAnswers = _userService.CountCorrectAnswersForQuizFilledByUser(QuizId, 0);
            return RedirectToPage("Summary", new { quizId = QuizId, correctAnswers });
        }
        return RedirectToPage("Item", new { quizId = QuizId, itemId = ItemId + 1 });

    }
}