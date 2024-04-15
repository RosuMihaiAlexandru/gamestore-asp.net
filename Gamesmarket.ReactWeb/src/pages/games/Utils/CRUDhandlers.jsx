import { useState } from "react";
import { toast } from "react-toastify";
import {
  createGame,
  deleteGame,
  editGame,
} from "../../../common/services/api/game/GameApi";

export const useGameHandlers = () => {
  const [gameData, setGameData] = useState({
    //Allows to track the status of form data
    name: "",
    developer: "",
    description: "",
    price: "",
    releaseDate: "",
    gameGenre: "",
    image: null,
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setGameData((prevData) => ({ ...prevData, [name]: value }));
  };

  const handleImageChange = (e, setPreviewImage) => {
    const imageFile = e.target.files[0];
    console.log("imageFile:", imageFile);
    setGameData((prevGameData) => ({
      ...prevGameData,
      image: imageFile,
    }));
    setPreviewImage(URL.createObjectURL(imageFile));
  };

  const handleCreate = async (e) => {
    e.preventDefault();
    try {
      const formData = new FormData();
      formData.append("name", gameData.name);
      formData.append("developer", gameData.developer);
      formData.append("description", gameData.description);
      formData.append("price", gameData.price);
      formData.append("releaseDate", gameData.releaseDate);
      formData.append("gameGenre", gameData.gameGenre);
      formData.append("imageFile", gameData.image);

      const response = await createGame(formData);

      toast.success("Game created successfully", {
        // Show successful notification
        position: "top-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
    } catch (error) {
      console.error("Error creating game:", error);
      toast.error("Error creating game. Please try again.", {
        // Show error notification
        position: "top-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
    }
  };

  const handleEditChange = (e, field) => {
    const value = e.target.type === "file" ? e.target.files[0] : e.target.value;
    setGameData((prevData) => ({ ...prevData, [field]: value }));
  };

  const handleEdit = async (id) => {
    try {
      const formData = new FormData();
      formData.append("name", gameData.name);
      formData.append("developer", gameData.developer);
      formData.append("description", gameData.description);
      formData.append("price", gameData.price);
      formData.append("releaseDate", gameData.releaseDate);
      formData.append("gameGenre", gameData.gameGenre);
      formData.append("imageFile", gameData.image);

      const response = await editGame(id, formData);

      toast.success("Game edited successfully", {
        position: "top-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
    } catch (error) {
      console.error("Error updating game:", error);
      toast.error("Error editing game. Please try again.", {
        position: "top-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
    }
  };

  const handleDelete = async (id) => {
    try {
      const response = await deleteGame(id);

      toast.success("Game deleted successfully", {
        // Show successful notification
        position: "top-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
    } catch (error) {
      console.error("Error deleting game:", error);
      toast.error("Error deleting game. Please try again.", {
        // Show error notification
        position: "top-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
    }
  };

  return {
    gameData,
    handleChange,
    handleCreate,
    handleEditChange,
    handleEdit,
    handleDelete,
    handleImageChange,
  };
};
