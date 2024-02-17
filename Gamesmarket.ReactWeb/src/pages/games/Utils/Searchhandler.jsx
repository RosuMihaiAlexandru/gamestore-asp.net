import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { searchGames } from "../../../common/services/api/game/GameApi";

const SearchHandler = () => {
  const navigate = useNavigate();
  const [searchQuery, setSearchQuery] = useState("");

  const handleSearch = async (e) => {
    e.preventDefault();

    try {
      const results = await searchGames(searchQuery);
      navigate("/search-results", { state: { results } });
    } catch (error) {
      console.error("Error searching game:", error);
    }
  };

  return (
    <form className="d-flex w-50" onSubmit={handleSearch}>
      <input
        className="form-control me-2"
        type="search"
        placeholder="Search"
        value={searchQuery}
        onChange={(e) => setSearchQuery(e.target.value)}
      />
      <button className="btn btn-outline-success" type="submit">
        Search
      </button>
    </form>
  );
};

export default SearchHandler;
