import React from "react";
import { ToastContainer } from "react-toastify";
import {
  isAdminOrModerator,
  isAuthenticated,
} from "../../../pages/accounts/Utils/AuthHandler";
import { API_URL } from "../../../common/services/constants/config";
import OrderHandler from "../../carts/Utils/OrderHandler";

const GameList = ({ games, onDelete, onEdit }) => {
  const isAllowedToEditAndDelete = isAdminOrModerator();
  const { handleCreateOrder } = OrderHandler();
  
  if (!Array.isArray(games)) {
    console.error("Invalid data format for games:", games);
    return null;
  }
  if (games.length === 0) {
    return <p>No games available</p>;
  }

  return (
    <div className="row row-cols-2">
      {games.map((game) => (
        <div key={game.id} className="col mb-4">
          <div className="card h-100">
            <img
              src={`${API_URL}${game.imagePath}`}
              className="card-img-top"
              alt={`Thumbnail for ${game.name}`}
              style={{ width: "50%", height: "50%" }}
            />
            <div className="card-body">
              <p>Name: {game.name}</p>
              <p>Developer: {game.developer}</p>
              <p>Description: {game.description}</p>
              <p>Price: {game.price}$</p>
              <p>
                ReleaseDate: {new Date(game.releaseDate).toLocaleDateString()}
              </p>
              <p>GameGenre: {game.gameGenre}</p>
            </div>
            <div className="d-flex justify-content-between mt-3 mb-3 ms-3">
              {isAllowedToEditAndDelete && (
                <>
                  <button
                    className="btn btn-primary me-2"
                    onClick={() => onEdit(game)}
                  >
                    Edit
                  </button>
                  <button
                    className="btn btn-danger"
                    onClick={() => onDelete(game.id)}
                  >
                    Delete
                  </button>
                </>
              )}
              {isAuthenticated() && (
                <>
                  <button
                    className="btn btn-success"
                    onClick={() => handleCreateOrder(game.id)}
                  >
                    Add to card
                  </button>
                </>
              )}
              <ToastContainer />
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};

export default GameList;
