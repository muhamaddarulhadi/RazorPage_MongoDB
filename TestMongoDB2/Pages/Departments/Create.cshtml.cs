using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestMongoDB.Models;
using TestMongoDB.Services;

namespace TestMongoDB.Pages.Departments;

public class CreateModel : PageModel
{
    private readonly ILogger<CreateModel> _logger;
    private readonly DepartmentsServices _dept;

     [BindProperty]
    public Department Input { get; set; }

    public CreateModel(ILogger<CreateModel> logger, DepartmentsServices dept)
    {
        _dept = dept;
        _logger = logger;
        Input = new Department();
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _dept.CreateAsync(Input);
        return RedirectToPage("Index");
    }
}

