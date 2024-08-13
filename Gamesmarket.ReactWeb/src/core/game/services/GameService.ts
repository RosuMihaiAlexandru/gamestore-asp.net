import $api from "../../../http";
import { AxiosResponse } from "axios";
import { IGame } from "../models/IGame";

export default class GameService {
  static getGames(): Promise<AxiosResponse<IGame[]>> {
    return $api.get<IGame[]>("/game/getGames");
  }

  static getGame(id: number): Promise<AxiosResponse<IGame>> {
    return $api.get<IGame>(`/game/getGame/${id}`);
  }

  static searchGames(searchQuery: string): Promise<AxiosResponse<IGame[]>> {
    const encodedQuery = encodeURIComponent(searchQuery);
    return $api.get<IGame[]>(`/game/findGamesByNameOrDev/${encodedQuery}`);
  }
}
