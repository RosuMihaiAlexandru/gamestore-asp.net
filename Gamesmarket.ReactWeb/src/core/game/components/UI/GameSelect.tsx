import { FC } from "react";
import { Grid, MenuItem, SelectChangeEvent } from "@mui/material";
import { StyledInputLabel } from "../../styles/StyledInputLabel";
import { StyledSelect } from "../../styles/StyledSelect";

interface FiltersProps {
  selectedGenre: string;
  sortByDate: string;
  sortByPrice: string;
  onGenreChange: (event: SelectChangeEvent<unknown>) => void;
  onsortByDateChange: (event: SelectChangeEvent<unknown>) => void;
  onsortByPriceChange: (event: SelectChangeEvent<unknown>) => void;
}

const Filters: FC<FiltersProps> = ({
  selectedGenre,
  sortByDate,
  sortByPrice,
  onGenreChange,
  onsortByDateChange,
  onsortByPriceChange,
}) => {
  return (
    <Grid container spacing={2} justifyContent="space-between">
      <Grid item xs={12} sm={6} md={4}>
        <StyledInputLabel>Genre</StyledInputLabel>
        <StyledSelect
          value={selectedGenre}
          onChange={onGenreChange}
          label="Genre"
          fullWidth
          displayEmpty
          MenuProps={{
            PaperProps: {
              style: {
                color: "white",
                backgroundColor: "#4a4b50",
              },
            },
          }}
        >
          <MenuItem value="">
            <em>None</em>
          </MenuItem>
          <MenuItem value="RPG">RPG</MenuItem>
          <MenuItem value="Action">Action</MenuItem>
          <MenuItem value="Adventure">Adventure</MenuItem>
          <MenuItem value="Shooter">Shooter</MenuItem>
          <MenuItem value="Simulation">Simulation</MenuItem>
          <MenuItem value="Strategy">Strategy</MenuItem>
          <MenuItem value="Sports">Sports</MenuItem>
          <MenuItem value="Horror">Horror</MenuItem>
          <MenuItem value="Platformer">Platformer</MenuItem>
          <MenuItem value="Fighting">Fighting</MenuItem>
        </StyledSelect>
      </Grid>

      <Grid item xs={12} sm={6} md={4}>
        <StyledInputLabel>Release Date</StyledInputLabel>
        <StyledSelect
          value={sortByDate !== null ? sortByDate.toString() : ""}
          onChange={onsortByDateChange}
          label="Release Date"
          fullWidth
          displayEmpty
          MenuProps={{
            PaperProps: {
              style: {
                color: "white",
                backgroundColor: "#4a4b50",
              },
            },
          }}
        >
          <MenuItem value="">
            <em>None</em>
          </MenuItem>
          <MenuItem value={"true"}>Old to New</MenuItem>
          <MenuItem value={"false"}>New to Old</MenuItem>
        </StyledSelect>
      </Grid>

      <Grid item xs={12} sm={6} md={4}>
        <StyledInputLabel>Price</StyledInputLabel>
        <StyledSelect
          labelId="price-select-label"
          value={sortByPrice !== null ? sortByPrice.toString() : ""}
          onChange={onsortByPriceChange}
          label="Price"
          fullWidth
          displayEmpty
          MenuProps={{
            PaperProps: {
              style: {
                color: "white",
                backgroundColor: "#4a4b50",
              },
            },
          }}
        >
          <MenuItem value="">
            <em>None</em>
          </MenuItem>
          <MenuItem value={"true"}>Low to High</MenuItem>
          <MenuItem value={"false"}>High to Low</MenuItem>
        </StyledSelect>
      </Grid>
    </Grid>
  );
};

export default Filters;
