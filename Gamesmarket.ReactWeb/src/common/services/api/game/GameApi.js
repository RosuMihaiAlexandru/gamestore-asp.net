import axios from "axios";

export async function getGames() {
  try {
    const response = await axios.get(
      "https://localhost:7202/api/game/getGames",
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
}
export async function createGame(gameData) {
  try {
    const token = localStorage.getItem("token");
    const response = await axios.post(
      "https://localhost:7202/api/game/createGame",
      gameData,
      {
        headers: {
          Authorization: `Bearer ${token}`, // Adding a token to the request header
        },
      },
    );
    return response.data;
  } catch (error) {
    console.error("Error creating game:", error);
    throw error;
  }
}
export async function deleteGame(Id) {
  try {
    const token = localStorage.getItem("token");
    const response = await axios.delete(
      `https://localhost:7202/api/game/delete/${Id}`,
      {
        headers: {
          Authorization: `Bearer ${token}`, // Adding a token to the request header
        },
      },
    );
    return response.data;
  } catch (error) {
    console.error("Error deleting game:", error);
    throw error;
  }
}
export async function getGame(Id) {
  try {
    const response = await axios.get(
      `https://localhost:7202/api/game/getGame/${Id}`,
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching a game data:", error);
    throw error;
  }
}
export async function editGame(Id, gameData) {
  try {
    const token = localStorage.getItem("token");
    const response = await axios.patch(
      `https://localhost:7202/api/game/editGame/${Id}`,
      gameData,
      {
        headers: {
          Authorization: `Bearer ${token}`, // Adding a token to the request header
        },
      },
    );
    return response.data;
  } catch (error) {
    console.error("Error editing game:", error);
    throw error;
  }
}
export async function searchGames(searchQuery) {
  try {
    const encodedQuery = encodeURIComponent(searchQuery);
    const response = await axios.get(
      `https://localhost:7202/api/game/findGamesByNameOrDev/${encodedQuery}`,
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching a game data:", error);
    throw error;
  }
}
