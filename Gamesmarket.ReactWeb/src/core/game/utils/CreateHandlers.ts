import { ChangeEvent, FormEvent } from "react";
import { IGameCreation } from "../models/IGameCreation";
import GameStore from "../store/GameStore";
import { SelectChangeEvent } from "@mui/material";

export const handleInputChange = (
  event: ChangeEvent<HTMLInputElement>,
  setFormData: React.Dispatch<React.SetStateAction<IGameCreation>>,
) => {
  const { name, value } = event.target;
  setFormData((prevData) => ({
    ...prevData,
    [name]: value,
  }));
};

export const handleSelectChange = (
  event: SelectChangeEvent<number>,
  setFormData: React.Dispatch<React.SetStateAction<IGameCreation>>,
) => {
  const { value } = event.target;
  setFormData((prevData) => ({
    ...prevData,
    gameGenre: value as number,
  }));
};

export const handleImageChange = (
  event: ChangeEvent<HTMLInputElement>,
  setFormData: React.Dispatch<React.SetStateAction<IGameCreation>>,
  setImagePreview: (image: string | null) => void,
) => {
  const file = event.target.files ? event.target.files[0] : null;
  if (file) {
    setFormData((prevData) => ({ ...prevData, imageFile: file }));
    const reader = new FileReader();
    reader.onloadend = () => {
      setImagePreview(reader.result as string);
    };
    reader.readAsDataURL(file);
  }
};

export const handleSubmit = async (
  event: FormEvent,
  formData: IGameCreation,
  gameStore: GameStore,
) => {
  event.preventDefault();
  if (!formData.imageFile) {
    gameStore.showSnack("Please upload an image.", "error");
    return;
  }

  await gameStore.createGame(
    formData.id,
    formData.name,
    formData.developer,
    formData.description,
    formData.price,
    new Date(formData.releaseDate),
    formData.gameGenre.toString(),
    formData.imageFile,
  );
};
