import $api from "../../../http";
import { AxiosResponse } from "axios";
import { IUsers } from "../models/IUsers";

export default class UserService {
  static fetchUsers(): Promise<AxiosResponse<IUsers[]>> {
    return $api.get<IUsers[]>("/account/getUsers");
  }
}
