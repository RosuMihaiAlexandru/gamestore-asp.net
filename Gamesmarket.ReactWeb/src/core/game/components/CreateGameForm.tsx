import React from "react";
import {
  Grid,
  TextField,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Button,
  Typography,
  Box,
} from "@mui/material";
import { GameGenreOptions } from "../utils/GameGenreOptions";
import {
  handleInputChange,
  handleSelectChange,
  handleImageChange,
  handleSubmit,
} from "../utils/CreateHandlers";
import { IGameCreation } from "../models/IGameCreation";
import GameStore from "../store/GameStore";

interface CreateGameFormProps {
  formData: IGameCreation;
  setFormData: React.Dispatch<React.SetStateAction<IGameCreation>>;
  imagePreview: string | null;
  setImagePreview: (image: string | null) => void;
  gameStore: GameStore;
}

const CreateGameForm: React.FC<CreateGameFormProps> = ({
  formData,
  setFormData,
  imagePreview,
  setImagePreview,
  gameStore,
}) => {
  return (
    <form onSubmit={(e) => handleSubmit(e, formData, gameStore)}>
      <Grid container spacing={4}>
        <Grid item xs={12} md={6}>
          <TextField
            name="name"
            label="Game Name"
            variant="outlined"
            value={formData.name}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
              handleInputChange(e, setFormData)
            }
            fullWidth
            required
            sx={{ mb: 2, backgroundColor: "#fff" }}
          />
          <TextField
            name="developer"
            label="Developer"
            variant="outlined"
            value={formData.developer}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
              handleInputChange(e, setFormData)
            }
            fullWidth
            required
            sx={{ mb: 2, backgroundColor: "#fff" }}
          />
          <TextField
            name="description"
            label="Description"
            variant="outlined"
            multiline
            rows={4}
            value={formData.description}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
              handleInputChange(e, setFormData)
            }
            fullWidth
            required
            sx={{ mb: 2, backgroundColor: "#fff" }}
          />
          <TextField
            name="price"
            label="Price"
            type="number"
            variant="outlined"
            value={formData.price}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
              handleInputChange(e, setFormData)
            }
            fullWidth
            required
            sx={{ mb: 2, backgroundColor: "#fff" }}
          />
          <TextField
            name="releaseDate"
            label="Release Date"
            type="date"
            variant="outlined"
            value={formData.releaseDate}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
              handleInputChange(e, setFormData)
            }
            fullWidth
            required
            sx={{ mb: 2, backgroundColor: "#fff" }}
            InputLabelProps={{
              shrink: true,
            }}
          />
          <FormControl fullWidth sx={{ mb: 2 }}>
            <InputLabel id="game-genre-label">Genre</InputLabel>
            <Select
              labelId="game-genre-label"
              name="gameGenre"
              value={formData.gameGenre}
              onChange={(e) => handleSelectChange(e, setFormData)}
              label="Genre"
              sx={{ backgroundColor: "#fff" }}
            >
              {GameGenreOptions.map((option) => (
                <MenuItem key={option.value} value={option.value}>
                  {option.label}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            sx={{
              mt: 2,
              backgroundColor: "#ff4020",
              color: "#fff",
              "&:hover": {
                backgroundColor: "#e0391d",
              },
            }}
          >
            Add Game
          </Button>
        </Grid>
        <Grid item xs={12} md={6}>
          <Typography variant="body1" sx={{ mb: 2 }}>
            Image must be less than 2 megabytes and have a format of .jpg,
            .webp, or .png.
          </Typography>
          <input
            accept="image/*"
            id="image-upload"
            type="file"
            onChange={(e) => handleImageChange(e, setFormData, setImagePreview)}
            style={{ display: "none" }}
          />
          <label htmlFor="image-upload">
            <Button
              variant="contained"
              component="span"
              sx={{
                mb: 2,
                backgroundColor: "#ff4020",
                color: "#fff",
                "&:hover": {
                  backgroundColor: "#e0391d",
                },
              }}
            >
              Upload Image
            </Button>
          </label>
          {imagePreview && (
            <Box sx={{ textAlign: "center", mt: 2 }}>
              <img
                src={imagePreview}
                alt="Preview"
                style={{ maxWidth: "100%", maxHeight: 300 }}
              />
            </Box>
          )}
        </Grid>
      </Grid>
    </form>
  );
};

export default CreateGameForm;
