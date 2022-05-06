using CompanyManagerMVC.Context;
using CompanyManagerMVC.Entities;
using CompanyManagerMVC.ViewModels;

namespace CompanyManagerMVC.Services;

public class EntityFactory
{
    private readonly MyDbContext _db;

    public EntityFactory(MyDbContext db)
    {
        _db = db;
    }

    public User CreateUser(RegisterViewModel model)
    {
        return new User
        {
            Email = model.Email,
            FirstName = model.Name,
            LastName = model.Surname,
            Phone = model.Phone,
            Password = model.Password,
            Department = _db.Departments.First(department => department.Name == model.Department),
            Role = _db.UserRoles.First(role => role.Name == model.Role)
        };
    }
}