import { useEffect } from "react";
import { useNavigate, useParams } from "react-router";
import { useAppDispatch, useAppSelector } from "../../store/hooks";
import { fetchTeam } from "../../store/teamSlice";

export default function TeamSwitchPage() {
    const params = useParams();
    const team = useAppSelector(s => s.team);
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const projectId = params["projectId"];
    const id = params["id"];

    useEffect(() => {
        debugger;
        if (id && projectId && team.id !== id) {
            dispatch(fetchTeam({ projectId, id }));
        }
        else if (team.loaded)
        {
            navigate("/scoutroute/team");
        }
    }, [id, projectId, team.id, team.loaded]);

    return (
        <div className="h-[90vh] border-2 border-t-0 w-full border-gray-200 flex flex-col justify-center content-center items-center">
            <div>Indlæser patrulje</div>
        </div>
    );
}