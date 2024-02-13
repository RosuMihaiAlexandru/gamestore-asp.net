import axios from "axios";

export async function getDetail() {
  try {
    const response = await axios.get(
      "https://localhost:7202/api/cart/getDetail",
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
}
export async function getItem(Id) {
  try {
    const response = await axios.get(
      `https://localhost:7202/api/cart/getItem/${Id}`,
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching a game data:", error);
    throw error;
  }
}
