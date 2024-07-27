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
import UsersPage from "./pages/admin/UsersPage";
import AccessDeniedPage from "./pages/redirect/AccessDeniedPage";
import NotFoundPage from "./pages/redirect/NotFoundPage";
import PrivateRoute from "./routes/PrivateRoute";

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
          <h4>
            {authStore.isAuth ? `You are authorised` : "Authorise please"}
          </h4>
          <h4>{authStore.isAdmin ? `You are admin wow` : "Admin?"}</h4>

          <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route element={<PrivateRoute roles={["Administrator"]} />}>
              <Route path="/users" element={<UsersPage />} />
            </Route>
            <Route
              element={<PrivateRoute roles={["Administrator", "Moderator"]} />}
            ></Route>
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
