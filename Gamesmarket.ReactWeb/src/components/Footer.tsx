import { Box, Typography } from "@mui/material";

function Footer() {
  return (
    <Box
      sx={{
        width: "100%",
        backgroundColor: "#616161",
        color: "white",
        textAlign: "center",
        padding: "10px 0",
        position: "static",
      }}
    >
      <Typography variant="body1">Designed by Samko Vitalii 2024</Typography>
    </Box>
  );
}

export default Footer;
