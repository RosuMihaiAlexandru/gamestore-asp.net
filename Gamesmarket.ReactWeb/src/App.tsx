import "./App.css";
import React, { useContext } from "react";
import { Routes, Route, Link } from "react-router-dom";
import LoginPage from "./pages/auth/LoginPage";
import RegisterPage from "./pages/auth/RegisterPage";
import { observer } from "mobx-react-lite";
import { Context } from "./main";

function App() {
  const { rootStore } = useContext(Context);
  const { authStore } = rootStore;

  const handleLogout = () => {
    authStore.logout();
  };

  return (
    <div>
      <h2>{authStore.isAuth ? `You are authorised` : "Authorise please"}</h2>
      {authStore.isAuth ? (
        <button onClick={handleLogout}>Logout</button>
      ) : (
        <>
          <Link to="/login">Login</Link>
          <div></div>
          <Link to="/register">Register</Link>
        </>
      )}
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
      </Routes>
    </div>
  );
}

export default observer(App);
