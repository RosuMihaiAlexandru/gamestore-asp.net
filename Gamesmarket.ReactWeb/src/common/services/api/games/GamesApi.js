import axios from 'axios';

export async function GetGames() {
    try {
      const response = await axios.get("https://localhost:7202/api/Game/GetGames");
      return response.data;
    } catch (error) {
      console.error('Error fetching data:', error);
      throw error;
    }
}
export async function CreateGame(gameData) {
  try {
    const response = await axios.post("https://localhost:7202/api/Game/CreateGame", gameData);
    return response.data;
  } catch (error) {
    console.error('Error creating game:', error);
    throw error;
  }
}
export async function DeleteGame(Id) {
  try {
    const response = await axios.delete(`https://localhost:7202/api/Game/Delete/${Id}`);
    return response.data;
  } catch (error) {
    console.error('Error deleting game:', error);
    throw error;
  }
}
export async function GetGame(Id) {
  try {
    const response = await axios.get(`https://localhost:7202/api/Game/GetGame/${Id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching a game data:', error);
    throw error;
  }
}
export async function EditGame(Id, gameData) {
  try {
    const response = await axios.patch(`https://localhost:7202/api/Game/EditGame/${Id}`, gameData);
    return response.data;
  } catch (error) {
    console.error('Error editing game:', error);
    throw error;
  }
}
  