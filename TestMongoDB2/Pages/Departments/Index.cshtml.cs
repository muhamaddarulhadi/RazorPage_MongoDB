using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestMongoDB.Models;
using TestMongoDB.Services;
using TestMongoDB.Controller.ControllerCode;

namespace TestMongoDB.Pages.Departments;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly DepartmentsServices _dept;
    public IList<Department> Departments { get; set; }

    public IndexModel(ILogger<IndexModel> logger, DepartmentsServices dept)
    {
        _dept = dept;
        _logger = logger;
        Departments = new List<Department>();
    }


    public async Task<IActionResult> OnGetAsync()
    {
        Departments = await _dept.GetAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        await _dept.RemoveAsync(id);
        return RedirectToPage("Index");
    }
}
