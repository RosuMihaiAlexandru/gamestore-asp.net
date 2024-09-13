import "./App.css";
import { Routes, Route } from "react-router-dom";
import LoginPage from "./pages/auth/LoginPage";
import RegisterPage from "./pages/auth/RegisterPage";
import Header from "./components/layout/Header";
import Footer from "./components/layout/Footer";
import { observer } from "mobx-react-lite";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { Box, Container, CssBaseline } from "@mui/material";
import UsersPage from "./pages/admin/UsersPage";
import AccessDeniedPage from "./pages/redirect/AccessDeniedPage";
import NotFoundPage from "./pages/redirect/NotFoundPage";
import PrivateRoute from "./routes/PrivateRoute";
import GamesPage from "./pages/games/GamesPage";
import GameDetails from "./core/game/components/GameDetails";
import SearchResultsPage from "./pages/games/SearchResultsPage";
import CreateGamePage from "./pages/admin/CreateGamePage";
import EditGamePage from "./pages/admin/EditGamePage";
import CartPage from "./pages/cart/CartPage";
import NewGamesPage from "./pages/games/NewGamesPage";
import HomePage from "./pages/home/HomePage";
import { Context } from "./main";
import { useContext } from "react";

function App() {
  const { rootStore } = useContext(Context);
  const { cartStore } = rootStore;

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
      <CssBaseline />
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          minHeight: "100vh",
          backgroundColor: "#18161c",
        }}
      >
        <Header orderLen={cartStore.orders.length} />
        <Container component="main" sx={{ flexGrow: 1, paddingBottom: "60px" }}>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/games" element={<GamesPage />} />
            <Route path="/game/:id" element={<GameDetails />} />
            <Route path="/new-games" element={<NewGamesPage />} />
            <Route path="/search-results" element={<SearchResultsPage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route
              element={
                <PrivateRoute roles={["User", "Moderator", "Administrator"]} />
              }
            >
              <Route path="/cart" element={<CartPage />} />
            </Route>
            <Route element={<PrivateRoute roles={["Administrator"]} />}>
              <Route path="/users" element={<UsersPage />} />
            </Route>
            <Route
              element={<PrivateRoute roles={["Administrator", "Moderator"]} />}
            >
              <Route path="/creategame" element={<CreateGamePage />} />
              <Route path="/editgame/:id" element={<EditGamePage />} />
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
