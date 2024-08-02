import { useState } from "react";
import {
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Typography,
  Box,
} from "@mui/material";
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
    }
  };

  return (
    <Box
      sx={{
        backgroundColor: "#2c2f33",
        padding: 2,
        borderRadius: 1,
        color: "#fff",
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
        sx={{
          mb: 2,
          width: "100%",
          "& .MuiInputBase-input": { color: "#fff" }, // Color of the text in the input field
          "& .MuiOutlinedInput-root .MuiOutlinedInput-notchedOutline": {
            borderColor: "#ccc", // Frame color (border)
          },
        }}
        InputLabelProps={{
          style: { color: "#ccc" }, // Label color
        }}
        InputProps={{
          style: { color: "#fff" }, // Color of the text in the input field
        }}
      />
      <FormControl
        variant="outlined"
        sx={{
          mb: 2,
          width: "100%",
          "& .MuiInputBase-input": { color: "#fff" },
          "& .MuiInputLabel-root": { color: "#ccc" }, // Label color
          "& .MuiOutlinedInput-root .MuiOutlinedInput-notchedOutline": {
            borderColor: "#ccc",
          },
        }}
      >
        <InputLabel id="role-select-label">New Role</InputLabel>
        <Select
          labelId="role-select-label"
          value={newRole}
          onChange={(e) => setNewRole(e.target.value as string)}
          label="New Role"
          sx={{ color: "#fff" }} // Color of the text in the selection field
          MenuProps={{
            PaperProps: {
              sx: {
                backgroundColor: "#2c2f33", // Menu background color
                "& .MuiMenuItem-root": {
                  color: "#fff", // Text color of menu items
                },
                "& .MuiMenuItem-root:hover": {
                  backgroundColor: "#3c3f43", // Background color when hovering over a menu item
                },
              },
            },
          }}
        >
          <MenuItem value="User">User</MenuItem>
          <MenuItem value="Moderator">Moderator</MenuItem>
          <MenuItem value="Administrator">Administrator</MenuItem>
        </Select>
      </FormControl>
      <Button
        variant="contained"
        onClick={handleRoleChange}
        sx={{
          width: "100%",
          backgroundColor: "#ff4020",
          color: "white",
          "&:hover": {
            backgroundColor: "#e0391d",
          },
        }}
      >
        Change Role
      </Button>
    </Box>
  );
};

export default observer(ChangeRoleForm);
