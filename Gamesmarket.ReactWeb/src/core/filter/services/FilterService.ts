import $api from "../../../http";
import { AxiosResponse } from "axios";
import { IGame } from "../models/IGame";

export default class FilterService {
  static searchGames(searchQuery: string): Promise<AxiosResponse<IGame[]>> {
    const encodedQuery = encodeURIComponent(searchQuery);
    return $api.get<IGame[]>(`/filter/findGamesByNameOrDev/${encodedQuery}`);
  }

  static searchGamesByGenre(
    searchGenre: string,
  ): Promise<AxiosResponse<IGame[]>> {
    return $api.get<IGame[]>(`/filter/getGamesByGenre/${searchGenre}`);
  }
}
