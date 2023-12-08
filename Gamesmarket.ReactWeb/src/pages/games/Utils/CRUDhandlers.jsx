import { useState } from 'react';
import { CreateGame, DeleteGame, EditGame} from '../../../common/services/api/games/GamesApi';

export const useGameHandlers = () => {
  const [gameData, setGameData] = useState({//Allows to track the status of form data
    name: '',
    developer: '',
    description: '',
    price: '',
    releaseDate: '',
    gameGenre: '',
  });
  
  //Get the event (e) and updates the corresponding property in gameData
  const handleChange = (e) => {
    const { name, value } = e.target;
    setGameData((prevData) => ({ ...prevData, [name]: value }));
  };

  const handleCreate = async (e) => {
    e.preventDefault();
    try {
      const response = await CreateGame(gameData);
      console.log('Game created successfully:', response);
      setShowSuccessModal(true); 
    } catch (error) {
      console.error('Error creating game:', error);
    }
  };

  const handleEditChange = (e, field) => {
    const { value } = e.target;
    console.log(`Setting ${field} to:`, value);
    setGameData((prevData) => ({
      ...prevData,
      [field]: value,
    }));
  };

  const handleEdit = async (id) => {
    try {
      const response = await EditGame(id, gameData);
      console.log('Game updated successfully:', response);
    } catch (error) {
      console.error('Error updating game:', error);
    }
  };

  const handleDelete = async (id) => {
    try {
      const response = await DeleteGame(id);
      console.log('Game deleted successfully:', response);
    } catch (error) {
      console.error('Error deleting game:', error);
    }
  };

  return { gameData, handleChange, handleCreate, handleEditChange, handleEdit, handleDelete};
};
