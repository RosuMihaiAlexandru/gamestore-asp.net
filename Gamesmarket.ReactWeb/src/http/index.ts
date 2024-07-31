import axios from "axios";

export const API_URL = `https://localhost:7202/api`;
export const API_URL_IMG = `https://localhost:7202/`;

const $api = axios.create({
  withCredentials: true,
  baseURL: API_URL,
});

$api.interceptors.request.use((config) => {
  config.headers.Authorization = `Bearer ${localStorage.getItem("token")}`;
  return config;
});

export default $api;
