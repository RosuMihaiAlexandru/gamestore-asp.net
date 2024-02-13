import axios from "axios";

export async function login(AuthRequest) {
  try {
    const response = await axios.post(
      "https://localhost:7202/api/account/login",
      AuthRequest,
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
}
export async function register(RegisterRequest) {
  try {
    const response = await axios.post(
      "https://localhost:7202/api/account/register",
      RegisterRequest,
    );
    return response.data;
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
    const response = await axios.get(
      "https://localhost:7202/api/account/getUsers",
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
}