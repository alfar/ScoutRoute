import { useTranslation } from "react-i18next";
import StopList from "../StopList/StopList";
import { useEffect, useState } from "react";
import { faCircleCheck, faCompass, faListAlt, faPaperPlane, faRotateLeft, faTriangleExclamation } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import RouteMap from "../RouteMap/RouteMap";
import { useAppDispatch, useAppSelector } from "../../store/hooks";
import { addComment, addExtraStop, completeRoute, fetchRoute, overfilledRoute, removeExtraStop, resetRouteStatus } from "../../store/routeSlice";

interface RouteViewProps {
    route: Route;
}

export default function RouteView(props: RouteViewProps) {
    const { t } = useTranslation();
    const [comment, setComment] = useState("");
    const [showMap, setShowMap] = useState(false);
    const selectProject = useAppSelector(s => s.team.projectId);

    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!props.route.loaded) {
            dispatch(fetchRoute({ projectId: selectProject, routeId: props.route.id }));
        }
    }, [props.route.loaded, props.route.id, selectProject]);

    const addCommentAction = () => {
        dispatch(addComment({ id: props.route.id, comment }));
        setComment("");
    };

    const addExtraStopAction = () => {
        dispatch(addExtraStop({ id: props.route.id }));
    };

    const removeExtraStopAction = () => {
        dispatch(removeExtraStop({ id: props.route.id }));
    };

    const completeRouteAction = () => {
        dispatch(completeRoute({ id: props.route.id }));
    };

    const overfilledRouteAction = () => {
        dispatch(overfilledRoute({ id: props.route.id }));
    };

    const resetRouteStatusAction = () => {
        dispatch(resetRouteStatus({ id: props.route.id }));
    };

    const counters = props.route.stops?.reduce((prev, curr) => {
        return [prev[0] + (curr.status !== 0 ? curr.quantity : 0), prev[1] + (curr.status === 2 ? curr.quantity : 0), prev[2] + curr.quantity];
    }, [props.route.extraStops, 0, props.route.extraStops]);

    return (
        <div className="w-full flex flex-col gap-3">
            <div className="grid grid-cols-[1fr_60px]">
                <h1 className="text-3xl">{props.route.name}</h1>
                {props.route.status === 0 ? (
                    <button className="row-span-2 rounded-lg border-2 border-blue-600 w-15 text-blue-600 p-2" onClick={() => setShowMap(!showMap)}>
                        <FontAwesomeIcon className="text-3xl" icon={showMap ? faListAlt : faCompass} />
                    </button>
                ) : (
                    <>
                        <button className="row-span-2 rounded-lg border-2 border-blue-600 w-15 text-blue-600 p-2" onClick={resetRouteStatusAction}>
                            <FontAwesomeIcon className="text-3xl" icon={faRotateLeft} />
                        </button>
                    </>
                )}
                {props.route.status === 0 && counters && <div className="text-md">{counters[0]} / {counters[2]}{counters[1] > 0 && <> {t("completedMissingMessage", { count: counters[1] })}</>}</div>}
                {props.route.status === 1 && counters && <div className="text-md text-green-500"><FontAwesomeIcon icon={faCircleCheck} /> {t("routeCompleted")}{counters[1] > 0 && <> {t("completedMissingMessage", { count: counters[1] })}</>}</div>}
                {props.route.status === 2 && counters &&<div className="text-md text-amber-500"><FontAwesomeIcon icon={faTriangleExclamation} /> {t("routeOverfilled")} ({counters[0]} / {counters[2]})</div>}
            </div>
            {props.route.status === 0 && (
                <>
                    {props.route.stops && (showMap ? <RouteMap route={props.route} /> : <StopList stops={props.route.stops} />)}
                    <div className="w-full flex items-center">
                        <button className="bg-blue-600 text-white rounded-lg text-3xl p-2 w-15" onClick={removeExtraStopAction}>-</button>
                        <h2 className="text-2xl grow text-center">{t('extraStops', { count: props.route.extraStops })}</h2>
                        <button className="bg-blue-600 text-white rounded-lg text-3xl p-2 w-15" onClick={addExtraStopAction}>+</button>
                    </div>
                    <div className="w-full border-2 p-2 border-gray-200 rounded-lg">
                        <h2 className="text-xl">{t("comments")}</h2>
                        <ul>
                            {props.route.comments?.map((c, i) => (
                                <li key={i} className="p-2 rounded-lg rounded-bl-none mb-2 bg-gray-200">{c}</li>
                            ))}
                        </ul>
                        <div className="flex gap-2">
                            <textarea className="w-full h-32 flex-grow border-2 border-gray-200 rounded-lg p-1" value={comment} onChange={(e) => {e.target.scrollIntoView(); setComment(e.currentTarget.value); }} />
                            <button type="submit" className="rounded-lg bg-blue-600 disabled:bg-gray-200 text-white h-12 w-12 p-2 self-end" disabled={comment.length === 0} onClick={addCommentAction}><FontAwesomeIcon icon={faPaperPlane} /></button>
                        </div>
                    </div>
                    {props.route.stops?.some(s => s.status === 0) ? (
                        <button className="w-full bg-amber-500 rounded-lg text-white p-2" onClick={overfilledRouteAction}><FontAwesomeIcon icon={faTriangleExclamation} /> {t("overfilledRouteButton")}</button>
                    ) : (
                        <button className="w-full bg-green-500 rounded-lg text-white p-2" onClick={completeRouteAction}><FontAwesomeIcon icon={faCircleCheck} /> {t("completeRouteButton")}</button>
                    )}
                </>
            )}
        </div>
    );
}