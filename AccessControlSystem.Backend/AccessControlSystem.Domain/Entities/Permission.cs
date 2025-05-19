namespace AccessControlSystem.Domain.Entities;

public class Permission
{
    public int Id { get; set; }
    public string EmployeeFirstName { get; set; } = string.Empty;
    public string EmployeeLastName { get; set; } = string.Empty;
    public int PermissionTypeId { get; set; }
    public DateTime PermissionDate { get; set; }
    public PermissionType PermissionType { get; set; } = default!;
}
