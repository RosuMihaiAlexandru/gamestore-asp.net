import { FC, useContext, useEffect, useState } from "react";
import { observer } from "mobx-react-lite";
import {
  Box,
  Button,
  List,
  ListItem,
  ListItemAvatar,
  Typography,
} from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import InfoOutlinedIcon from "@mui/icons-material/InfoOutlined";
import { Context } from "../../../main";
import Load from "../../../components/UI/Load";
import { API_URL_IMG } from "../../../http";
import Snack from "../../../components/UI/Snack";
import { useNavigate } from "react-router-dom";
import OrderDialog from "./UI/OrderDialog";

const CartsForm: FC = () => {
  const navigate = useNavigate();
  const { rootStore } = useContext(Context);
  const { cartStore, orderStore } = rootStore;

  const [open, setOpen] = useState(false);

  useEffect(() => {
    cartStore.getOrders();
  }, [cartStore]);

  const handleDialog = (id: number) => {
    setOpen(true);
    cartStore.getOrder(id);
  };

  const handleDialogClose = () => {
    setOpen(false);
  };

  const handleDelete = async (id: number) => {
    await orderStore.deleteOrder(id);
    cartStore.getOrders();
  };

  const handleImageClick = (gameId: number) => {
    navigate(`/game/${gameId}`);
  };

  if (orderStore.isLoading || cartStore.isLoading) {
    return <Load />;
  }

  return (
    <Box
      sx={{
        color: "#fff",
        p: 2,
        borderRadius: "8px",
        backgroundColor: "#1d1b21",
      }}
    >
      {!cartStore.orders.length ? (
        <Typography variant="h6" sx={{ textAlign: "center", mt: 4 }}>
          No orders found for the user :(
        </Typography>
      ) : (
        <List>
          {cartStore.orders.map((order) => (
            <>
              <ListItem
                key={order.id}
                sx={{
                  alignItems: "flex-start",
                  mb: 4,
                  p: 2,
                  borderRadius: "8px",
                  backgroundColor: "#2c2f33",
                }}
              >
                <ListItemAvatar>
                  <img
                    src={`${API_URL_IMG}${order.imagePath}`}
                    alt={order.gameName}
                    style={{
                      width: 200,
                      height: 100,
                      flexShrink: 0,
                      cursor: "pointer",
                    }}
                    onClick={() => handleImageClick(order.gameId)}
                  />
                </ListItemAvatar>
                <Box sx={{ color: "#fff", flexGrow: 1, ml: 2 }}>
                  <Typography
                    variant="h6"
                    sx={{ mb: 1, fontSize: "16px", textAlign: "left" }}
                  >
                    {order.gameName}
                  </Typography>
                </Box>

                <Box sx={{ textAlign: "right" }}>
                  <Typography
                    variant="h6"
                    sx={{
                      color: "#c6d4df",
                      fontSize: "16px",
                      textAlign: "center",
                      mt: 4,
                    }}
                  >
                    {order.gamePrice}$
                  </Typography>

                  <Box
                    sx={{
                      display: "flex",
                      alignItems: "center",
                      justifyContent: "flex-end",
                      mt: 2,
                    }}
                  >
                    <Button
                      variant="outlined"
                      startIcon={<InfoOutlinedIcon />}
                      onClick={() => handleDialog(order.id)}
                      sx={{
                        color: "#fff",
                        textTransform: "none",
                        fontSize: "14px",
                        paddingRight: "8px",
                        paddingLeft: "8px",
                      }}
                    >
                      Details
                    </Button>
                    <Typography sx={{ margin: "0 8px" }}>||</Typography>
                    <Button
                      variant="outlined"
                      startIcon={<DeleteIcon />}
                      onClick={() => handleDelete(order.id)}
                      sx={{
                        color: "#fff",
                        textTransform: "none",
                        fontSize: "14px",
                        paddingRight: "8px",
                        paddingLeft: "8px",
                      }}
                    >
                      Delete
                    </Button>
                  </Box>
                </Box>
              </ListItem>
            </>
          ))}
        </List>
      )}

      <OrderDialog
        open={open}
        onClose={handleDialogClose}
        order={cartStore.order}
        isLoading={cartStore.isLoading}
      />

      <Snack
        isOpen={orderStore.snackOpen}
        handleClose={() => orderStore.closeSnack()}
        message={orderStore.snackMessage}
        severity={orderStore.snackSeverity}
      />
    </Box>
  );
};

export default observer(CartsForm);
