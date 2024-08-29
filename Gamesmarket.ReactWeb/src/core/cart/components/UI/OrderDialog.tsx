import { FC } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Grid,
  Typography,
} from "@mui/material";
import Load from "../../../../components/UI/Load";

interface OrderDialogProps {
  open: boolean;
  onClose: () => void;
  order: any;
  isLoading: boolean;
}

const OrderDialog: FC<OrderDialogProps> = ({
  open,
  onClose,
  order,
  isLoading,
}) => {
  return (
    <Dialog open={open} onClose={onClose} fullWidth>
      <DialogTitle sx={{ m: 0, p: 2, textAlign: "center" }} id="dialog-title">
        Order details
      </DialogTitle>
      {isLoading ? (
        <Load />
      ) : (
        <DialogContent dividers>
          <Grid container>
            <Grid item xs={6}>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                Game Name:
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                Developer:
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                Genre:
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                Price:
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                Customer:
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                Email:
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                Order Date:
              </Typography>
            </Grid>
            <Grid item xs={6}>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                {order.gameName}
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                {order.gameDeveloper}
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                {order.gameGenre}
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                {order.gamePrice}$
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                {order.name}
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                {order.email}
              </Typography>
              <Typography variant="body1" sx={{ marginBottom: 1 }}>
                {order.dateCreate}
              </Typography>
            </Grid>
          </Grid>
        </DialogContent>
      )}
      <DialogActions>
        <Button autoFocus onClick={onClose} color="primary">
          Close
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default OrderDialog;
