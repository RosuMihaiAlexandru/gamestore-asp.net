import React, { useState, useContext, useEffect } from "react";
import { Box, Typography, Paper } from "@mui/material";
import { Context } from "../../main";
import Snack from "../../components/UI/Snack";
import CreateGameForm from "../../core/game/components/CreateGameForm";
import { IGameCreation } from "../../core/game/models/IGameCreation";
import { observer } from "mobx-react-lite";
import { useParams, useNavigate } from "react-router-dom";
import { GameGenreOptions } from "../../core/game/utils/GameGenreOptions";
import { API_URL_IMG } from "../../http";

const EditGamePage: React.FC = () => {
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const { rootStore } = useContext(Context);
  const { gameStore } = rootStore;

  const [formData, setFormData] = useState<IGameCreation>({
    id: 0,
    name: "",
    developer: "",
    description: "",
    price: "",
    releaseDate: "",
    gameGenre: 0,
    imageFile: null,
  });

  const [imagePreview, setImagePreview] = useState<string | null>(null);
  useEffect(() => {
    if (id) {
      gameStore.getGame(Number(id)).then(() => {
        if (!gameStore.game.id) {
          navigate("/*");
        }
        const { game } = gameStore;
        const genreOption = GameGenreOptions.find(
          (option) => option.label === game.gameGenre,
        );
        setFormData({
          id: game.id,
          name: game.name,
          developer: game.developer,
          description: game.description,
          price: game.price,
          releaseDate: new Date(game.releaseDate).toISOString().split("T")[0],
          gameGenre: genreOption ? genreOption.value : 0,
          imageFile: null,
        });
        setImagePreview(`${API_URL_IMG}${game.imagePath}`);
      });
    }
  }, [id, gameStore]);

  return (
    <Box
      sx={{
        mt: 6,
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        color: "#fff",
      }}
    >
      <Paper
        elevation={24}
        sx={{
          p: 4,
          maxWidth: 1200,
          width: "100%",
          backgroundColor: "#2c2f33",
          color: "#fff",
        }}
      >
        <Typography variant="h4" align="center" sx={{ mb: 4 }}>
          Edit Game
        </Typography>

        <CreateGameForm
          formData={formData}
          setFormData={setFormData}
          imagePreview={imagePreview}
          setImagePreview={setImagePreview}
          gameStore={gameStore}
        />
        <Snack
          isOpen={gameStore.snackOpen}
          handleClose={gameStore.closeSnack.bind(gameStore)}
          message={gameStore.snackMessage}
          severity={gameStore.snackSeverity}
        />
      </Paper>
    </Box>
  );
};

export default observer(EditGamePage);
