import { useAuth } from "@frontegg/react";
import { useDeleteStop, useGetUnassignedStops } from "../../../api/scoutroute";
import { faStickyNote } from "@fortawesome/free-regular-svg-icons/faStickyNote";
import ListItem from "../../List/ListItem";
import List from "../../List/List";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashCan } from "@fortawesome/free-regular-svg-icons/faTrashCan";
import Section from "../../Section/Section";

interface UnassignedStopListProps {
    projectId: string;
    onToggleStop?: (stopId: string, isChecked: boolean) => void;
}

export default function UnassignedStopList(props: UnassignedStopListProps) {
    var { user } = useAuth();

    var getStops = useGetUnassignedStops(props.projectId, { axios: { headers: { "Authorization": `Bearer ${user?.accessToken}` } }, query: { queryKey: [`unassigned-stops/${props.projectId}`] } });
    var { mutateAsync: deleteStop } = useDeleteStop({ axios: { headers: { "Authorization": `Bearer ${user?.accessToken}` } }, mutation: { mutationKey: [`unassigned-stops/${props.projectId}`] } });

    const handleDeleteStop = async (stopId: string) => {
        try {
            await deleteStop({ projectId: props.projectId, stopId });
            getStops.refetch();
        } catch (error) {
            console.error('Failed to delete stop:', error);
        }
    };

    const toggleSelectedAction = (stopId: string, isChecked: boolean) => {
        if (props.onToggleStop) {
            props.onToggleStop(stopId, isChecked);
        }
    };

    return (
        <>
            {getStops.status === 'pending' ? <div>Loading...</div> : getStops.status === "error" ? <div>Error! {getStops.error.message}</div> :
                getStops.data.data.length > 0 ? (
                    <Section title="Ikke planlagte stop">
                        <List>
                            {getStops.data?.data.map(stop => (
                                <ListItem icon={faStickyNote}>
                                    <div className="flex">
                                        {props.onToggleStop && <input type="checkbox" className="mr-2" onChange={e => toggleSelectedAction(stop.id, e.target.checked)} />}
                                        <div className={stop.routeId === null ? "text-red-500" : ""}>{stop.title}</div>
                                        <button onClick={() => handleDeleteStop(stop.id)}><FontAwesomeIcon icon={faTrashCan} /></button>
                                    </div>
                                </ListItem>
                            ))}
                        </List>
                    </Section>
                ) : null
            }
        </>
    );
}