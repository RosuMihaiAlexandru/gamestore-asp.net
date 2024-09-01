import $api from "../../../http";
import { AxiosResponse } from "axios";
import { IGame } from "../models/IGame";

export default class SortService {
  static getGamesByIdDesc(): Promise<AxiosResponse<IGame[]>> {
    return $api.get<IGame[]>("/sort/getGamesByIdDesc");
  }
  static getGamesByReleaseDate(
    ascending: boolean,
  ): Promise<AxiosResponse<IGame[]>> {
    return $api.get<IGame[]>(`/sort/getGamesByReleaseDate/${ascending}`);
  }
  static getGamesByPrice(ascending: boolean): Promise<AxiosResponse<IGame[]>> {
    return $api.get<IGame[]>(`/sort/getGamesByPrice/${ascending}`);
  }
}
