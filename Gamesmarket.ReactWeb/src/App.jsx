import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.min.js";

import "./App.css";
import { QueryClient, QueryClientProvider } from "react-query";
import { Navigate, Route, Routes } from "react-router-dom";
import { MainLayout } from "./common/Layout/MainLayout";
import {
  isAuthenticated,
  isAdminOrModerator,
} from "./pages/accounts/Utils/AuthHandler";
import GamesPage from "./pages/games/GamesPage";
import CreateGame from "./pages/games/CreateGamePage";
import HomePage from "./pages/HomePage";
import NotFoundPage from "./pages/NotFoundPage";
import AccessDeniedPage from "./pages/AccessDeniedPage";
import GameDetailsPage from "./pages/games/GameDetailsPage";
import SearchResultsPage from "./pages/games/SearchResultsPage";
import LoginPage from "./pages/accounts/LoginPage";
import RegistrationPage from "./pages/accounts/RegistrationPage";
import CartPage from "./pages/carts/CartPage";

const queryClient = new QueryClient();
function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Routes>
        <Route path="/" element={<MainLayout />}>
          <Route index element={<HomePage />} />
          <Route path="games" element={<GamesPage />} />
          <Route
            path="creategame"
            element={
              isAdminOrModerator() ? (
                <CreateGame />
              ) : (
                <Navigate to="/AccessDenied" />
              )
            }
          />
          <Route path="game/:id" element={<GameDetailsPage />} />
          <Route path="search-results" element={<SearchResultsPage />} />
          <Route path="login" element={<LoginPage />} />
          <Route path="register" element={<RegistrationPage />} />
          <Route
            path="cart"
            element={
              isAuthenticated() ? <CartPage /> : <Navigate to="/AccessDenied" />
            }
          />
          <Route path="AccessDenied" element={<AccessDeniedPage />} />
          <Route path="*" element={<NotFoundPage />} />
        </Route>
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
    </QueryClientProvider>
  );
}

export default App;
