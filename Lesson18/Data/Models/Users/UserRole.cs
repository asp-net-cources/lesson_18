namespace Lesson18.Data.Models.Users;

public class UserRole
{
    public string Name { get; set; }

    public static UserRole Admin() => new() { Name = "Admin" };
    public static UserRole Common() => new() { Name = "Common" };

    public static UserRole GetRole(string roleStr)
    {
        return roleStr switch
        {
            "Admin" => Admin(),
            _ => Common()
        };
    }
}