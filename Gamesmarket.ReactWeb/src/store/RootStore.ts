import AuthStore from "../core/auth/store/AuthStore";
import UserStore from "../core/users/store/UserStore";

export class RootStore {
  authStore: AuthStore;
  userStore: UserStore;

  constructor() {
    this.authStore = new AuthStore();
    this.userStore = new UserStore();
  }
}
