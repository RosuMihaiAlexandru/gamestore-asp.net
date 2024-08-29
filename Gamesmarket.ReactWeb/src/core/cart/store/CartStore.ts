import { makeAutoObservable, runInAction } from "mobx";
import CartService from "../services/CartService";
import { IGetOrder } from "../models/IGetOrder";
import { IGetOrders } from "../models/IGetOrders";

export default class CartStore {
  order = {} as IGetOrder;
  orders: IGetOrders[] = [];
  isLoading = false;
  errorMessage: string | null = null;
  snackMessage: string = "";
  snackSeverity: "success" | "error" = "success";
  snackOpen: boolean = false;

  constructor() {
    makeAutoObservable(this);
  }

  get totalPrice(): number {
    return this.orders.reduce((total, order) => total + order.gamePrice, 0);
  }

  showSnack(message: string, severity: "success" | "error" = "success") {
    this.snackMessage = message;
    this.snackSeverity = severity;
    this.snackOpen = true;
  }

  closeSnack() {
    this.snackOpen = false;
  }

  async getOrders() {
    this.isLoading = true;
    try {
      const response = await CartService.getDetail();
      runInAction(() => {
        this.orders = response.data;
        this.isLoading = false;
      });
    } catch (e: any) {
      runInAction(() => {
        console.log(e.response?.data?.description);
        this.snackMessage = e.response?.data.message || "Failed to get orders";
        this.snackSeverity = "error";
        this.snackOpen = true;
        this.isLoading = false;
      });
    }
  }

  async getOrder(id: number) {
    this.isLoading = true;
    try {
      const response = await CartService.getItem(id);
      runInAction(() => {
        this.order = response.data;
        this.isLoading = false;
      });
    } catch (e: any) {
      runInAction(() => {
        console.log(e.response?.data?.description);
        this.snackMessage = e.response?.data.message || "Failed to get order";
        this.snackSeverity = "error";
        this.snackOpen = true;
        this.isLoading = false;
      });
    }
  }
}
