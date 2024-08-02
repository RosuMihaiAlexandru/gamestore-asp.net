import { FC } from "react";
import {
  Box,
  Card,
  CardContent,
  CardMedia,
  Grid,
  Typography,
} from "@mui/material";
import { observer } from "mobx-react-lite";
import { IGame } from "../../models/IGame";
import { API_URL_IMG } from "../../../../http";
import { useNavigate } from "react-router-dom";

interface GameItemProps {
  game: IGame;
}

const GameItem: FC<GameItemProps> = ({ game }) => {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/game/${game.id}`);
  };

  return (
    <Grid item xs={12} md={4}>
      <Card
        sx={{
          mt: 6,
          backgroundColor: "#18161c",
          color: "#fff",
          boxShadow: "none",
        }}
        onClick={handleClick}
        style={{ cursor: "pointer" }}
      >
        <div key={game.id}>
          <CardMedia
            image={`${API_URL_IMG}${game.imagePath}`}
            component="img"
            alt={`Thumbnail for ${game.name}`}
            title="green iguana"
            sx={{
              width: "100%",
              height: 250,
              objectFit: "cover",
              maxHeight: "220px",
            }}
          />
          <CardContent>
            <Box
              sx={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
              }}
            >
              <Typography variant="body1">{game.name}</Typography>
              <Typography variant="h6">{game.price}$</Typography>
            </Box>
          </CardContent>
        </div>
      </Card>
    </Grid>
  );
};

export default observer(GameItem);
