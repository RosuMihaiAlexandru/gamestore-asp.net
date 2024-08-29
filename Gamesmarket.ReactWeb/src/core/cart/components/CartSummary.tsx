import { FC, useContext } from "react";
import { observer } from "mobx-react-lite";
import { Box, Typography, Button } from "@mui/material";
import { Context } from "../../../main";

const CartSummary: FC = () => {
  const { rootStore } = useContext(Context);
  const { cartStore } = rootStore;

  const handleCheckout = () => {
    cartStore.showSnack("Proceed to checkout.", "success");
    return;
  };

  return (
    <Box
      sx={{
        backgroundColor: "#2c2f33",
        color: "#fff",
        p: 2,
        borderRadius: "8px",
        maxWidth: "300px",
      }}
    >
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          mb: 2,
        }}
      >
        <Typography variant="h6" sx={{ fontSize: "16px" }}>
          Total cost:
        </Typography>
        <Typography variant="h6" sx={{ fontSize: "16px" }}>
          {cartStore.totalPrice}$
        </Typography>
      </Box>
      <Button
        variant="contained"
        color="primary"
        sx={{
          mt: 2,
          backgroundColor: "#ff4020",
          color: "white",
          "&:hover": {
            backgroundColor: "#e0391d",
          },
        }}
        fullWidth
        onClick={handleCheckout}
        disabled={cartStore.orders.length === 0}
      >
        Proceed to Checkout
      </Button>
    </Box>
  );
};

export default observer(CartSummary);
