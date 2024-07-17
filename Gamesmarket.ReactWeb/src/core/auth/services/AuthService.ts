import $api from "../../../http";
import { AxiosResponse } from "axios";
import { AuthResponse } from "../models/response/AuthResponse";

export default class AuthService {
  static async login(
    email: string,
    password: string,
  ): Promise<AxiosResponse<AuthResponse>> {
    return $api.post<AuthResponse>("account/login", { email, password });
  }

  static async register(
    email: string,
    birthDate: Date | null,
    password: string,
    passwordConfirm: string,
    name: string,
  ): Promise<AxiosResponse<AuthResponse>> {
    return $api.post<AuthResponse>("account/register", {
      email,
      birthDate: birthDate ? birthDate.toISOString() : null, // Convert birthDate to string
      password,
      passwordConfirm,
      name,
    });
  }
}
