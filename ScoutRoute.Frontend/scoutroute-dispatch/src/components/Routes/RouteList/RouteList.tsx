import { useAuth } from "@frontegg/react";
import { useGetAllRoutes } from "../../../api/scoutroute";
import { Link } from "react-router-dom";
import ListItem from "../../List/ListItem";
import List from "../../List/List";
import { faRectangleList } from "@fortawesome/free-regular-svg-icons";
import Section from "../../Section/Section";

interface RouteListProps {
    projectId: string;
}

export default function RouteList(props: RouteListProps) {
    var { user } = useAuth();
    if (user === undefined) {
        return <div>Loading... RouteList</div>;
    }   

    const { data, error, isFetching, isPending, isError } = useGetAllRoutes(props.projectId, { axios: { headers: { "Authorization": `Bearer ${user?.accessToken}` } }, query: { queryKey: [`routes/${props.projectId}`] } });

    return (
        <Section title="Ruter">
            {isPending ? <div>Loading... (pend)</div> : isError ? <div>Error! {error.message}</div> :
                <List>
                    {data?.data?.map(route => (
                        <ListItem key={route.id} icon={faRectangleList}>
                            <div className="flex">
                                <Link to={`/project/${props.projectId}/route/${route.id}`}>{route.name}</Link>
                            </div>
                        </ListItem>
                    ))}
                </List >}

            {isFetching ? <div>Background Updating...</div> : null}
        </Section>
    );
}