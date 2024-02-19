import axios from "axios";

export async function createOrder(orderData) {
  try {
    const token = localStorage.getItem("token");
    const response = await axios.post(
      "https://localhost:7202/api/order/createOrder",
      orderData,
      {
        headers: {
          Authorization: `Bearer ${token}`, // Adding a token to the request header
        },
      },
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
}
export async function deleteOrder(Id) {
  try {
    const token = localStorage.getItem("token");
    const response = await axios.delete(
      `https://localhost:7202/api/order/delete/${Id}`,
      {
        headers: {
          Authorization: `Bearer ${token}`, // Adding a token to the request header
        },
      },
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching a game data:", error);
    throw error;
  }
}
