import { IUser } from "../IUser";

export interface AuthResponse {
  Token: string;
  refreshToken: string;
  user: IUser;
}
