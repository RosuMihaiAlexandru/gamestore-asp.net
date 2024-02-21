import React from "react";
import { Link } from "react-router-dom";
import SearchHandler from "../../pages/games/Utils/Searchhandler";
import {
  isAdminOrModerator,
  isAdmin,
} from "../../pages/accounts/Utils/AuthHandler";
import AuthNav from "./AuthNav";

export const MainNav = () => {
  const isAllowedToCreateGame = isAdminOrModerator();
  const isAdministrator = isAdmin();
  return (
    <>
      <nav className="navbar navbar-expand-lg navbar-white bg-white shadow-sm">
        <div className="container-fluid">
          <Link className="navbar-brand" to="/">
            GamesMarket
          </Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
              <li className="nav-item">
                <Link
                  className="nav-link active"
                  aria-current="page"
                  to="/games"
                >
                  Games
                </Link>
              </li>
              {isAllowedToCreateGame && (
                <li className="nav-item">
                  <Link
                    className="nav-link active"
                    aria-current="page"
                    to="/creategame"
                  >
                    CreateGame
                  </Link>
                </li>
              )}
              {isAdministrator && (
                <li className="nav-item">
                  <Link
                    className="nav-link active"
                    aria-current="page"
                    to="/usersPage"
                  >
                    Users
                  </Link>
                </li>
              )}
            </ul>
            <div className="d-flex align-items-center justify-content-center flex-grow-1">
              <SearchHandler />
            </div>
            <AuthNav />
          </div>
        </div>
      </nav>
    </>
  );
};
