import { makeAutoObservable, runInAction } from "mobx";
import FilterService from "../services/FilterService";
import { RootStore } from "../../../store/RootStore";

export default class FilterStore {
  rootStore: RootStore;
  isLoading = false;
  errorMessage: string | null = null;

  constructor(rootStore: RootStore) {
    makeAutoObservable(this);
    this.rootStore = rootStore;
  }

  async searchGames(searchQuery: string) {
    const { gameStore } = this.rootStore;
    this.isLoading = true;
    this.errorMessage = null;
    try {
      const response = await FilterService.searchGames(searchQuery);
      runInAction(() => {
        gameStore.games = response.data;
        this.isLoading = false;
      });
    } catch (e: any) {
      runInAction(() => {
        this.errorMessage = "Game not found";
        console.error(this.errorMessage);
        console.error(e);
        gameStore.games = [];
        this.isLoading = false;
      });
    }
  }

  async searchGamesByGenre(searchGenre: string) {
    const { gameStore } = this.rootStore;
    this.isLoading = true;
    this.errorMessage = null;
    try {
      const response = await FilterService.searchGamesByGenre(searchGenre);
      runInAction(() => {
        gameStore.games = response.data;
        this.isLoading = false;
      });
      return response.data;
    } catch (e: any) {
      runInAction(() => {
        this.errorMessage = "Games of this genre not found";
        console.error(this.errorMessage);
        console.error(e);
        gameStore.games = [];
        this.isLoading = false;
      });
      return [];
    }
  }
}
