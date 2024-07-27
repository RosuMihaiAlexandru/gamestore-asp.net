import CssBaseline from "@mui/material/CssBaseline";
import { Box, Typography, Container } from "@mui/material";
import PanToolIcon from "@mui/icons-material/PanTool";

export default function AccessDeniedPage() {
  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        minHeight: "75vh",
        textAlign: "center",
        padding: 2,
      }}
    >
      <CssBaseline />
      <Container sx={{ mt: 8, mb: 2 }}>
        <PanToolIcon sx={{ fontSize: 80, color: "#f50057", mb: 2 }} />
        <Typography variant="h2" sx={{ mb: 1, fontWeight: "bold" }}>
          403 Forbidden
        </Typography>
        <Typography variant="h5" sx={{ mb: 3 }}>
          Sorry, but the requested sourse is not avaliable for you.
        </Typography>
      </Container>
    </Box>
  );
}
