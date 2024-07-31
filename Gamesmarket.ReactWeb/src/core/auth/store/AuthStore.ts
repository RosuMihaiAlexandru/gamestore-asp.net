import { makeAutoObservable } from "mobx";
import { IUser } from "../models/IUser";
import AuthService from "../services/AuthService";

export default class AuthStore {
  user = {} as IUser;
  isAuth = false;
  isAdmin = false;
  isModerator = false;

  constructor() {
    makeAutoObservable(this);
  }

  setAuth(bool: boolean) {
    this.isAuth = bool;
  }

  setAdmin(bool: boolean) {
    this.isAdmin = bool;
  }

  setModerator(bool: boolean) {
    this.isModerator = bool;
  }

  setUser(user: IUser) {
    this.user = user;
    this.checkUserRole(user.role);
  }

  checkUserRole(role: string) {
    this.setAdmin(role === "Administrator");
    this.setModerator(role === "Moderator");
  }

  async login(email: string, password: string) {
    try {
      const response = await AuthService.login(email, password);
      console.log(response);
      localStorage.setItem("token", response.data.token);
      this.setAuth(true);
      this.setUser(response.data);
    } catch (e) {
      console.log(e.response?.data?.message);
    }
  }

  async register(
    email: string,
    birthDate: Date | null,
    password: string,
    passwordConfirm: string,
    name: string,
  ) {
    try {
      const response = await AuthService.register(
        email,
        birthDate,
        password,
        passwordConfirm,
        name,
      );
      console.log(response);
      localStorage.setItem("token", response.data.token);
      this.setAuth(true);
      this.setUser(response.data);
    } catch (e) {
      console.log(e.response?.data?.message);
    }
  }

  async logout() {
    try {
      localStorage.removeItem("token");
      this.setAuth(false);
      this.setAdmin(false);
      this.setModerator(false);
      this.setUser({} as IUser);
    } catch (e) {
      console.log(e.response?.data?.message);
    }
  }
}
