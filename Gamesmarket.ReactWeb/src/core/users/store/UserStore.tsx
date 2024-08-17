import { IUsers } from "../models/IUsers";
import { makeAutoObservable, runInAction } from "mobx";
import UserService from "../services/UserService";

export default class UserStore {
  users: IUsers[] = [];
  isLoading = false;
  pageSize = 5;
  snackMessage: string = "";
  snackSeverity: "success" | "error" = "success";
  snackOpen: boolean = false;

  constructor() {
    makeAutoObservable(this);
  }

  setPageSize(pageSize: number) {
    this.pageSize = pageSize;
  }

  closeSnack() {
    this.snackOpen = false;
  }

  async getUsers() {
    this.isLoading = true;
    try {
      const response = await UserService.fetchUsers();
      runInAction(() => {
        this.users = response.data;
        this.isLoading = false;
      });
    } catch (e: any) {
      runInAction(() => {
        console.error("Failed to fetch users", e);
        this.isLoading = false;
        this.snackMessage = e.response?.data || "Failed to fetch users";
        this.snackSeverity = "error";
        this.snackOpen = true;
      });
    }
  }

  async changeRole(email: string, newRole: string) {
    try {
      const response = await UserService.changeRole(email, newRole);
      console.log(response);
      runInAction(() => {
        this.snackMessage = response.data;
        this.snackSeverity = "success";
        this.snackOpen = true;
        this.getUsers(); // Refresh the users list after changing the role
      });
    } catch (e: any) {
      runInAction(() => {
        this.snackMessage = e.response?.data || "An error occurred";
        this.snackSeverity = "error";
        this.snackOpen = true;
      });
    }
  }
}
