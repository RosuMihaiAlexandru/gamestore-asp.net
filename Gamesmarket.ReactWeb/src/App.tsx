import "./App.css";
import { useContext } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoginPage from "./pages/auth/LoginPage";
import RegisterPage from "./pages/auth/RegisterPage";
import Header from "./components/Header";
import Footer from "./components/Footer";
import { observer } from "mobx-react-lite";
import { Context } from "./main";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { Box, Container } from "@mui/material";
import UsersPage from "./pages/admin/UsersPage";
import AccessDeniedPage from "./pages/redirect/AccessDeniedPage";
import NotFoundPage from "./pages/redirect/NotFoundPage";
import PrivateRoute from "./routes/PrivateRoute";
import GamesPage from "./pages/games/GamesPage";

function App() {
  const { rootStore } = useContext(Context);
  const { authStore } = rootStore;

  const theme = createTheme({
    palette: {
      primary: {
        main: "#212424",
      },
      secondary: {
        main: "#272727",
      },
    },
  });

  return (
    <ThemeProvider theme={theme}>
      <Box
        sx={{ display: "flex", flexDirection: "column", minHeight: "100vh" }}
      >
        <Header />
        <Container component="main" sx={{ flexGrow: 1, paddingBottom: "60px" }}>
          <Routes>
            <Route path="/games" element={<GamesPage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route element={<PrivateRoute roles={["Administrator"]} />}>
              <Route path="/users" element={<UsersPage />} />
            </Route>
            <Route path="/accessDenied" element={<AccessDeniedPage />} />
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
        </Container>
        <Footer />
      </Box>
    </ThemeProvider>
  );
}

export default observer(App);
