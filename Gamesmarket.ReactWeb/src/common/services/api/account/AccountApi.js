import axios from "axios";

export async function login(email, password) {
  try {
    const response = await axios.post(
      "https://localhost:7202/api/account/login",
      { email, password },
    );
    // Saving the data to local storage
    const accessToken = response.data.token;
    const userRole = response.data.role;
    const userUsername = response.data.username;
    localStorage.setItem("token", accessToken);
    localStorage.setItem("role", userRole);
    localStorage.setItem("username", userUsername);
    return response.data; // Returning all user data
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
}
export async function register(userData) {
  try {
    const response = await axios.post(
      "https://localhost:7202/api/account/register",
      userData,
    );
    // Saving the data to local storage
    const accessToken = response.data.token;
    const userRole = response.data.role;
    const userUsername = response.data.username;
    localStorage.setItem("token", accessToken);
    localStorage.setItem("role", userRole);
    localStorage.setItem("username", userUsername);
    return response.data; // Returning all user data
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
}

export async function refreshToken(TokenModel) {
  try {
    const response = await axios.post(
      "https://localhost:7202/api/account/refresh-token",
      TokenModel,
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
}
export async function revoke(username) {
  try {
    const response = await axios.post(
      "https://localhost:7202/api/account/revoke",
      username,
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
}
export async function revokeAll() {
  try {
    const response = await axios.post(
      "https://localhost:7202/api/account/revoke-all",
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
}
export async function getUsers() {
  try {
    const token = localStorage.getItem("token");
    const response = await axios.get(
      "https://localhost:7202/api/account/getUsers",
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
