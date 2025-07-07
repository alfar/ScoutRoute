import './App.css';
import RoutesPage from './pages/RoutesPage/RoutesPage';
import TeamPage from './pages/TeamPage/TeamPage';
import { useTranslation } from 'react-i18next';
import { Navigate, NavLink, Route, Routes } from 'react-router';
import TeamSwitchPage from './pages/TeamSwitchPage/TeamSwitchPage';
import RouteAddPage from './pages/RouteAddPage/RouteAddPage';

function App() {
  const { t } = useTranslation();

  const activeClass = "w-1/2 border-2 border-b-0 border-gray-200 rounded-tl-lg rounded-tr-lg p-2 text-blue-600 text-xl font-bold text-center"
  const inactiveClass = "w-1/2 border-2 border-b-0 border-gray-200 bg-gray-200 rounded-tl-lg rounded-tr-lg p-2 text-blue-600 text-xl text-center";

  return (
    <>
      <nav className="w-full flex">
        <NavLink to="/scoutroute/routes" className={({ isActive }) => isActive ? activeClass : inactiveClass}>{t("routeTab")}</NavLink>
        <NavLink to="/scoutroute/team" className={({ isActive }) => isActive ? activeClass : inactiveClass}>{t("teamTab")}</NavLink>
      </nav>
      <Routes>
        <Route path="scoutroute">
          <Route path="routes" element={<RoutesPage />} />
          <Route path="routes/add/:id" element={<RouteAddPage />} />
          <Route path="team/switch/:projectId/:id" element={<TeamSwitchPage />} />
          <Route path="team" element={<TeamPage />} />
          <Route index element={<Navigate to="/scoutroute/routes" />} />
        </Route>
      </Routes>
    </>
  )
}

export default App
