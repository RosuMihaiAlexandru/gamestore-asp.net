import axios from 'axios';

export async function Login(AuthRequest) {
    try {
      const response = await axios.post("https://localhost:7202/api/accounts/login", AuthRequest);
      return response.data;
    } catch (error) {
      console.error('Error fetching data:', error);
      throw error;
    }
}
export async function Register(RegisterRequest) {
    try {
      const response = await axios.post("https://localhost:7202/api/accounts/register", RegisterRequest);
      return response.data;
    } catch (error) {
      console.error('Error fetching data:', error);
      throw error;
    }
}
export async function RefreshToken(TokenModel) {
    try {
      const response = await axios.post("https://localhost:7202/api/accounts/refresh-token", TokenModel);
      return response.data;
    } catch (error) {
      console.error('Error fetching data:', error);
      throw error;
    }
}
export async function Revoke(username) {
    try {
      const response = await axios.post("https://localhost:7202/api/accounts/revoke", username);
      return response.data;
    } catch (error) {
      console.error('Error fetching data:', error);
      throw error;
    }
}
export async function RevokeAll() {
    try {
      const response = await axios.post("https://localhost:7202/api/accounts/revoke-all");
      return response.data;
    } catch (error) {
      console.error('Error fetching data:', error);
      throw error;
    }
}
