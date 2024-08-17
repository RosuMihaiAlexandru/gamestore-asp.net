import { FC, useContext, useEffect, useState } from "react";
import { Context } from "../../../main";
import { Box, Grid } from "@mui/material";
import GameItem from "./UI/GameItem";
import Load from "../../../components/UI/Load";
import NoGames from "../../../pages/games/NoGames";
import StyledPagination from "../styles/StyledPagination";
import { observer } from "mobx-react-lite";

const GameList: FC = () => {
  const { rootStore } = useContext(Context);
  const { gameStore } = rootStore;
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 9;

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

export default observer(GameList);
