import React from 'react';
import PermissionsPage from './pages/PermissionsPage'; // Asegúrate que esta ruta sea correcta
import { Box, Typography } from '@mui/material';

const App = () => {
  return (
    <Box
      sx={{
        height: '100vh',
        width: '100vw',
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        backgroundColor: '#f5f5f5',
        padding: '2rem',
        overflowY: 'auto'
      }}
    >
      <Typography variant="h4" gutterBottom>
        Administrador de Permisos
      </Typography>
      <PermissionsPage />
    </Box>
  );
};

export default App;
