import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.min.js';

import './App.css'
import { QueryClient, QueryClientProvider } from 'react-query';
import { Route, Routes } from 'react-router-dom';
import { MainLayout } from './common/Layout/MainLayout';
import GamesPage from './pages/games/GamesPage';
import CreateGame from './pages/games/CreateGamePage';
import HomePage from './pages/HomePage';
import GameDetailsPage from './pages/games/GameDetailsPage';
import SearchResultsPage from './pages/games/SearchResultsPage';

const queryClient = new QueryClient()
function App() {
  return (
    <QueryClientProvider client={queryClient}>
        <Routes>
          <Route path="/" element={<MainLayout/>}>
            <Route index element={<HomePage/>}/>
            <Route path="games" element={<GamesPage/>}/>
            <Route path="creategame" element={<CreateGame/>}/>
            <Route path="game/:id" element={<GameDetailsPage />} />
            <Route path="search-results" element={<SearchResultsPage />} />
          </Route>
        </Routes>
    </QueryClientProvider>
  )
}

export default App