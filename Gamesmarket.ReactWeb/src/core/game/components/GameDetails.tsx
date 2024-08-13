import { FC, useContext, useEffect } from "react";
import { useParams } from "react-router-dom";
import { observer } from "mobx-react-lite";
import { Context } from "../../../main";
import Load from "../../../components/UI/Load";
import { API_URL_IMG } from "../../../http";
import {
  Grid,
  Card,
  CardContent,
  CardMedia,
  Typography,
  Box,
  Button,
} from "@mui/material";
import NoGames from "../../../pages/games/NoGames";

const GameDetails: FC = () => {
  const { id } = useParams<{ id: string }>();
  const { rootStore } = useContext(Context);
  const { gameStore } = rootStore;
  const game = gameStore.game;

  useEffect(() => {
    if (id) {
      gameStore.getGame(Number(id));
    }
  }, [id, gameStore]);

  if (gameStore.isLoading) {
    return <Load />;
  }

  if (!gameStore.game || !gameStore.game.id) {
    return <NoGames />;
  }

  return (
    <Box sx={{ mt: 8 }}>
      <Grid container spacing={4}>
        <Grid item xs={12} md={6.5}>
          <Card>
            <CardMedia
              component="img"
              image={`${API_URL_IMG}${game.imagePath}`}
              alt={game.name}
              sx={{ height: 350, objectFit: "cover" }}
            />
          </Card>
        </Grid>

        <Grid item xs={12} md={5.5}>
          <Card
            sx={{
              backgroundColor: "#18181f",
              color: "#fff",
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
            }}
          >
            <CardContent sx={{ width: "100%", textAlign: "center" }}>
              <Typography variant="h4" align="center" sx={{ mb: 12 }}>
                {game.name}
              </Typography>

              <Typography variant="h4" align="center" gutterBottom>
                {game.price}$
              </Typography>

              <Box sx={{ display: "flex", justifyContent: "center", mt: 2 }}>
                <Button
                  variant="contained"
                  color="primary"
                  sx={{
                    width: "75%",
                    backgroundColor: "#ff4020",
                    "&:hover": {
                      backgroundColor: "#e0391d",
                    },
                  }}
                >
                  Add to cart
                </Button>
              </Box>
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12} md={6.5}>
          <Box>
            <Typography variant="h4" sx={{ mb: 3, color: "#fff" }}>
              About
            </Typography>
            <Typography variant="body1" sx={{ color: "#999" }}>
              {game.description}
            </Typography>
          </Box>
        </Grid>

        <Grid item xs={12} md={5.5}>
          <CardContent sx={{ mt: 6.5 }}>
            <Grid container spacing={2}>
              <Grid item xs={6} sx={{ color: "#999" }}>
                <Typography variant="body1" gutterBottom>
                  Developer:
                </Typography>
                <Typography variant="body1" gutterBottom>
                  Release Date:
                </Typography>
                <Typography variant="body1" gutterBottom>
                  Genre:
                </Typography>
              </Grid>
              <Grid item xs={6} sx={{ color: "#fff" }}>
                <Typography variant="body1" gutterBottom>
                  {game.developer}
                </Typography>
                <Typography variant="body1" gutterBottom>
                  {new Date(game.releaseDate).toLocaleDateString()}
                </Typography>
                <Typography variant="body1" gutterBottom>
                  {game.gameGenre}
                </Typography>
              </Grid>
            </Grid>
          </CardContent>
        </Grid>
      </Grid>
    </Box>
  );
};

export default observer(GameDetails);
