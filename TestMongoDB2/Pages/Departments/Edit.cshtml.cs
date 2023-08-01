using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestMongoDB.Models;
using TestMongoDB.Services;

namespace TestMongoDB.Pages.Departments;

public class EditModel : PageModel
{
    private readonly ILogger<EditModel> _logger;
    private readonly DepartmentsServices _dept;
    
    [BindProperty]
    public Department Input { get; set; }

    public EditModel(ILogger<EditModel> logger, DepartmentsServices dept)
    {
        _dept = dept;
        _logger = logger;
        Input = new Department();
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        Input = await _dept.GetAsync(id);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        await _dept.UpdateAsync(id,Input);
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        await _dept.RemoveAsync(id);
        return RedirectToPage("Index");
    }
}

