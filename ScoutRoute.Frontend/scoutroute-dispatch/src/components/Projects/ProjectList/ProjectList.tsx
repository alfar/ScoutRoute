import { useAuth } from "@frontegg/react";
import ProjectCreator from "../ProjectCreator/ProjectCreator";
import { useGetAllProjects } from "../../../api/scoutroute";
import { Link } from "react-router-dom";
import ListItem from "../../List/ListItem";
import List from "../../List/List";
import { faAddressBook } from "@fortawesome/free-regular-svg-icons/faAddressBook";

export default function ProjectList() {
    var { user } = useAuth();

    const { status, data, error, isFetching } = useGetAllProjects({axios: { headers: { "Authorization": `Bearer ${user?.accessToken}`}}, query: { queryKey: [ 'projects' ] }});

    return (
        <>
            <h1>Projects</h1>
            {status === 'pending' ? <div>Loading...</div> : status === "error" ? <div>Error! {error.message}</div> : 
            <List>
                {data.data.projects.map(project => (
                    <ListItem key={project.id} icon={faAddressBook}><Link to={`/project/${project.id}`}>{project.name}</Link></ListItem>
                ))}
            </List>}

            {isFetching ? <div>Background Updating...</div> : null}
            <ProjectCreator />
        </>
    );
}