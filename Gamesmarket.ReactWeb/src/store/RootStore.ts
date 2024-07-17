import AuthStore from "../core/auth/store/AuthStore";

export class RootStore {
  authStore: AuthStore;

  constructor() {
    this.authStore = new AuthStore();
  }
}
