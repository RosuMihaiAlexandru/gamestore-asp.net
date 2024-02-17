import React, { useState } from "react";
import { useQuery } from "react-query";
import { getGames } from "../../common/services/api/game/GameApi";
import GameList from "./components/GamesList";
import { useGameHandlers } from "./Utils/CRUDhandlers";
import ModalEdit from "./components/UI/ModalEdit";
import { isAdminOrModerator } from "../../pages/accounts/Utils/AuthHandler";

function GamesPage() {
  const { data, isLoading } = useQuery(["games"], getGames);
  const { handleDelete, handleEdit, handleEditChange } = useGameHandlers();
  const [modal, setModal] = useState(false);
  const [selectedGame, setSelectedGame] = useState(null);
  const isAllowedToEditAndDelete = isAdminOrModerator();

  const openEditModal = (game) => {
    setSelectedGame(game);
    setModal(true);
  };

  return (
    <>
      <div className="row mt-3">
        <div className="col">
          <h1>Avaliable Games</h1>
        </div>
      </div>
      <div className="row">
        <div className="col">
          {isLoading && <p>Data is loading...</p>}
          {!isLoading && !data && <p>No games available.</p>}
          {!isLoading && data && data.length > 0 && (
            <div className="card rounded-4 shadow-sm">
              <div className="card-body">
                <GameList
                  games={data}
                  onDelete={handleDelete}
                  onEdit={openEditModal}
                  isAllowedToEditAndDelete={isAllowedToEditAndDelete}
                />
                <ModalEdit
                  visible={modal}
                  setVisible={setModal}
                  selectedGame={selectedGame}
                  handleEdit={handleEdit}
                  handleEditChange={handleEditChange}
                />
              </div>
            </div>
          )}
        </div>
      </div>
    </>
  );
}

export default GamesPage;
