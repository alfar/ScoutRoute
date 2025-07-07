import { Link, useParams } from "react-router-dom";
import Section from "../components/Section/Section";
import { useAuth } from "@frontegg/react";
import { useGetTeam } from "../api/scoutroute";
import ListItem from "../components/List/ListItem";
import List from "../components/List/List";
import { faStickyNote } from "@fortawesome/free-regular-svg-icons/faStickyNote";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowAltCircleLeft } from "@fortawesome/free-regular-svg-icons";
import { QRCodeSVG } from 'qrcode.react';

export default function TeamPage() {
    const params = useParams();
    const { user } = useAuth();
    const projectId = params.id;
    const teamId = params.teamId;

    if (user === undefined) {
        return <div>Loading...</div>;
    }

    const { status, data, error } = useGetTeam(projectId!, teamId!, { axios: { headers: { "Authorization": `Bearer ${user?.accessToken}` } } });

    return (
        <>
            <h1><Link to={`/project/${projectId}`}><FontAwesomeIcon icon={faArrowAltCircleLeft} /></Link> Patrulje</h1>
            {status === 'pending' ? <div>Loading...</div> : status === "error" ? <div>Error! {error.message}</div> :
                <>
                    <Section title={data.data.name || "Intet navn"}>
                        <dl>
                            <dt>Team lead</dt>
                            <dd>{data.data.teamLead}</dd>
                            <dt>Phone</dt>
                            <dd>{data.data.phone}</dd>
                            <dt>Trailer type</dt>
                            <dd>{data.data.trailerType}</dd>
                        </dl>
                    </Section>
                    <Section title={"Members"}>
                        <List>
                            {data.data.members?.map((s, i) => (
                                <ListItem key={i} icon={faStickyNote}>
                                    <div className="flex">
                                        <div>{s}</div>
                                    </div>
                                </ListItem>
                            ))}
                        </List>
                    </Section>
                    <Section title="Del">
                        <a href={`https://localhost:5174/scoutroute/team/switch/${projectId}/${teamId}`} className="w-full">
                            <QRCodeSVG className="w-full" value={`https://localhost:5174/scoutroute/team/switch/${projectId}/${teamId}`} />
                        </a>
                    </Section>
                </>
            }
        </>
    );
}