import React, { useState, useMemo } from "react";
import { useQuery } from "react-query";
import { ToastContainer } from "react-toastify";
import { getGames } from "../../common/services/api/game/GameApi";
import OrderHandler from "../carts/Utils/OrderHandler";
import GameList from "./components/GamesList";
import { useGameHandlers } from "./Utils/CRUDhandlers";
import ModalEdit from "./components/UI/ModalEdit";
import { isAdminOrModerator } from "../../pages/accounts/Utils/AuthHandler";
import Pagination from "../../common/pagination/Pagination";
import { getPageCount } from "../../common/utils/pages";

function GamesPage() {
  const { data, isLoading } = useQuery(["games"], getGames);
  const { handleDelete, handleEdit, handleEditChange } = useGameHandlers();
  const [modal, setModal] = useState(false);
  const [selectedGame, setSelectedGame] = useState(null);
  const isAllowedToEditAndDelete = isAdminOrModerator();
  const { onCreateOrder } = OrderHandler();
  const [currentPage, setCurrentPage] = useState(1);

  const gamesPerPage = 6;
  const totalPages = useMemo(() => getPageCount(data?.length || 0, gamesPerPage), [data, gamesPerPage]);
  const currentGames = useMemo(() => {
    const indexOfLastGame = currentPage * gamesPerPage;
    const indexOfFirstGame = indexOfLastGame - gamesPerPage;
    return data ? data.slice(indexOfFirstGame, indexOfLastGame) : [];
  }, [data, currentPage, gamesPerPage]);

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

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
                  games={currentGames}
                  onDelete={handleDelete}
                  onEdit={openEditModal}
                  onCreateOrder={onCreateOrder}
                  isAllowedToEditAndDelete={isAllowedToEditAndDelete}
                />
                <Pagination
                  totalPages={totalPages}
                  currentPage={currentPage}
                  onPageChange={handlePageChange}
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
      <ToastContainer />
    </>
  );
}

export default GamesPage;
