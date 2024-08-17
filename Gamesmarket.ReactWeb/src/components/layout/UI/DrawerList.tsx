import React from "react";
import {
  List,
  ListItem,
  ListItemText,
  ListItemIcon,
  ListItemButton,
  Divider,
  Box,
} from "@mui/material";
import { People, Add as AddIcon } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";

interface DrawerListProps {
  toggleDrawer: (open: boolean) => void;
}

const DrawerList: React.FC<DrawerListProps> = ({ toggleDrawer }) => {
  const navigate = useNavigate();

  const handleNavigation = (path: string) => {
    navigate(path);
    toggleDrawer(false);
  };

  return (
    <Box
      sx={{ width: 250 }}
      role="presentation"
      onClick={() => toggleDrawer(false)}
    >
      <List>
        <ListItem disablePadding>
          <ListItemButton onClick={() => handleNavigation("/users")}>
            <ListItemIcon>
              <People />
            </ListItemIcon>
            <ListItemText primary="Users" />
          </ListItemButton>
        </ListItem>
        <ListItem disablePadding>
          <ListItemButton onClick={() => handleNavigation("/creategame")}>
            <ListItemIcon>
              <AddIcon />
            </ListItemIcon>
            <ListItemText primary="Add game" />
          </ListItemButton>
        </ListItem>
      </List>
      <Divider />
    </Box>
  );
};

export default DrawerList;
