//CONTROLLER UTK BUAT SMUA FUNGSI CRUD BAGI DEPARTMENT

using TestMongoDB.Models;
using TestMongoDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace TestMongoDB.Pages.Controller;

public class ControllerCode : ControllerBase
{
    private readonly DepartmentsServices _dept;

    public ControllerCode(DepartmentsServices dept) =>
        _dept = dept;

    public async Task<List<Department>> OnGetAsync() =>
        await _dept.GetAsync();

    public async Task<ActionResult<Department>> OnGetAsync(string id)
    {
        var dept = await _dept.GetAsync(id);

        if (dept is null)
        {
            return NotFound();
        }

        return dept;
    }


    public async Task<IActionResult> OnPostSaveAsync(Department newDept)
    {
        await _dept.CreateAsync(newDept);

        return CreatedAtAction(nameof(Get), new { id = newDept.Id }, newDept);
    }


    public async Task<IActionResult> OnPostUpdateAsync(string id, Department updatedDept)
    {
        var dept = await _dept.GetAsync(id);

        if (dept is null)
        {
            return NotFound();
        }

        updatedDept.Id = dept.Id;

        await _dept.UpdateAsync(id, updatedDept);

        return NoContent();
    }


    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        var dept = await _dept.GetAsync(id);

        if (dept is null)
        {
            return NotFound();
        }

        await _dept.RemoveAsync(id);

        return NoContent();
    }
}