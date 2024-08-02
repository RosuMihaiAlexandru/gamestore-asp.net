import { FC, useContext, useEffect, useState } from "react";
import { Context } from "../../../main";
import { Box, Grid, Typography } from "@mui/material";
import GameItem from "./UI/GameItem";
import Load from "./UI/Load";
import NoGames from "../../../pages/games/NoGames";
import StyledPagination from "../styles/StyledPagination";

const GameList: FC = () => {
  const { rootStore } = useContext(Context);
  const { gameStore } = rootStore;
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 6;

  useEffect(() => {
    gameStore.getGames();
  }, [gameStore]);

  if (gameStore.isLoading) {
    return <Load />;
  }

  if (gameStore.games.length === 0) {
    return <NoGames />;
  }

  const handleChangePage = (
    _event: React.ChangeEvent<unknown>,
    value: number,
  ) => {
    setCurrentPage(value);
  };

  const paginatedGames = gameStore.games.slice(
    (currentPage - 1) * itemsPerPage,
    currentPage * itemsPerPage,
  );

  return (
    <Box>
      <Typography variant="h4" sx={{ mt: 6, color: "#fff" }}>
        {" "}
        All Games
      </Typography>
      <Grid container spacing={4}>
        {paginatedGames.map((game) => (
          <GameItem key={game.id} game={game} />
        ))}
      </Grid>
      <StyledPagination
        count={Math.ceil(gameStore.games.length / itemsPerPage)}
        page={currentPage}
        onChange={handleChangePage}
        size="large"
        sx={{ mt: 4, display: "flex", justifyContent: "center" }}
      />
    </Box>
  );
};

export default GameList;
