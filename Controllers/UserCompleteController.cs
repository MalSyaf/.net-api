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
        string parameters = "";
        string sql = @"EXEC TutorialAppSchema_spUsers_Get";
        
        if (userId != 0) 
        {
           parameters += ", @UserId = " + userId.ToString();
        }
        if (isActive) 
        {
           parameters += ", @Active = " + isActive.ToString();
        }

        sql += parameters.Substring(1);
    
        IEnumerable<UserComplete> users = _dapper.LoadData<UserComplete>(sql);
        return users;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
            UPDATE TutorialAppSchema.Users 
            SET [FirstName] = '" + user.FirstName + 
                "', [LastName] = '" + user.LastName + 
                "', [Email]= '" + user.Email + 
                "', [Gender] = '" + user.Gender + 
                "', [Active] = '" + user.Active + 
            "' WHERE UserId = " + user.UserId;

        Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"
        INSERT INTO TutorialAppSchema.Users(
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active]
        ) VALUES (" +
            "'" + user.FirstName + 
            "', '" + user.LastName +
            "', '" + user.Email +
            "', '" + user.Gender +
            "', '" + user.Active +
            "')";

        Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Add User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"DELETE FROM TutorialAppSchema.Users WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User");
    }

    //User Salary

    [HttpGet("GetUserSalary")]
    // public IActionResult Test()
    public IEnumerable<UserSalary> GetUserSalary()
    {
        string sql = @"
            SELECT 
            UserSalary.UserId, 
            UserSalary.Salary 
            FROM TutorialAppSchema.UserSalary";

        IEnumerable<UserSalary> userSalary = _dapper.LoadData<UserSalary>(sql);

        return userSalary;
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        string sql = @"
            UPDATE TutorialAppSchema.UserSalary 
            SET [Salary] = '" + userSalary.Salary + 
            "' WHERE UserId = " + userSalary.UserId;

        Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User Salary");
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalary userSalary)
    {
        string sql = @"
        INSERT INTO TutorialAppSchema.UserSalary(
            [UserId],
            [Salary]
        ) VALUES (" + userSalary.UserId
            + ", " + userSalary.Salary +
            ")";

        Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Add User Salary");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = @"DELETE FROM TutorialAppSchema.UserSalary WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User Salary");
    }

    //User Job Info

    [HttpGet("GetUserJob")]
    // public IActionResult Test()
    public IEnumerable<UserJobInfo> GetUserJob()
    {
        string sql = @"
            SELECT 
            UserJobInfo.UserId,
            UserJobInfo.JobTitle, 
            UserJobInfo.Department 
            FROM TutorialAppSchema.UserJobInfo";

        IEnumerable<UserJobInfo> userJobInfo = _dapper.LoadData<UserJobInfo>(sql);

        return userJobInfo;
    }

    [HttpPut("EditJobInfo")]
    public IActionResult EditJobInfo(UserJobInfo userJobInfo)
    {
        string sql = @"
            UPDATE TutorialAppSchema.UserJobInfo 
            SET [JobTitle] = '" + userJobInfo.JobTitle + 
                "', [Department] = '" + userJobInfo.Department + 
                "' WHERE UserId = " + userJobInfo.UserId;

        Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User Job");
    }

    [HttpPost("AddUserJob")]
    public IActionResult AddUserJob(UserJobInfo userJobInfo)
    {
        string sql = @"
        INSERT INTO TutorialAppSchema.UserJobInfo(
            [UserId],
            [JobTitle],
            [Department]
        ) VALUES (" + userJobInfo.UserId + 
            ", '" + userJobInfo.JobTitle +
            "', '" + userJobInfo.Department +
            "')";

        Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Add User Job");
    }

    [HttpDelete("DeleteUserJob/{userId}")]
    public IActionResult DeleteUserJob(int userId)
    {
        string sql = @"DELETE FROM TutorialAppSchema.UserJobInfo WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if(_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User Job");
    }
}