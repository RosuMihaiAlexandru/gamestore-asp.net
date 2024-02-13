import React from "react";
import { Link } from "react-router-dom";
import SearchHandler from "../../pages/games/Utils/Searchhandler";

export const MainNav = () => {
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
              <li className="nav-item">
                <Link
                  className="nav-link active"
                  aria-current="page"
                  to="/creategame"
                >
                  CreateGame
                </Link>
              </li>
            </ul>
            <li className="d-flex">
              <SearchHandler />
            </li>
          </div>
        </div>
      </nav>
    </>
  );
};
