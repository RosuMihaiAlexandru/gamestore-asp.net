import { useState } from "react";
import { Drawer, IconButton, Typography } from "@mui/material";
import { AdminPanelSettingsOutlined } from "@mui/icons-material";
import DrawerList from "./UI/DrawerList";

export default function AdminPanel() {
  const [open, setOpen] = useState(false);

  const toggleDrawer = (newOpen: boolean) => {
    setOpen(newOpen);
  };

  return (
    <div>
      <IconButton onClick={() => toggleDrawer(true)}>
        <AdminPanelSettingsOutlined
          sx={{ color: "white", ml: 1, width: 30, height: 30 }}
        />
        <Typography sx={{ color: "white", ml: 1 }}>Admin panel</Typography>
      </IconButton>
      <Drawer open={open} onClose={() => toggleDrawer(false)}>
        <DrawerList toggleDrawer={toggleDrawer} />
      </Drawer>
    </div>
  );
}
