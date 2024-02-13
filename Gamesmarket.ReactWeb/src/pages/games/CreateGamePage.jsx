import React, { useState } from "react";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import MyCreateInput from "./components/UI/input/MyCreateInput";
import MyButton from "./components/UI/button/MyButton";
import ModalSelect from "./components/UI/select/ModalSelect";
import { useGameHandlers } from "./Utils/CRUDhandlers";
import { GameGenreOptions } from "./components/GameGenre";

function CreateGamePage() {
  const { gameData, handleChange, handleCreate, handleImageChange } =
    useGameHandlers();
  const [previewImage, setPreviewImage] = useState(null);

  return (
    <form onSubmit={handleCreate}>
      <ToastContainer />
      <div className="row mt-3">
        <h2 className="text-center">Add a new game</h2>
        <div className="col-md-6">
          <div className=" mb-2">
            <MyCreateInput
              type="text"
              placeholder="Name"
              name="name"
              value={gameData.name}
              onChange={handleChange}
            />
          </div>
          <div className=" mb-2">
            <MyCreateInput
              type="text"
              placeholder="Developer"
              name="developer"
              value={gameData.developer}
              onChange={handleChange}
            />
          </div>
          <div className=" mb-2">
            <MyCreateInput
              type="text"
              placeholder="Description"
              name="description"
              value={gameData.description}
              onChange={handleChange}
            />
          </div>
          <div className=" mb-2">
            <MyCreateInput
              type="number"
              placeholder="Price"
              name="price"
              value={gameData.price}
              onChange={handleChange}
            />
          </div>
          <div className=" mb-2">
            <MyCreateInput
              type="date"
              placeholder="ReleaseDate"
              name="releaseDate"
              value={gameData.releaseDate}
              onChange={handleChange}
            />
          </div>
          <ModalSelect
            id="gameGenre"
            name="gameGenre"
            value={gameData.gameGenre}
            onChange={(e) => handleChange(e, "gameGenre")}
            options={GameGenreOptions}
          />
          <div className=" mb-2">
            <MyButton type="submit">Add Game</MyButton>
          </div>
        </div>
        <div className="col-md-6">
          <p>
            Picture must be less than 2 megabyte and have format of .jpg .webp
            .png
          </p>
          <div className="mb-2">
            <MyCreateInput
              type="file"
              onChange={(e) => handleImageChange(e, setPreviewImage)}
            />
          </div>
          {previewImage && (
            <div className="text-center mt-2">
              <img
                src={previewImage}
                alt="Selected"
                style={{ maxWidth: "100%", maxHeight: "300px" }}
              />
            </div>
          )}
        </div>
      </div>
    </form>
  );
}

export default CreateGamePage;
