import { useState, useEffect } from "react";
import { getDetail, getItem } from "../../../common/services/api/cart/CartApi";

export const CartHandler = () => {
  const [cartData, setCartData] = useState([]);
  const [orderDetails, setOrderDetails] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchCartDetail = async () => {
      try {
        const data = await getDetail();
        setCartData(data);
      } catch (error) {
        setError(error.message);
      }
    };

    fetchCartDetail();
  }, []);

  const fetchOrderDetails = async (orderId) => {
    try {
      const data = await getItem(orderId);
      setOrderDetails(data);
    } catch (error) {
      setError(error.message);
    }
  };

  return { cartData, orderDetails, error, fetchOrderDetails };
};
