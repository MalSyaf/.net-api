using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsers")]
    // public IActionResult Test()
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] 
            FROM TutorialAppSchema.Users";

        IEnumerable<User> users = _dapper.LoadData<User>(sql);

        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    // public IActionResult Test()
    public User GetSingleUser(int userId)
    {
        string sql = @"
            SELECT [UserId],
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active] 
            FROM TutorialAppSchema.Users
                WHERE UserId = " + userId.ToString();

        User user = _dapper.LoadDataSingle<User>(sql);

        return user;
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

    [HttpGet("GetSingleUserSalary/{userId}")]
    // public IActionResult Test()
    public UserSalary GetSingleUserSalary(int userId)
    {
        string sql = @"
            SELECT 
            UserSalary.UserId, 
            UserSalary.Salary 
            FROM TutorialAppSchema.UserSalary
                WHERE UserId = " + userId.ToString();

        UserSalary userSalary = _dapper.LoadDataSingle<UserSalary>(sql);

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

    [HttpGet("SingleJobInfo/{userId}")]
    // public IActionResult Test()
    public UserJobInfo SingleJobInfo(int userId)
    {
        string sql = @"
            SELECT 
            UserJobInfo.UserId, 
            UserJobInfo.JobTitle, 
            UserJobInfo.Department 
            FROM TutorialAppSchema.UserJobInfo
                WHERE UserId = " + userId.ToString();

        UserJobInfo userSalary = _dapper.LoadDataSingle<UserJobInfo>(sql);

        return userSalary;
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