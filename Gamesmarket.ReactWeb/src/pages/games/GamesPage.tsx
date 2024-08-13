import React from "react";
import GameList from "../../core/game/components/GameList";
import { Typography } from "@mui/material";

const GamesPage: React.FC = () => {
  return (
    <div>
      <Typography variant="h4" sx={{ mt: 6, color: "#fff" }}>
        {" "}
        All Games
      </Typography>
      <GameList />
    </div>
  );
};

export default GamesPage;
