export const createRequestPermissionDTO = ({
  employeeFirstName,
  employeeLastName,
  permissionTypeId,
}) => ({
  employeeFirstName,
  employeeLastName,
  permissionTypeId: parseInt(permissionTypeId, 10)
});
