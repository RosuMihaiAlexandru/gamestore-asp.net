import { makeAutoObservable, runInAction } from "mobx";
import { IGame } from "../models/IGame";
import GameService from "../services/GameService";

export default class GameStore {
  game = {} as IGame;
  games: IGame[] = [];
  isLoading = false;

  constructor() {
    makeAutoObservable(this);
  }

  async getGames() {
    this.isLoading = true;
    try {
      const response = await GameService.getGames();
      runInAction(() => {
        this.games = response.data;
        this.isLoading = false;
      });
    } catch (error) {
      runInAction(() => {
        console.error("Failed to get games", error);
        this.isLoading = false;
      });
    }
  }

  async getGame(id: number) {
    this.isLoading = true;
    try {
      const response = await GameService.getGame(id);
      runInAction(() => {
        this.game = response.data;
        this.isLoading = false;
      });
    } catch (error) {
      runInAction(() => {
        console.error("Failed to get game", error);
        this.isLoading = false;
      });
    }
  }
}
