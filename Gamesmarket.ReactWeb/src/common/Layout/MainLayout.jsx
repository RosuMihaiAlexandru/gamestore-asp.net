import React from "react";
import { Outlet } from "react-router-dom";
import { MainNav } from "./MainNav";

export const MainLayout = () => {
  return (
    <>
      <MainNav />
      <main className="container" style={{ minHeight: "100vh" }}>
        <Outlet />
      </main>

      <footer className="container-fluid">
        Designed by Samko Vitalii 2023-2024
      </footer>
    </>
  );
};
