import React, { useContext } from "react";
import { Context } from "../../main";
import GameList from "../../core/game/components/GameList";
import { Box, Typography } from "@mui/material";
import NoGames from "./NoGames";

const SearchResultsPage: React.FC = () => {
  const { rootStore } = useContext(Context);
  const { gameStore, filterStore } = rootStore;

  return (
    <div>
      <Box sx={{ mt: 4, color: "#fff" }}>
        <Typography variant="h4">Search Results</Typography>
        {filterStore.errorMessage ? (
          <NoGames />
        ) : (
          <GameList games={gameStore.games} isLoading={gameStore.isLoading} />
        )}
      </Box>
    </div>
  );
};

export default SearchResultsPage;
