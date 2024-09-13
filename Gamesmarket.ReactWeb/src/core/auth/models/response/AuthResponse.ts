import { IUser } from "../IUser";

export interface AuthResponse {
  description: string;
  statusCode: string;
  data: IUser;
}
