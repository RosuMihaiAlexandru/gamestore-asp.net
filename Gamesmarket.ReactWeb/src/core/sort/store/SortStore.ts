import { makeAutoObservable, runInAction } from "mobx";
import SortService from "../services/SortService";
import { RootStore } from "../../../store/RootStore";

export default class SortStore {
  rootStore: RootStore;
  isLoading = false;
  errorMessage: string | null = null;

  constructor(rootStore: RootStore) {
    makeAutoObservable(this);
    this.rootStore = rootStore;
  }

  async getGamesByIdDesc() {
    const { gameStore } = this.rootStore;
    this.isLoading = true;
    this.errorMessage = null;
    try {
      const response = await SortService.getGamesByIdDesc();
      runInAction(() => {
        gameStore.games = response.data;
        this.isLoading = false;
      });
    } catch (e: any) {
      runInAction(() => {
        this.errorMessage = "Games not found";
        console.error(this.errorMessage);
        console.error(e);
        gameStore.games = [];
        this.isLoading = false;
      });
    }
  }

  async getGamesByReleaseDate(ascending: boolean) {
    const { gameStore } = this.rootStore;
    this.isLoading = true;
    this.errorMessage = null;
    try {
      const response = await SortService.getGamesByReleaseDate(ascending);
      runInAction(() => {
        gameStore.games = response.data;
        this.isLoading = false;
      });
      return response.data;
    } catch (e: any) {
      runInAction(() => {
        this.errorMessage = "Games not found";
        console.error(this.errorMessage);
        console.error(e);
        gameStore.games = [];
        this.isLoading = false;
      });
      return [];
    }
  }

  async getGamesByPrice(ascending: boolean) {
    const { gameStore } = this.rootStore;
    this.isLoading = true;
    this.errorMessage = null;
    try {
      const response = await SortService.getGamesByPrice(ascending);
      runInAction(() => {
        gameStore.games = response.data;
        this.isLoading = false;
      });
      return response.data;
    } catch (e: any) {
      runInAction(() => {
        this.errorMessage = "Games not found";
        console.error(this.errorMessage);
        console.error(e);
        gameStore.games = [];
        this.isLoading = false;
      });
      return [];
    }
  }
}
