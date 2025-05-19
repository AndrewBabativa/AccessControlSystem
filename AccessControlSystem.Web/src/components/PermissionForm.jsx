import React, { useState, useEffect } from 'react';
import {
  TextField,
  Button,
  MenuItem,
  Grid,
  Typography,
  Card,
  CardContent,
  Box,
  Snackbar,
  Alert,
} from '@mui/material';
import { createRequestPermissionDTO } from '../dtos/PermissionDTO';
import { addPermission } from '../api/permissions';
import { getPermissionTypes } from '../api/permissionType';  

const PermissionForm = ({ onSuccess }) => {
  const [form, setForm] = useState({
    employeeFirstName: '',
    employeeLastName: '',
    permissionTypeId: '',  
  });

  const [permissionTypes, setPermissionTypes] = useState([]);  

  const [snackbar, setSnackbar] = useState({
    open: false,
    message: '',
    severity: 'success',
  });

  useEffect(() => {
    const fetchPermissionTypes = async () => {
      try {
        const data = await getPermissionTypes();
        setPermissionTypes(data);
        if(data.length > 0) setForm(prev => ({ ...prev, permissionTypeId: data[0].id }));
      } catch (error) {
        console.error('Error loading permission types', error);
      }
    };

    fetchPermissionTypes();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const handleCloseSnackbar = () => {
    setSnackbar((prev) => ({ ...prev, open: false }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const dto = createRequestPermissionDTO(form);
    try {
      const data = await addPermission(dto);
      setSnackbar({
        open: true,
        message: `Permiso creado correctamente con ID ${data.id}`,
        severity: 'success',
      });
      onSuccess(data);
      setForm({
        employeeFirstName: '',
        employeeLastName: '',
        permissionTypeId: permissionTypes.length > 0 ? permissionTypes[0].id : '',
      });
    } catch (error) {
      setSnackbar({
        open: true,
        message: 'Error al crear el permiso. Por favor intente de nuevo.',
        severity: 'error',
      });
      console.error('Error submitting permission:', error);
    }
  };

  return (
    <Box sx={{ maxWidth: 700, mx: 'auto', mt: 5, px: 2 }}>
      <Card elevation={3}>
        <CardContent>
          <Typography variant="h5" align="center" gutterBottom>
            Solicitar Permiso
          </Typography>

          <Box component="form" onSubmit={handleSubmit} sx={{ mt: 2 }}>
            <Grid container spacing={3}>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  name="employeeFirstName"
                  label="Primer Nombre"
                  value={form.employeeFirstName}
                  onChange={handleChange}
                  required
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  name="employeeLastName"
                  label="Apellido"
                  value={form.employeeLastName}
                  onChange={handleChange}
                  required
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  select
                  name="permissionTypeId"
                  label="Tipo de Permiso"
                  value={form.permissionTypeId}
                  onChange={handleChange}
                  required
                  sx={{
                    width: '185px',
                    '.MuiSelect-select': {
                      display: 'flex',
                      alignItems: 'center',
                    },
                  }}
                >
                  {permissionTypes.map((type) => (
                    <MenuItem key={type.id} value={type.id}>
                      {type.description}
                    </MenuItem>
                  ))}
                </TextField>
              </Grid>
              <Grid item xs={12}>
                <Box sx={{ display: 'flex', justifyContent: 'flex-end' }}>
                  <Button type="submit" variant="contained" color="primary" size="large">
                    Solicitar
                  </Button>
                </Box>
              </Grid>
            </Grid>
          </Box>
        </CardContent>
      </Card>

      <Snackbar
        open={snackbar.open}
        autoHideDuration={4000}
        onClose={handleCloseSnackbar}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
      >
        <Alert
          onClose={handleCloseSnackbar}
          severity={snackbar.severity}
          sx={{ width: '100%' }}
        >
          {snackbar.message}
        </Alert>
      </Snackbar>
    </Box>
  );
};

export default PermissionForm;
