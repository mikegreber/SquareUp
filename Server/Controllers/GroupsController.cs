using Microsoft.AspNetCore.Mvc;
using SquareUp.Server.Models;
using SquareUp.Server.Services.Groups;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using static SquareUp.Shared.ControllerEndpoints.Groups;

namespace SquareUp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _service;

    public GroupsController(IGroupService service)
    {
        _service = service;
    }


    [HttpGet(GetAllPath)]
    public async Task<ActionResult<ServiceResponse<List<GroupClient>>>> GetGroups()
    {
        var result = await _service.GetGroups();
        return Ok(result);
    }

    [HttpGet(GetGroupById)]
    public async Task<ActionResult<ServiceResponse<GroupClient>>> GetGroup(int id)
    {
        var result = await _service.GetGroup(id);
        return Ok(result);
    }


    [HttpPost(PostAddGroup)]
    public async Task<ActionResult<ServiceResponse<GroupClient>>> AddGroup(AddGroupRequest request)
    {
        var result = await _service.AddGroup(request);
        return Ok(result);
    }

    [HttpPost(PostAddExpense)]
    public async Task<ActionResult<ServiceResponse<Expense>>> AddExpense(AddExpenseRequest request)
    {
        var result = await _service.AddExpense(request);
        return Ok(result);
    }

    [HttpPost(PostAddUser)]
    public async Task<ActionResult<ServiceResponse<User>>> AddUser(AddUserRequest request)
    {
        var result = await _service.AddUser(request);
        return Ok(result);
    }

}