export interface IGameCreation {
  id: number;
  name: string;
  developer: string;
  description: string;
  price: string;
  releaseDate: string;
  gameGenre: number;
  imageFile: File | null;
}
