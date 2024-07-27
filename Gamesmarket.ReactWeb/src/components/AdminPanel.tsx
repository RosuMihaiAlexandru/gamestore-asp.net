import { useState } from "react";
import { Drawer, IconButton } from "@mui/material";
import { AdminPanelSettings } from "@mui/icons-material";
import DrawerList from "./UI/DrawerList";

export default function AdminPanel() {
  const [open, setOpen] = useState(false);

  const toggleDrawer = (newOpen: boolean) => {
    setOpen(newOpen);
  };

  return (
    <div>
      <IconButton onClick={() => toggleDrawer(true)}>
        <AdminPanelSettings sx={{ ml: 1, width: 30, height: 30 }} />
      </IconButton>
      <Drawer open={open} onClose={() => toggleDrawer(false)}>
        <DrawerList toggleDrawer={toggleDrawer} />
      </Drawer>
    </div>
  );
}
