import AuthStore from "../core/auth/store/AuthStore";
import UserStore from "../core/users/store/UserStore";
import GameStore from "../core/game/store/GameStore";
import OrderStore from "../core/order/store/OrderStore";
import CartStore from "../core/cart/store/CartStore";

export class RootStore {
  authStore: AuthStore;
  userStore: UserStore;
  gameStore: GameStore;
  orderStore: OrderStore;
  cartStore: CartStore;

  constructor() {
    this.authStore = new AuthStore();
    this.userStore = new UserStore();
    this.gameStore = new GameStore();
    this.orderStore = new OrderStore(this);
    this.cartStore = new CartStore();
  }
}
