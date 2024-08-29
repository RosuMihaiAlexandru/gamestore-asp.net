import $api from "../../../http";
import { AxiosResponse } from "axios";
import { IGetOrders } from "../models/IGetOrders";
import { IGetOrder } from "../models/IGetOrder";

export default class OrderService {
  static getDetail(): Promise<AxiosResponse<IGetOrders[]>> {
    return $api.get<IGetOrders[]>("/cart/getDetail");
  }

  static getItem(id: number): Promise<AxiosResponse<IGetOrder>> {
    return $api.get<IGetOrder>(`/cart/getItem`, {
      params: { id },
    });
  }
}
