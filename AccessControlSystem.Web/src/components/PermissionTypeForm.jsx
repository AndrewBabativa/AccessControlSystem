import React, { useState } from 'react';
import { Box, TextField, Button, Typography } from '@mui/material';
import { addPermissionType } from '../api/permissionType'; 

const PermissionTypeForm = ({ onSuccess, onCancel }) => {
  const [description, setDescription] = useState('');
  const [error, setError] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!description.trim()) {
      setError('La descripción es obligatoria');
      return;
    }

    setError('');

    try {
      const newPermissionType = await addPermissionType({ description });
      onSuccess(newPermissionType);
      setDescription('');
    } catch (err) {
      setError('Error al crear el tipo de permiso');
      console.error(err);
    }
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit}
      sx={{
        backgroundColor: '#f9f9f9',
        padding: 3,
        borderRadius: 2,
        boxShadow: 1,
        maxWidth: 400,
        margin: '0 auto',
      }}
    >
      <Typography variant="h6" mb={2}>
        Crear Tipo de Permiso
      </Typography>

      <TextField
        label="Descripción"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        fullWidth
        error={!!error}
        helperText={error}
        autoFocus
      />

      <Box sx={{ mt: 3, display: 'flex', justifyContent: 'space-between' }}>
        <Button variant="contained" type="submit">
          Crear
        </Button>
        <Button variant="outlined" onClick={onCancel}>
          Cancelar
        </Button>
      </Box>
    </Box>
  );
};

export default PermissionTypeForm;
