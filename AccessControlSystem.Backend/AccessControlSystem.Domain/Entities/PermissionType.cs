namespace AccessControlSystem.Domain.Entities;

public class PermissionType
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
