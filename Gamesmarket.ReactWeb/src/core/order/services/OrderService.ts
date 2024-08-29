import $api from "../../../http";
import { AxiosResponse } from "axios";
import { OrderResponse } from "../models/response/OrderResponse";
import { DeleteOrderResponse } from "../models/response/DeleteOrderResponse";

export default class OrderService {
  static createOrder(
    quantity: number,
    dateCreated: Date | null,
    email: string,
    name: string,
    gameId: number,
    login: string,
  ): Promise<AxiosResponse<OrderResponse>> {
    return $api.post<OrderResponse>("/order/createOrder", {
      quantity,
      dateCreated: dateCreated ? dateCreated.toISOString() : null,
      email,
      name,
      gameId,
      login,
    });
  }

  static deleteOrder(id: number): Promise<AxiosResponse<DeleteOrderResponse>> {
    return $api.delete<DeleteOrderResponse>(`/order/delete/${id}`);
  }
}
