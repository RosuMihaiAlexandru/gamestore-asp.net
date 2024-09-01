import { FC, useState, useContext, useEffect } from "react";
import GameList from "../../core/game/components/GameList";
import { Box, SelectChangeEvent, Typography } from "@mui/material";
import { Context } from "../../main";
import { observer } from "mobx-react-lite";
import Filters from "../../core/game/components/UI/GameSelect";

const NewGamesPage: FC = () => {
  const { rootStore } = useContext(Context);
  const { sortStore, filterStore, gameStore } = rootStore;

  const [selectedGenre, setSelectedGenre] = useState<string>("");
  const [sortByDate, setSortByDate] = useState<boolean | null>(null);
  const [sortByPrice, setSortByPrice] = useState<boolean | null>(null);

  useEffect(() => {
    if (selectedGenre) {
      filterStore.searchGamesByGenre(selectedGenre);
    } else if (sortByDate !== null) {
      sortStore.getGamesByReleaseDate(sortByDate);
    } else if (sortByPrice !== null) {
      sortStore.getGamesByPrice(sortByPrice);
    } else {
      sortStore.getGamesByIdDesc();
    }
  }, [
    selectedGenre,
    sortByDate,
    sortByPrice,
    gameStore,
    filterStore,
    sortStore,
  ]);

  const handleGenreChange = (event: SelectChangeEvent<unknown>) => {
    setSelectedGenre(event.target.value as string);
    setSortByDate(null); // Reset other filters
    setSortByPrice(null);
  };

  const handleSortByDateChange = (event: SelectChangeEvent<unknown>) => {
    setSortByDate(event.target.value === "true"); // Convert a string to a boolean
    setSelectedGenre("");
    setSortByPrice(null);
  };

  const handleSortByPriceChange = (event: SelectChangeEvent<unknown>) => {
    setSortByPrice(event.target.value === "true");
    setSelectedGenre("");
    setSortByDate(null);
  };

  return (
    <div>
      <Typography variant="h4" sx={{ mt: 6, color: "#fff" }}>
        New Games
      </Typography>

      <Box sx={{ flexGrow: 1, mt: 4 }}>
        <Filters
          selectedGenre={selectedGenre}
          sortByDate={sortByDate !== null ? sortByDate.toString() : ""}
          sortByPrice={sortByPrice !== null ? sortByPrice.toString() : ""}
          onGenreChange={handleGenreChange}
          onsortByDateChange={handleSortByDateChange}
          onsortByPriceChange={handleSortByPriceChange}
        />
      </Box>

      <GameList games={gameStore.games} isLoading={gameStore.isLoading} />
    </div>
  );
};

export default observer(NewGamesPage);
