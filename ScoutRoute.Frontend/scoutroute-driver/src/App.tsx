import './App.css';
import { useAppSelector } from './store/hooks';
import RouteView from './components/RouteView/RouteView';

function App() {
  const routes = useAppSelector(s => s.routes.routes);

  return (
    <>
        {routes.map(r => (
          <RouteView route={r} key={r.id} />
        ))}
    </>
  )
}

export default App
