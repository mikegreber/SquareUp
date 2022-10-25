using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SquareUp.Server.Models;
using SquareUp.Server.Services.Groups;
using SquareUp.Shared.Models;
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

    [Authorize]
    [HttpGet("test")]
    public async Task<ServiceResponse<string>> Test() =>
        new (data: "String Result", message: "Here is the message.");

    [HttpGet(GetAllUri)]
    public async Task<ActionResult<ServiceResponse<List<Group>>>> GetGroups()
    {
        var result = await _service.GetGroups();
        return Ok(result);
    }

    [HttpGet(GetAllInfoUri)]
    public async Task<ActionResult<ServiceResponse<List<GroupInfo>>>> GetGroupsInfo()
    {
        var result = await _service.GetGroupsInfo();
        return Ok(result);
    }


    [HttpGet(GetGroupsByUserIdUri)]
    public async Task<ActionResult<ServiceResponse<Group>>> GetGroups(int id)
    {
        var result = await _service.GetUserGroups(Request);
        return Ok(result);
    }

    [HttpGet(GetGroupsInfoByUserIdUri)]
    public async Task<ActionResult<ServiceResponse<GroupInfo>>> GetGroupsInfo(int id)
    {
        var result = await _service.GetUserGroupsInfo(Request);
        return Ok(result);
    }

    [HttpGet(GetGroupByIdUri)]
    public async Task<ActionResult<ServiceResponse<Group>>> GetGroup(int id)
    {
        var result = await _service.GetGroup(Request, id);
        return Ok(result);
    }

    [HttpPost(PostAddGroupUri)]
    public async Task<ActionResult<ServiceResponse<GroupInfo>>> CreateGroup(GroupBase group)
    {
        var result = await _service.CreateGroup(Request, group);
        return Ok(result);
    }

    [HttpPost(PostAddParticipantUri)]
    public async Task<ActionResult<ServiceResponse<GroupInfo>>> AddParticipant(AddParticipantRequest request)
    {
        var result = await _service.AddParticipant(Request, request);
        return Ok(result);
    }

    [HttpPost(PostInviteParticipantUri)]
    public async Task<ActionResult<ServiceResponse<GroupInfo>>> InviteParticipant(InviteParticipantRequest request)
    {
        var result = await _service.InviteParticipant(Request, request);
        return Ok(result);
    }

    [HttpPost(PostAddUserUri)]
    public async Task<ActionResult<ServiceResponse<User>>> AddUser(AddUserRequest payload)
    {
        var result = await _service.AddUser(Request, payload);
        return Ok(result);
    }

    [HttpPut(PutEditGroupUri)]
    public async Task<ActionResult<ServiceResponse<GroupInfo>>> UpdateGroup(GroupRequest payload)
    {
        var result = await _service.UpdateGroup(Request, payload);
        return Ok(result);
    }

    [HttpDelete(DeleteGroupUri)]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteGroup(int id)
    {
        var result = await _service.DeleteGroup(Request, id);
        return Ok(result);
    }

}