import React, { useState, useContext } from "react";
import { Box, Typography, Paper } from "@mui/material";
import { Context } from "../../main";
import Snack from "../../components/UI/Snack";
import CreateGameForm from "../../core/game/components/CreateGameForm";
import { IGameCreation } from "../../core/game/models/IGameCreation";
import { observer } from "mobx-react-lite";

const CreateGamePage: React.FC = () => {
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
          Add a New Game
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

export default observer(CreateGamePage);
