import { makeAutoObservable, runInAction } from "mobx";
import { IGame } from "../models/IGame";
import GameService from "../services/GameService";
import FilterService from "../../filter/services/FilterService";

export default class GameStore {
  game = {} as IGame;
  games: IGame[] = [];
  isLoading = false;
  errorMessage: string | null = null;
  snackMessage: string = "";
  snackSeverity: "success" | "error" = "success";
  snackOpen: boolean = false;

  constructor() {
    makeAutoObservable(this);
  }

  closeSnack() {
    this.snackOpen = false;
  }

  showSnack(message: string, severity: "success" | "error" = "success") {
    this.snackMessage = message;
    this.snackSeverity = severity;
    this.snackOpen = true;
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

  async createGame(
    id: number,
    name: string,
    developer: string,
    description: string,
    price: string,
    releaseDate: Date,
    gameGenre: string,
    imageFile: File,
  ) {
    this.isLoading = true;
    try {
      const formData = new FormData();
      formData.append("id", id.toString());
      formData.append("name", name);
      formData.append("developer", developer);
      formData.append("description", description);
      formData.append("price", price);
      formData.append("releaseDate", releaseDate.toISOString());
      formData.append("gameGenre", gameGenre);
      formData.append("imageFile", imageFile);

      const response = await GameService.createGame(formData);

      runInAction(() => {
        this.isLoading = false;
        this.snackMessage = response.data.description;
        this.snackSeverity = "success";
        this.snackOpen = true;
      });
    } catch (e: any) {
      runInAction(() => {
        console.log(e.response?.data?.message);
        this.isLoading = false;
        this.snackMessage =
          e.response?.data.description || "Failed to create a game";
        this.snackSeverity = "error";
        this.snackOpen = true;
      });
    }
  }

  async editGame(
    id: number,
    name: string,
    developer: string,
    description: string,
    price: string,
    releaseDate: Date,
    gameGenre: string,
    imageFile: File,
  ) {
    this.isLoading = true;
    try {
      const formData = new FormData();
      formData.append("id", id.toString());
      formData.append("name", name);
      formData.append("developer", developer);
      formData.append("description", description);
      formData.append("price", price);
      formData.append("releaseDate", releaseDate.toISOString());
      formData.append("gameGenre", gameGenre);
      formData.append("imageFile", imageFile);

      const response = await GameService.editGame(id, formData);

      runInAction(() => {
        this.isLoading = false;
        this.snackMessage = response.data.description;
        this.snackSeverity = "success";
        this.snackOpen = true;
      });
    } catch (e: any) {
      runInAction(() => {
        console.log(e.response?.data?.message);
        this.isLoading = false;
        this.snackMessage =
          e.response?.data.description || "Failed to edit a game";
        this.snackSeverity = "error";
        this.snackOpen = true;
      });
    }
  }

  async deleteGame(id: number) {
    this.isLoading = true;
    try {
      const response = await GameService.deleteGame(id);
      runInAction(() => {
        this.snackMessage = response.data.description;
        this.snackSeverity = "success";
        this.snackOpen = true;
        this.isLoading = false;
      });
    } catch (e: any) {
      runInAction(() => {
        console.log(e.response?.data?.description);
        this.snackMessage =
          e.response?.data.message || "Failed to delete the game";
        this.snackSeverity = "error";
        this.snackOpen = true;
        this.isLoading = false;
      });
    }
  }

  async searchGames(searchQuery: string) {
    this.isLoading = true;
    this.errorMessage = null;
    try {
      const response = await FilterService.searchGames(searchQuery);
      runInAction(() => {
        this.games = response.data;
        this.isLoading = false;
      });
    } catch (e: any) {
      runInAction(() => {
        this.errorMessage = "Game not found";
        console.error(this.errorMessage);
        console.error(e);
        this.games = [];
        this.isLoading = false;
      });
    }
  }
}
