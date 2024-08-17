import $api from "../../../http";
import { AxiosResponse } from "axios";
import { IGame } from "../models/IGame";
import { GamesResponse } from "../models/response/GamesResponse";
import { DeleteGameResponse } from "../models/response/DeleteGameResponse";

export default class GameService {
  static getGames(): Promise<AxiosResponse<IGame[]>> {
    return $api.get<IGame[]>("/game/getGames");
  }

  static getGame(id: number): Promise<AxiosResponse<IGame>> {
    return $api.get<IGame>(`/game/getGame/${id}`);
  }

  static createGame(formData: FormData): Promise<AxiosResponse<GamesResponse>> {
    return $api.post<GamesResponse>("/game/createGame", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
  }

  static deleteGame(id: number): Promise<AxiosResponse<DeleteGameResponse>> {
    return $api.delete<DeleteGameResponse>(`/game/delete/${id}`);
  }

  static searchGames(searchQuery: string): Promise<AxiosResponse<IGame[]>> {
    const encodedQuery = encodeURIComponent(searchQuery);
    return $api.get<IGame[]>(`/game/findGamesByNameOrDev/${encodedQuery}`);
  }
}
