import { useState, useEffect } from 'react';
import PermissionForm from '../components/PermissionForm';
import ModifyPermissionForm from '../components/ModifyPermissionForm';
import PermissionList from '../components/PermissionList';
import PermissionTypeForm from '../components/PermissionTypeForm';
import { Box, Typography, Button, Collapse, Divider } from '@mui/material';
import { getPermissions } from '../api/permissions';

const PermissionsPage = () => {
  const [selectedPermission, setSelectedPermission] = useState(null);
  const [permissions, setPermissions] = useState([]);
  const [showCreateForm, setShowCreateForm] = useState(false);
  const [showCreatePermissionTypeForm, setShowCreatePermissionTypeForm] = useState(false);

  useEffect(() => {
    const fetchPermissions = async () => {
      try {
        const data = await getPermissions();
        setPermissions(data);
      } catch (error) {
        console.error('Error fetching permissions:', error);
      }
    };

    fetchPermissions();
  }, []);

  const handleEdit = (permission) => {
    setSelectedPermission(permission);
    setShowCreateForm(false);
  };

  const handleFormSuccess = (newPermission) => {
    setPermissions((prev) => [...prev, newPermission]);
    setShowCreateForm(false);
  };

  const toggleCreateForm = () => {
    setSelectedPermission(null);
    setShowCreateForm((prev) => !prev);
  };

  return (
    <Box sx={{ maxWidth: 1000, width: '100%', mt: 4, px: 2 }}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
        <Button variant="contained" onClick={toggleCreateForm}>
          {showCreateForm ? 'Cancelar' : 'Crear Permiso'}
        </Button>
      </Box>

      <Collapse in={showCreateForm}>
        <PermissionForm onSuccess={handleFormSuccess} />

        <Box sx={{  p: 3, borderRadius: 2, boxShadow: 1, mt:4, mb: 4 }}>
          <Button
            variant="outlined"
            onClick={() => setShowCreatePermissionTypeForm((prev) => !prev)}
          >
            {showCreatePermissionTypeForm ? 'Cancelar Tipo de Permiso' : 'Crear Tipo de Permiso'}
          </Button>

          <Collapse in={showCreatePermissionTypeForm}>
            <PermissionTypeForm
              onCancel={() => setShowCreatePermissionTypeForm(false)}
              onSuccess={() => {
                setShowCreatePermissionTypeForm(false);
              }}
            />
          </Collapse>
        </Box>

        <Divider sx={{ my: 4 }} />
      </Collapse>

      {selectedPermission && (
        <>
          <ModifyPermissionForm
            permission={selectedPermission}
            onSuccess={(updated) => {
              setPermissions((prev) =>
                prev.map((perm) => (perm.id === updated.id ? updated : perm))
              );
            }}
            onClose={() => setSelectedPermission(null)}
          />
          <Divider sx={{ my: 4 }} />
        </>
      )}

      <PermissionList onEdit={handleEdit} permissions={permissions} />
    </Box>
  );
};

export default PermissionsPage;
