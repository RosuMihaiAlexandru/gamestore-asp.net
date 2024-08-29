import { makeAutoObservable, runInAction } from "mobx";
import OrderService from "../services/OrderService";
import { RootStore } from "../../../store/RootStore";

export default class OrderStore {
  rootStore: RootStore;
  isLoading = false;
  errorMessage: string | null = null;
  snackMessage: string = "";
  snackSeverity: "success" | "error" = "success";
  snackOpen: boolean = false;

  constructor(rootStore: RootStore) {
    makeAutoObservable(this);
    this.rootStore = rootStore;
  }

  closeSnack() {
    this.snackOpen = false;
  }

  async createOrder(quantity: number, gameId: number) {
    const { user } = this.rootStore.authStore;
    this.isLoading = true;
    try {
      const response = await OrderService.createOrder(
        quantity,
        new Date(),
        user.email,
        user.name,
        gameId,
        user.username,
      );
      runInAction(() => {
        this.isLoading = false;
        this.snackMessage =
          response.data.description || "Order created successfully!";
        this.snackSeverity = "success";
        this.snackOpen = true;
      });
    } catch (e: any) {
      runInAction(() => {
        console.log(e.response?.data?.message);
        this.isLoading = false;
        this.snackMessage =
          e.response?.data.description || "Failed to make an order";
        this.snackSeverity = "error";
        this.snackOpen = true;
      });
    }
  }

  async deleteOrder(id: number) {
    this.isLoading = true;
    try {
      const response = await OrderService.deleteOrder(id);
      runInAction(() => {
        this.snackMessage =
          response.data.description || "Order deleted successfully!";
        this.snackSeverity = "success";
        this.snackOpen = true;
        this.isLoading = false;
      });
    } catch (e: any) {
      runInAction(() => {
        console.log(e.response?.data?.description);
        this.snackMessage =
          e.response?.data.message || "Failed to delete order";
        this.snackSeverity = "error";
        this.snackOpen = true;
        this.isLoading = false;
      });
    }
  }
}
