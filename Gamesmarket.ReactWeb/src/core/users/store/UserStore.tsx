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

  async changeRole(email: string, newRole: string) {
    try {
      const response = await UserService.changeRole(email, newRole);
      console.log(response);
    } catch (e) {
      console.log(e.response?.data);
    }
  }
}
