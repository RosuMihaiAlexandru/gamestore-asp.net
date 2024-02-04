import React from 'react';
import { useParams } from 'react-router-dom';
import { useQuery } from 'react-query';
import { getGame } from '../../common/services/api/game/GameApi';

const GameDetailsPage = () => {
  const { id } = useParams();
  const { data: game, isLoading } = useQuery(['game', id], () => getGame(id));

  return (
    <div className='row mt-3'>
      <div className='col-md-3'>
      {isLoading && <p>Loading game image...</p>}
        {!isLoading && game && (
      <img src={`https://localhost:7202/${game.imagePath}`} className="card-img-top" alt={`Thumbnail for ${game.name}`} />
      )}
      </div>
      <div className='col-md-6'>
        {isLoading && <p>Loading game details...</p>}
        {!isLoading && game && (
          <div>
            <h2>{game.name}</h2>
            <p>Developer: {game.developer}</p>
            <p>Description: {game.description}</p>
            <p>ReleaseDate: {new Date(game.releaseDate).toLocaleDateString()}</p>
            <p>GameGenre: {game.gameGenre}</p>
          </div>
        )}
      </div>
      <div className='col-md-3 d-flex justify-content-center align-items-center'>
      {isLoading && <p>Loading game price...</p>}
        {!isLoading && game && (
          <div>
            <p>Price: {game.price}$</p>
            <button class="btn btn-success">Add to card</button>
            </div>
          )}
      </div>
    </div>
  );
};

export default GameDetailsPage;
