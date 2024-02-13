import React from "react";
import { useLocation } from "react-router-dom";
import GameList from "./components/GamesList";

const SearchResultsPage = () => {
  const location = useLocation();
  const { results } = location.state || {};

  return (
    <div className="container">
      <h1>Search Results</h1>
      {results && <GameList games={results} />}
    </div>
  );
};

export default SearchResultsPage;
