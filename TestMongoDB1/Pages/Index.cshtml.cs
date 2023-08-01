using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestMongoDB.Models;
using TestMongoDB.Services;

namespace TestMongoDB.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly DepartmentsServices _depts;
    
    public IList<Department>Departments {get; set;}

    [BindProperty]
    public Department Department {get; set;}

    public IndexModel(ILogger<IndexModel> logger, DepartmentsServices depts)
    {
        _logger = logger;  // logger : cth user login utk access (seseuai utk audit)
        _depts = depts;
        Departments = new List<Department>();
        Department = new Department();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Departments = await _depts.GetAsync();
        return Page();
    }

    public async Task <JsonResult> OnGetDeptAsync(string id)
    {
        var dept = await _depts.GetAsync(id);
        if (dept ==null)
        {
            dept = new Department();
        }
        
        return new JsonResult (dept);
    }

    public async Task<JsonResult> OnPostSaveAsync()  //utk save add dan update
    {
        var isUpdate = false;

        if (!String.IsNullOrEmpty(Department.Id))    // || utk or  ! (if notS)
        {
            await _depts.UpdateAsync(Department.Id,Department);
            isUpdate = true;
        }
        else
        {
            await _depts.CreateAsync(Department);
        }

        var result = new { Status = "Ok", Message = "Department is saved!", DeptId = Department.Id, IsUpdate = isUpdate };

        return new JsonResult(result);
    }

    public async Task<JsonResult> OnPostDeleteAsync() // utk delete
    {
        var status = "Ok";
        var msg = "Department information is deleted.";
        var deletedId = Department.Id;

        if (!String.IsNullOrEmpty(Department.Id))    
        {
            await _depts.RemoveAsync(Department.Id);
        }
        else
        {
            status ="Error";
            msg ="Invalid action";
        }

        var result = new { Status = status, Message = msg, DeptId = deletedId };

        return new JsonResult(result);
    }
}
