import React, { useState } from "react";
import { CartHandler } from "./Utils/CartHandler";
import OrdersList from "./components/OrdersList";
import ModalOrder from "./components/ModalOrder";

const CartPage = () => {
  const { cartData, error, fetchOrderDetails, orderDetails } = CartHandler();
  const [isModalVisible, setIsModalVisible] = useState(false);

  const handleOrderClick = (orderId) => {
    fetchOrderDetails(orderId);
    setIsModalVisible(true);
  };

  return (
    <div>
      <h1 className="mt-4 mb-4">Your Cart</h1>
      {error && <p>Error: {error}</p>}
      {cartData && (
        <OrdersList orders={cartData} onOrderClick={handleOrderClick} />
      )}
      <ModalOrder
        orderDetails={orderDetails}
        visible={isModalVisible}
        setVisible={setIsModalVisible}
      />
    </div>
  );
};

export default CartPage;
