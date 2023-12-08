import React from 'react';
import { useParams } from 'react-router-dom';
import { useQuery } from 'react-query';
import { GetGame } from '../../common/services/api/games/GamesApi';

const GameDetailsPage = () => {
  const { id } = useParams();
  const { data: game, isLoading } = useQuery(['game', id], () => GetGame(id));

  return (
    <div className='row mt-3'>
      <div className='col-md-3'>
      </div>
      <div className='col-md-6'>
        {isLoading && <p>Loading game details...</p>}
        {!isLoading && game && (
          <div>
            <h2>{game.name}</h2>
            <p>Developer: {game.developer}</p>
            <p>Description: {game.description}</p>
            <p>Price: {game.price} грн.</p>
            <p>ReleaseDate: {new Date(game.releaseDate).toLocaleDateString()}</p>
            <p>GameGenre: {game.gameGenre}</p>
          </div>
        )}
      </div>
      <div className='col-md-3'>
      </div>
    </div>
  );
};

export default GameDetailsPage;
