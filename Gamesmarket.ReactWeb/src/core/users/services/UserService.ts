import $api from "../../../http";
import { AxiosResponse } from "axios";
import { IUsers } from "../models/IUsers";
import { RoleChangeResponse } from "../models/response/RoleChangeResponse";

export default class UserService {
  static fetchUsers(): Promise<AxiosResponse<IUsers[]>> {
    return $api.get<IUsers[]>("/account/getUsers");
  }

  static async changeRole(
    email: string,
    newRole: string,
  ): Promise<AxiosResponse<RoleChangeResponse>> {
    return $api.post<RoleChangeResponse>("account/change-role", { email, newRole });
  }
}
