import { useState } from "react";
import { Button, FormControl, InputLabel, MenuItem, Select, TextField, Typography, Paper } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useContext } from "react";
import { Context } from "../../../../main";

const ChangeRoleForm = () => {
  const { rootStore } = useContext(Context);
  const { userStore } = rootStore;
  const [email, setEmail] = useState<string>("");
  const [newRole, setNewRole] = useState<string>("");

  const handleRoleChange = async () => {
    if (email && newRole) {
      await userStore.changeRole(email, newRole);
      userStore.getUsers(); // Refresh the users list after changing the role
    }
  };

  return (
    <Paper
      sx={{
        padding: 2,
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'flex-start',
        alignItems: 'center',
        height: '100%',
        boxSizing: 'border-box'
      }}
    >
      <Typography variant="h6" gutterBottom>
        Change User Role
      </Typography>
      <TextField
        label="User Email"
        variant="outlined"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        sx={{ mb: 2, width: "100%" }}
      />
      <FormControl variant="outlined" sx={{ mb: 2, width: "100%" }}>
        <InputLabel id="role-select-label">New Role</InputLabel>
        <Select
          labelId="role-select-label"
          value={newRole}
          onChange={(e) => setNewRole(e.target.value as string)}
          label="New Role"
        >
          <MenuItem value="User">User</MenuItem>
          <MenuItem value="Moderator">Moderator</MenuItem>
          <MenuItem value="Administrator">Administrator</MenuItem>
        </Select>
      </FormControl>
      <Button variant="contained" onClick={handleRoleChange} sx={{ width: '100%' }}>
        Change Role
      </Button>
    </Paper>
  );
};

export default observer(ChangeRoleForm);
