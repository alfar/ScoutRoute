import { useParams } from "react-router-dom";
import UnassignedStopList from "../Stops/UnassignedStopList/UnassignedStopList";
import StopCreator from "../Stops/StopCreator/StopCreator";
import { useState } from "react";
import RouteCreator from "../Routes/RouteCreator/RouteCreator";
import RouteList from "../Routes/RouteList/RouteList";
import TeamList from "../Teams/TeamList";
import TeamCreator from "../Teams/TeamCreator";

export default function Dashboard() {
    var params = useParams();
    var [selectedStops, setSelectedStops] = useState<string[]>([]);

    const toggleStopAction = (stopId: string, isChecked: boolean) => {
        if (isChecked) {
            setSelectedStops((prev: string[]) => [...prev, stopId]);
        } else {
            setSelectedStops((prev: string[]) => prev.filter((id) => id !== stopId));
        }
    };

    return (
        <div className="grid grid-cols-3 gap-5">
            <div>
                <UnassignedStopList projectId={params.id!} onToggleStop={toggleStopAction} />
                <StopCreator projectId={params.id!} />
            </div>
            <div>
                <RouteList projectId={params.id!} />
                <RouteCreator projectId={params.id!} selectedStops={selectedStops} />
            </div>
            <div>
                <TeamList projectId={params.id!} />
                <TeamCreator projectId={params.id!} />
            </div>
        </div>
    );
}