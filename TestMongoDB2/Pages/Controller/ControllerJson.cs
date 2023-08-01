using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestMongoDB.Models;
using TestMongoDB.Services;

namespace TestMongoDB.Pages.Controller;

public class ControllerJson: ControllerBase
{
    private readonly DepartmentsServices _dept;
    public IList<Department> Departments { get; set; }

    [BindProperty]
    public Department Department { get; set; }

    public ControllerJson(ILogger<IndexModel> logger, DepartmentsServices dept)
    {
        _dept = dept;
        _logger = logger;
        Departments = new List<Department>();
        Department = new Department();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Departments = await _depts.GetAsync();
        return Page();
    }

    public async Task<JsonResult> OnGetDeptAsync(string id)
    {
        var dept = await _depts.GetAsync(id);
        if(dept == null)
        {
            dept = new Department();
        }

        return new JsonResult(dept);
    }

    public async Task<JsonResult> OnPostSaveAsync()
    {
        if(!String.IsNullOrEmpty(Department.Id))
        {
            await _depts.UpdateAsync(Department.Id, Department);
        }
        else
        {
            await _depts.CreateAsync(Department);
        }

        var result = new { Status = "Ok", Message = "Department information  is saved!" };
        return new JsonResult(result);
    }

    public async Task<JsonResult> OnPostDeleteAsync()
    {
        var status = "OK";
        var msg = "Department information is deleted";

        if(!String.IsNullOrEmpty(Department.Id))
        {
            await _depts.RemoveAsync(Department.Id);
        }
        else
        {
            status = "Error";
            msg = "Invalid action";
        }

        var result = new { Status = status, Message = msg };
        return new JsonResult(result);
    }
}