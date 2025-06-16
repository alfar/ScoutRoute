import { useAuth } from "@frontegg/react";
import { useGetAllTeams } from "../../api/scoutroute";
import { Link } from "react-router-dom";
import ListItem from "../List/ListItem";
import List from "../List/List";
import { faRectangleList } from "@fortawesome/free-regular-svg-icons";
import Section from "../Section/Section";

interface TeamListProps {
    projectId: string;
}

export default function TeamList(props: TeamListProps) {
    var { user } = useAuth();
    if (user === undefined) {
        return <div>Loading... TeamList</div>;
    }

    const { data, error, isFetching, isPending, isError } = useGetAllTeams(props.projectId, { axios: { headers: { "Authorization": `Bearer ${user?.accessToken}` } }, query: { queryKey: [`teams/${props.projectId}`] } });

    return (
        <Section title="Patruljer">
            {isPending ? <div>Loading... (pend)</div> : isError ? <div>Error! {error.message}</div> :
                <List>
                    {data?.data?.map(team => (
                        <ListItem key={team.id} icon={faRectangleList}>
                            <div className="flex">
                                <Link to={`/project/${props.projectId}/team/${team.id}`}>{team.name}</Link>
                            </div>
                        </ListItem>
                    ))}
                </List>
            }
            {isFetching ? <div>Background Updating...</div> : null}
        </Section>
    );
}