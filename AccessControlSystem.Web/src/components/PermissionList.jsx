import { formatDate } from '../utils/formatDate';
import { List, ListItem, ListItemText, Button } from '@mui/material';

const PermissionList = ({ permissions, onEdit }) => {
  return (
    <div>
      <h3>Permisos Registrados</h3>
      <List>
        {permissions.map((perm) => (
          <ListItem key={perm.id} divider>
            <ListItemText
              primary={`${perm.employeeFirstName} ${perm.employeeLastName}`}
              secondary={`Tipo: ${perm.permissionTypeId} - Fecha: ${formatDate(perm.permissionDate)}`}
            />
            <Button variant="outlined" onClick={() => onEdit(perm)}>
              Editar
            </Button>
          </ListItem>
        ))}
      </List>
    </div>
  );
};

export default PermissionList;
