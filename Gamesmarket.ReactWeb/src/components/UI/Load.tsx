import { Box, CircularProgress } from "@mui/material";

export default function Load() {
  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
      }}
    >
      <CircularProgress sx={{ color: "white" }} />
    </Box>
  );
}
