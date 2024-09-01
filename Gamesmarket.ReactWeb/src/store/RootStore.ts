import AuthStore from "../core/auth/store/AuthStore";
import UserStore from "../core/users/store/UserStore";
import GameStore from "../core/game/store/GameStore";
import OrderStore from "../core/order/store/OrderStore";
import CartStore from "../core/cart/store/CartStore";
import FilterStore from "../core/filter/store/FilterStore";
import SortStore from "../core/sort/store/SortStore";

export class RootStore {
  authStore: AuthStore;
  userStore: UserStore;
  gameStore: GameStore;
  orderStore: OrderStore;
  cartStore: CartStore;
  filterStore: FilterStore;
  sortStore: SortStore;

  constructor() {
    this.authStore = new AuthStore();
    this.userStore = new UserStore();
    this.gameStore = new GameStore();
    this.orderStore = new OrderStore(this);
    this.cartStore = new CartStore();
    this.filterStore = new FilterStore(this);
    this.sortStore = new SortStore(this);
  }
}
