export interface IOrder {
  id: number;
  quantity: number;
  dateCreated: Date;
  email: string;
  name: string;
  gameId: number;
  gameGenre: string;
  login: string;
}
