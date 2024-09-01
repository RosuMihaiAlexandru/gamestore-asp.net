import { FC, useState } from "react";
import { Box, Grid } from "@mui/material";
import GameItem from "./UI/GameItem";
import Load from "../../../components/UI/Load";
import NoGames from "../../../pages/games/NoGames";
import { StyledPagination } from "../styles/StyledPagination";
import { observer } from "mobx-react-lite";
import { IGame } from "../models/IGame";

interface GameListProps {
  games: IGame[];
  isLoading: boolean;
  hidePagination?: boolean;
}

const GameList: FC<GameListProps> = ({ games, isLoading, hidePagination }) => {
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 9;

  if (isLoading) {
    return <Load />;
  }

  if (games.length === 0) {
    return <NoGames />;
  }

  const handleChangePage = (
    _event: React.ChangeEvent<unknown>,
    value: number,
  ) => {
    setCurrentPage(value);
  };

  const paginatedGames = games.slice(
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
      {!hidePagination && (
        <StyledPagination
          count={Math.ceil(games.length / itemsPerPage)}
          page={currentPage}
          onChange={handleChangePage}
          size="large"
          sx={{ mt: 4, display: "flex", justifyContent: "center" }}
        />
      )}
    </Box>
  );
};

export default observer(GameList);
