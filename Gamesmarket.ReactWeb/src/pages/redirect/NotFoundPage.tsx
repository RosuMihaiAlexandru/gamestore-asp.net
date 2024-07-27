import CssBaseline from "@mui/material/CssBaseline";
import { Box, Typography, Container } from "@mui/material";
import SearchOff from "@mui/icons-material/SearchOff";

export default function NotFoundPage() {
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
        <SearchOff sx={{ fontSize: 80, color: "#f50057", mb: 2 }} />
        <Typography variant="h2" sx={{ mb: 1, fontWeight: "bold" }}>
          404 Page not found
        </Typography>
        <Typography variant="h5" sx={{ mb: 3 }}>
          Apologies, but the page you were looking for wasn't found.
        </Typography>
      </Container>
    </Box>
  );
}
