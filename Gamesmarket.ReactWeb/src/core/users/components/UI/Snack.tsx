import React from "react";
import { Snackbar, Alert } from "@mui/material";

interface SnackProps {
  isOpen: boolean;
  handleClose: () => void;
  message: string;
  severity: "success" | "error";
}

const Snack: React.FC<SnackProps> = ({
  isOpen,
  handleClose,
  message,
  severity,
}) => {
  return (
    <Snackbar open={isOpen} onClose={handleClose} autoHideDuration={3000}>
      <Alert onClose={handleClose} severity={severity} sx={{ width: "100%" }}>
        {message}
      </Alert>
    </Snackbar>
  );
};

export default Snack;
