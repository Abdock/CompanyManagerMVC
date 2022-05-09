namespace CompanyManagerMVC.ViewModels;

#nullable disable

public class PostViewModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Department { get; set; }
    public string Creator { get; set; }
    public IFormFileCollection Files { get; set; }
}