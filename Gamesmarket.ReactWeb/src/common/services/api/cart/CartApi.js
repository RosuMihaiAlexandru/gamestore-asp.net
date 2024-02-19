import axios from "axios";

export async function getDetail() {
  try {
    const token = localStorage.getItem("token");
    const response = await axios.get(
      "https://localhost:7202/api/cart/getDetail",
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
export async function getItem(Id) {
  try {
    const token = localStorage.getItem("token");
    const response = await axios.get(
      `https://localhost:7202/api/cart/getItem?id=${Id}`,
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
