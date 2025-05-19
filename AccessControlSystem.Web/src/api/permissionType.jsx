import axiosClient from './axiosClient'; 

export const getPermissionTypes = async () => {
  const response = await axiosClient.get('/PermissionType');
  return response.data;
};

export const addPermissionType = async (permissionType) => {
  const response = await axiosClient.post('/PermissionType', permissionType);
  return response.data;
};
