using ApplicationCore.Interfaces.AdminService;
using ApplicationCore.Models.QuizAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01;

public class Quizes : PageModel
{
    private readonly IQuizAdminService _adminService;

    public Quizes(IQuizAdminService adminService)
    {
        _adminService = adminService;
    }
    [BindProperty]
    public List<Quiz> Answers { get; set; }
    public void OnGet()
    {
        Answers = (List<Quiz>)_adminService.FindAllQuizzes();
    }
}