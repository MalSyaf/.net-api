using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserCompleteController : ControllerBase
{
    DataContextDapper _dapper;
    public UserCompleteController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsers/{userId}/{isActive}")]
    // public IActionResult Test()
    public IEnumerable<UserComplete> GetUsers(int userId, bool isActive)
    {
        string sql = @"EXEC TutorialAppSchema_spUsers_Get";
        string parameters = "";

        if (userId != 0)
        {
            parameters += ", @UserId = " + userId.ToString();
        }
        if (isActive)
        {
            parameters += ", @Active = " + isActive.ToString();
        }

        if (parameters.Length > 0)
        {
            sql += parameters.Substring(1);//, parameters.Length);
        }

        IEnumerable<UserComplete> users = _dapper.LoadData<UserComplete>(sql);
        return users;
    }

    [HttpPut("UpsertUser")]
    public IActionResult UpsertUser(UserComplete user)
    {
        string sql = @"
            EXEC TutorialAppSchema_spUsers_Upsert
                 @FirstName = '" + user.FirstName +
                "', @LastName = '" + user.LastName +
                "', @Email = '" + user.Email +
                "', @Gender = '" + user.Gender +
                "', @Active = '" + user.Active +
                "', @JobTItle = '" + user.JobTitle +
                "', @Department = '" + user.Department +
                "', @Salary = '" + user.Salary +
                "', @UserId = " + user.UserId;

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"TutorialAppSchema_spUsers_Delete @UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User");
    }
}