import "./App.css";
import React, { useContext } from "react";
import { Routes, Route, Link } from "react-router-dom";
import LoginPage from "./pages/auth/LoginPage";
import RegisterPage from "./pages/auth/RegisterPage";
import Header from "./components/Header";
import Footer from "./components/Footer";
import { observer } from "mobx-react-lite";
import { Context } from "./main";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { Box, Container } from "@mui/material";

function App() {
  const { rootStore } = useContext(Context);
  const { authStore } = rootStore;

  const theme = createTheme({
    palette: {
      primary: {
        main: "#03a9f4",
      },
      secondary: {
        main: "#69f0ae",
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
          <h2>
            {authStore.isAuth ? `You are authorised` : "Authorise please"}
          </h2>
          <h2>{authStore.isAdmin ? `You are admin wow` : "Admin?"}</h2>
          <Link to="/login">Login</Link>
          <div />
          <Link to="/register">Register</Link>
          <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
          </Routes>
        </Container>
        <Footer />
      </Box>
    </ThemeProvider>
  );
}

export default observer(App);
