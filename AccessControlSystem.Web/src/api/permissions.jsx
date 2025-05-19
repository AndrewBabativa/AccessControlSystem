import axiosClient from './axiosClient';

export const getPermissions = async () => {
  const response = await axiosClient.get('/');
  return response.data;
};

export const addPermission = async (permission) => {
  console.log(permission);
  const response = await axiosClient.post('/request', permission);
  return response.data;
};

export const updatePermission = async (permission) => {
  console.log('Sending to backend:', JSON.stringify(permission, null, 2));
  const response = await axiosClient.put('/modify', permission);
  return response.data;
};
