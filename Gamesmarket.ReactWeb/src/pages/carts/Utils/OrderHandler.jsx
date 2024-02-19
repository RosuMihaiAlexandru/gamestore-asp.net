import { useState } from "react";
import { toast } from "react-toastify";
import {
  createOrder,
  deleteOrder,
} from "../../../common/services/api/order/OrderApi";

const OrderHandler = () => {
  const [error, setError] = useState(null);
  
  const handleDelete = async (id) => {
    try {
      const response = await deleteOrder(id);
      console.log("Order deleted successfully:", response);
    } catch (error) {
      setError(error.message);
    }
  };

  const onCreateOrder = async (orderData) => {
    try {
      const response = await createOrder(orderData);
      console.log("Order created successfully:", response);
      toast.success("Order created successfully!");
    } catch (error) {
      console.error("Error creating order:", error);
      toast.error("Failed to create order.");
    }
  };

  const handleCreateOrder = (gameId) => {
    const orderData = {
      quantity: 1,
      dateCreated: new Date().toISOString(),
      email: localStorage.getItem("username"),
      name: localStorage.getItem("name"),
      gameId: gameId,
      login: localStorage.getItem("username"),
    };
    onCreateOrder(orderData);
  };

  return { handleDelete, error, onCreateOrder, handleCreateOrder };
};

export default OrderHandler;
