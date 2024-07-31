import AuthStore from "../core/auth/store/AuthStore";
import UserStore from "../core/users/store/UserStore";
import GameStore from "../core/game/store/GameStore";

export class RootStore {
  authStore: AuthStore;
  userStore: UserStore;
  gameStore: GameStore;

  constructor() {
    this.authStore = new AuthStore();
    this.userStore = new UserStore();
    this.gameStore = new GameStore();
  }
}
