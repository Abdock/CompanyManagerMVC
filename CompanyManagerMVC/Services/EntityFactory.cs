using CompanyManagerMVC.Context;
using CompanyManagerMVC.Entities;
using CompanyManagerMVC.ViewModels;
using File = CompanyManagerMVC.Entities.File;

namespace CompanyManagerMVC.Services;

public class EntityFactory
{
    private readonly MyDbContext _db;
    private readonly IWebHostEnvironment _environment;

    public EntityFactory(MyDbContext db, IWebHostEnvironment environment)
    {
        _db = db;
        _environment = environment;
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

    public File CreateFile(IFormFile file)
    {
        string path = _environment.WebRootPath + $"Files/Images/{DateTime.UtcNow.Ticks}.{file.FileName}";
        var stream = new FileStream(path, FileMode.Create);

        return new File
        {
            Location = path
        };
    }

    public Post CreatePost(PostViewModel model)
    {
        return new Post
        {
            Title = model.Title,
            Content = model.Content,
            CreationDate = DateOnly.FromDateTime(DateTime.UtcNow),
            Department = _db.Departments.FirstOrDefault(department => department.Name == model.Department)!,
            Creator = _db.Users.FirstOrDefault(user => user.Email == model.Creator)!
        };
    }
}