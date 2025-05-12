using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShiftWebApi.Data;
using ShiftWebApi.Models;

namespace ShiftWebApi.Services;

public interface IUserService
{
    public List<User> GetAllUsers();
    public User? GetUserById(int id);
    public User CreateUser(User user);
    public User UpdateUser(int id, User updatedUser);
    public string? DeleteUser(int id);
}
public class UserService : IUserService
{
    private readonly ShiftDbContext _dbContext;
    public UserService(ShiftDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public User CreateUser(User user)
    {
        EntityEntry<User> newUser = _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return newUser.Entity;
    }

    public User UpdateUser(int id, User updatedUser)
    {
        User? userToUpdate = _dbContext.Users.Find(id);
        if (userToUpdate == null) return null;
        _dbContext.Entry(userToUpdate).CurrentValues.SetValues(updatedUser);
        _dbContext.SaveChanges();
        return userToUpdate;
    }

    public string? DeleteUser(int id)
    {
        User? userToDelete = _dbContext.Users.Find(id);
        if (userToDelete == null) return null;
        _dbContext.Users.Remove(userToDelete);
        _dbContext.SaveChanges();
        return $"Successfully deleted user with id: {id}, first name: {userToDelete.FirstName}, last name: {userToDelete.LastName}";
    }

    public List<User> GetAllUsers()
    {
        return _dbContext.Users.ToList();
    }

    public User? GetUserById(int id)
    {
        User? user = _dbContext.Users.Find(id);
        return user == null ? null : user;
    }
}
