import { useTranslation } from "react-i18next";
import StopList from "../StopList/StopList";
import { useState } from "react";
import { faCompass, faListAlt } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import RouteMap from "../RouteMap/RouteMap";

interface RouteViewProps {
    route: Route;
}

export default function RouteView(props: RouteViewProps) {
    const { t } = useTranslation();
    const [comment, setComment] = useState("");
    const [showMap, setShowMap] = useState(false);

    return (
        <div className="w-full flex flex-col p-2 border-2 border-gray-200 rounded-lg">
            <div className="flex justify-between mb-5">
                <h1 className="text-3xl">{props.route.name}</h1>
                <button className="rounded-lg bg-blue-600 w-15 text-white p-2" onClick={() => setShowMap(!showMap)}>
                    <FontAwesomeIcon className="text-3xl" icon={showMap ? faListAlt : faCompass} />
                </button>
            </div>
            {showMap ? <RouteMap route={props.route} /> : <StopList stops={props.route.stops} />}
            <div className="w-full">
                <label>{t("comment")}</label>
                <textarea className="w-full h-32 border-2 border-gray-300 p-1" value={comment} onChange={(e) => setComment(e.currentTarget.value)} />
            </div>
        </div>
    );
}