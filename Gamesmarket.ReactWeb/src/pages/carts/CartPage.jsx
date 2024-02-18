import React, { useState, useEffect } from "react";
import { CartHandler } from "./Utils/CartHandler";
import OrdersList from "./components/OrdersList";
import { isAuthenticated } from "../../pages/accounts/Utils/AuthHandler";

const CartPage = () => {
  const { cartData, error } = CartHandler();

  return (
    <div>
      <h1 className="mt-4 mb-4">Your cart</h1>
      {error && <p>Error: {error}</p>}
      {cartData && <OrdersList orders={cartData} />}
    </div>
  );
};

export default CartPage;
