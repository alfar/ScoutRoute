import { useAppSelector } from '../../store/hooks';
import RouteView from '../../components/RouteView/RouteView';

export default function RoutesPage() {
  const routes = useAppSelector(s => s.routes.routes);

  return (
    <div className="w-full p-2 border-2 border-t-0 border-gray-200">
        {routes.map(r => (
          <RouteView route={r} key={r.id} />
        ))}
    </div>
  )
}