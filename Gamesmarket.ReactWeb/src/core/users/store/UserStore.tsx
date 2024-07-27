import { IUsers } from "../models/IUsers";
import { makeAutoObservable, runInAction } from "mobx";
import UserService from "../services/UserService";

export default class UserStore {
  users: IUsers[] = [];
  isLoading = false;
  pageSize = 5;

  constructor() {
    makeAutoObservable(this);
  }

  setPageSize(pageSize: number) {
    this.pageSize = pageSize;
  }

  getBoxHeight() {
    if (this.pageSize === 10) return 635;
    if (this.pageSize === 25) return 1450;
    return 375; // default height for other page sizes
  }

  async getUsers() {
    this.isLoading = true;
    try {
      const response = await UserService.fetchUsers();
      runInAction(() => {
        this.users = response.data;
        this.isLoading = false;
      });
    } catch (error) {
      runInAction(() => {
        console.error("Failed to fetch users", error);
        this.isLoading = false;
      });
    }
  }
}
