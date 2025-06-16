import { useEffect } from "react";
import { useNavigate, useParams } from "react-router";
import { useAppDispatch, useAppSelector } from "../../store/hooks";
import { assignRouteTeam } from "../../store/routeSlice";

export default function RouteAddPage() {
    const params = useParams();
    const routeId = params["id"];

    const team = useAppSelector(s => s.team);

    const routeLoaded = useAppSelector(s => s.routes.routes.find(r => r.id == routeId)?.loaded || false);
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const projectId = team.projectId;

    useEffect(() => {
        if (routeId && projectId && !routeLoaded) {
            dispatch(assignRouteTeam({ projectId, teamId: team.id, routeId }));
        }
        else if (team.loaded)
        {
            navigate("/scoutroute/team");
        }
    }, [routeId, projectId, team.id, routeLoaded]);

    return (
        <div className="h-[90vh] border-2 border-t-0 w-full border-gray-200 flex flex-col justify-center content-center items-center">
            <div>Indlæser rute</div>
        </div>
    );
}