import React from 'react';
import { useQuery } from 'react-query';
import { Link } from 'react-router-dom';
import { GetGames } from '../common/services/api/games/GamesApi';

const HomePage = () => {
  const { data: games, isLoading: isLoadingGames } = useQuery(["games"], GetGames);
  const gamesToDisplay = games?.slice(0, 3) || [];

  return (
    <div>
      <h2>Best Games</h2>
      {isLoadingGames && <p>Loading games...</p>}
      {!isLoadingGames && gamesToDisplay.length === 0 && (
      <p>No games available.</p>
      )}
      {!isLoadingGames && gamesToDisplay.length > 0 && (
        <div className="row row-cols-1 row-cols-md-3 g-4">
          {gamesToDisplay.map((game) => (
            <div key={game.id} className="col">
              <Link to={`/game/${game.id}`}>
                <div className="card h-100 align-items-center">
                  <img
                    src={`https://localhost:7202/${game.imagePath}`}
                    alt={`Thumbnail for ${game.name}`}
                    className="card-img-top" 
                    style={{ width: '75%', height: '75%' }}
                  />
                  <div className="card-body">
                    <h5 className="card-title">{game.name}</h5>
                    <p className="card-text">Price: {game.price}$</p>
                  </div>
                </div>
              </Link>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default HomePage;
