import CssBaseline from "@mui/material/CssBaseline";
import { Box, Typography, Container } from "@mui/material";
import VideogameAssetOff from "@mui/icons-material/VideogameAssetOff";

export default function NoGames() {
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
      <Container sx={{ mt: 8, mb: 2, color: "#fff" }}>
        <VideogameAssetOff sx={{ fontSize: 80, color: "#f50057", mb: 2 }} />
        <Typography variant="h2" sx={{ mb: 1, fontWeight: "bold" }}>
          No games available on request
        </Typography>
      </Container>
    </Box>
  );
}
