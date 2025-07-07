import { Link, useParams } from "react-router-dom";
import Section from "../components/Section/Section";
import { useAuth } from "@frontegg/react";
import { useGetRoute } from "../api/scoutroute";
import ListItem from "../components/List/ListItem";
import List from "../components/List/List";
import { faStickyNote } from "@fortawesome/free-regular-svg-icons/faStickyNote";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowAltCircleLeft } from "@fortawesome/free-regular-svg-icons";
import { QRCodeSVG } from 'qrcode.react';

export default function RoutePage() {
    const params = useParams();
    const { user } = useAuth();
    const projectId = params.id;
    const routeId = params.routeId;

    if (user === undefined) {
        return <div>Loading...</div>;
    }

    const { status, data, error } = useGetRoute(projectId!, routeId!, { axios: { headers: { "Authorization": `Bearer ${user?.accessToken}` } } });

    return (
        <>
            <h1><Link to={`/project/${projectId}`}><FontAwesomeIcon icon={faArrowAltCircleLeft} /></Link> Rute</h1>
            {status === 'pending' ? <div>Loading...</div> : status === "error" ? <div>Error! {error.message}</div> :
                <>
                    <Section title={data.data.name}>
                        <List>
                            {data.data.stops?.map(s => (
                                <ListItem key={s.id} icon={faStickyNote}>
                                    <div className="flex">
                                        <div>{s.title}</div>
                                    </div>
                                </ListItem>
                            ))}
                        </List>
                    </Section>
                    <Section title="Del">
                        <QRCodeSVG className="w-full" value={`https://whee.dk/scoutroute/project/${projectId}/takeroute/${routeId}`} />
                    </Section>
                </>
            }
        </>
    );
}