import React from 'react';
import MyCreateInput from './components/UI/input/MyCreateInput';
import MyButton from './components/UI/button/MyButton';
import { useGameHandlers } from './Utils/CRUDhandlers';

function CreateGamePage() {
  const { gameData, handleChange, handleCreate } = useGameHandlers();

  return (
    <form onSubmit={handleCreate}>
      <div className='row mt-3'>
        <h2 className="text-center">Add a new game</h2>
        <div className='row align-items-start'>
          <div className=" mb-2">
            <MyCreateInput type="text" placeholder="Name" name="name" value={gameData.name} onChange={handleChange} />
          </div>          
          <div className=" mb-2">
          <MyCreateInput type="text" placeholder="Developer" name="developer" value={gameData.developer} onChange={handleChange} />
          </div>          
          <div className=" mb-2">
          <MyCreateInput type="text" placeholder="Description" name="description" value={gameData.description} onChange={handleChange} />
          </div>          
          <div className=" mb-2">
          <MyCreateInput type="number" placeholder="Price" name="price" value={gameData.price} onChange={handleChange} />
          </div>          
          <div className=" mb-2">
          <MyCreateInput type="date" placeholder="ReleaseDate" name="releaseDate" value={gameData.releaseDate} onChange={handleChange} />
          </div>
          <div className=" mb-2">
          <MyCreateInput type="number" placeholder="GameGenre" name="gameGenre" value={gameData.gameGenre} onChange={handleChange} min="0" max="9" />
          </div>
          <div className=" mb-2">
          <MyButton type="submit">Add Game</MyButton>
          </div>
        </div>
      </div>
    </form>
  );
}

export default CreateGamePage;