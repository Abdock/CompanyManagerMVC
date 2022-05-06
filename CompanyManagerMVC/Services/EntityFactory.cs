using CompanyManagerMVC.Entities;
using CompanyManagerMVC.ViewModels;

namespace CompanyManagerMVC.Services;

public class EntityFactory
{
    public User CreateUser(RegisterViewModel model)
    {
        return new User
        {
            Email = model.Email,
            FirstName = model.Name,
            LastName = model.Surname,
            Phone = model.Phone,
            Password = model.Password,
            Department = model.Department,
            Role = model.Role
        };
    }
}