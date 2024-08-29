import { FC, useContext } from "react";
import { observer } from "mobx-react-lite";
import { Box, Grid, Typography } from "@mui/material";
import CartsForm from "../../core/cart/components/CartsForm";
import CartSummary from "../../core/cart/components/CartSummary";
import Snack from "../../components/UI/Snack";
import { Context } from "../../main";

const CartPage: FC = () => {
  const { rootStore } = useContext(Context);
  const { cartStore } = rootStore;

  return (
    <Box sx={{ mt: 8, p: 4 }}>
      <Typography variant="h4" color="#ffffff">
        Your cart
      </Typography>
      <Grid container spacing={4}>
        <Grid item xs={12} md={8}>
          <CartsForm />
        </Grid>
        <Grid item xs={12} md={4}>
          <CartSummary />
        </Grid>
      </Grid>
      <Snack
        isOpen={cartStore.snackOpen}
        handleClose={() => cartStore.closeSnack()}
        message={cartStore.snackMessage}
        severity={cartStore.snackSeverity}
      />
    </Box>
  );
};

export default observer(CartPage);
