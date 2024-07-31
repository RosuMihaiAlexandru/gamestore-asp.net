import { FC, useContext, useEffect, useState } from "react";
import { Context } from "../../../main";
import { Box, Grid, Pagination } from "@mui/material";
import GameItem from "./UI/GameItem";
import Load from "./UI/Load";

const GameList: FC = () => {
  const { rootStore } = useContext(Context);
  const { gameStore } = rootStore;
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 30;

  useEffect(() => {
    gameStore.getGames();
  }, [gameStore]);

  if (gameStore.isLoading) {
    return <Load />;
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
      <Grid container spacing={3}>
        {paginatedGames.map((game) => (
          <GameItem key={game.id} game={game} />
        ))}
      </Grid>
      <Pagination
        count={Math.ceil(gameStore.games.length / itemsPerPage)}
        page={currentPage}
        onChange={handleChangePage}
        sx={{ mt: 4, display: "flex", justifyContent: "center" }}
      />
    </Box>
  );
};

export default GameList;
