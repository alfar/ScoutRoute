import { Route, Routes } from 'react-router-dom';
import './App.css'
import { useAuth, useLoginWithRedirectV2 } from '@frontegg/react'
import ProjectList from './components/Projects/ProjectList/ProjectList';
import Dashboard from './components/Dashboard/Dashboard';
import { useEffect } from 'react';
import RoutePage from './pages/RoutePage';
import TeamPage from './pages/TeamPage';

function App() {
  const { user, isAuthenticated } = useAuth();
  const loginWithRedirect = useLoginWithRedirectV2();

  useEffect(() => {
    console.log("Logging in again");
    if (!isAuthenticated) {
      loginWithRedirect({ shouldRedirectToLogin: false });
    }
  }, [isAuthenticated, loginWithRedirect]);

  return isAuthenticated ?
    (
      <>
        <header>
          <nav className="flex items-center gap-2">
            <div>{user?.name}</div>
          </nav>
        </header>
        <main>
          <Routes>
            <Route path='/' Component={ProjectList} />
            <Route path='/Project/:id' Component={Dashboard} />
            <Route path='/Project/:id/Route/:routeId' Component={RoutePage} />
            <Route path='/Project/:id/Team/:teamId' Component={TeamPage} />
          </Routes>
        </main>
      </>
    ) :
    (
      <main>Logger ind</main>
    );
}

export default App
