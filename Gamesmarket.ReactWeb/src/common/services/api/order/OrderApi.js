import axios from 'axios';

export async function createOrder(orderData) {
    try {
      const response = await axios.post("https://localhost:7202/api/order/createOrder", orderData);
      return response.data;
    } catch (error) {
      console.error('Error fetching data:', error);
      throw error;
    }
}
export async function deleteOrder(Id) {
    try {
      const response = await axios.delete(`https://localhost:7202/api/order/delete/${Id}`);
      return response.data;
    } catch (error) {
      console.error('Error fetching a game data:', error);
      throw error;
    }
  }