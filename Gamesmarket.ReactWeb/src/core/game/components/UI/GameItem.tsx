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

interface GameItemProps {
  game: IGame;
}

const GameItem: FC<GameItemProps> = ({ game }) => {
  return (
    <Grid item xs={12} md={4}>
      <Card sx={{ mt: 6 }}>
        <div key={game.id}>
          <CardMedia
            image={`${API_URL_IMG}${game.imagePath}`}
            component="img"
            alt={`Thumbnail for ${game.name}`}
            title="green iguana"
            sx={{ width: "100%", height: 250, objectFit: "cover" }}
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
