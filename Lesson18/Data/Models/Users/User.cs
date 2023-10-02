using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson18.Data.Models.Users;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [StringLength(100)]
    public string FirstName { get; set; }
    [StringLength(100)]
    public string LastName { get; set; }
    [StringLength(100)]
    public string Login { get; set; }
    [StringLength(100)]
    public string Password { get; set; }
    [StringLength(100)]
    public string RoleStr { get; set; }

    [NotMapped]
    public UserRole Role
    {
        get => UserRole.GetRole(RoleStr);
        set => RoleStr = value.Name;
    }
}