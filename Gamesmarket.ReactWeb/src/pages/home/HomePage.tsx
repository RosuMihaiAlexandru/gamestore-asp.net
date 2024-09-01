import { FC, useContext, useEffect, useState } from "react";
import { Box, Button, Typography } from "@mui/material";
import { Context } from "../../main";
import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router-dom";
import GameList from "../../core/game/components/GameList";
import { IGame } from "../../core/game/models/IGame";

const HomePage: FC = () => {
  const { rootStore } = useContext(Context);
  const { gameStore, sortStore, filterStore } = rootStore;
  const navigate = useNavigate();

  const [rpgGames, setRpgGames] = useState<IGame[]>([]);
  const [latestGames, setLatestGames] = useState<IGame[]>([]);
  const [cheapestGames, setCheapestGames] = useState<IGame[]>([]);

  useEffect(() => {
    const fetchGames = async () => {
      const rpg = await filterStore.searchGamesByGenre("RPG");
      setRpgGames(rpg.slice(0, 3));

      const latest = await sortStore.getGamesByReleaseDate(false); // New to Old
      setLatestGames(latest.slice(0, 3));

      const cheapest = await sortStore.getGamesByPrice(true); // Low to High
      setCheapestGames(cheapest.slice(0, 3));
    };

    fetchGames();
  }, [filterStore, sortStore]);

  const goToGamesPage = () => {
    navigate("/games");
  };

  return (
    <div>
      <Typography
        variant="h3"
        sx={{ mt: 6, color: "#fff", textAlign: "center" }}
      >
        Welcome to the Game Store
      </Typography>

      <Box sx={{ mt: 6 }}>
        <Button
          variant="text"
          sx={{ color: "#fff", textTransform: "none", p: 0 }}
          onClick={goToGamesPage}
        >
          <Typography variant="h5" sx={{ color: "#fff" }}>
            The Latest Games &gt;
          </Typography>
        </Button>
        <GameList
          games={latestGames}
          isLoading={gameStore.isLoading}
          hidePagination
        />
      </Box>

      <Box sx={{ mt: 6 }}>
        <Button
          variant="text"
          sx={{ color: "#fff", textTransform: "none", p: 0 }}
          onClick={goToGamesPage}
        >
          <Typography variant="h5" sx={{ color: "#fff" }}>
            RPG Games &gt;
          </Typography>
        </Button>
        <GameList
          games={rpgGames}
          isLoading={gameStore.isLoading}
          hidePagination
        />
      </Box>

      <Box sx={{ mt: 6 }}>
        <Button
          sx={{ color: "#fff", textTransform: "none", p: 0 }}
          onClick={goToGamesPage}
        >
          <Typography variant="h5" sx={{ color: "#fff" }}>
            The Cheapest Games &gt;
          </Typography>
        </Button>
        <GameList
          games={cheapestGames}
          isLoading={gameStore.isLoading}
          hidePagination
        />
      </Box>
    </div>
  );
};

export default observer(HomePage);
