import { TextField, IconButton, Box } from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";

function Search() {
  return (
    <Box sx={{ display: "flex", alignItems: "center", flexGrow: 2 }}>
      <TextField
        type="search"
        placeholder="What are you looking for?"
        variant="outlined"
        sx={{
          flexGrow: 1,
          backgroundColor: "white",
          borderRadius: "4px 0 0 4px",
          "& .MuiOutlinedInput-root": {
            height: "50px",
          },
        }}
      />
      <IconButton
        sx={{
          backgroundColor: "#1769aa",
          color: "white",
          borderRadius: "0 4px 4px 0",
          padding: "10px 15px",
          height: "50px",
        }}
      >
        <SearchIcon />
      </IconButton>
    </Box>
  );
}

export default Search;
