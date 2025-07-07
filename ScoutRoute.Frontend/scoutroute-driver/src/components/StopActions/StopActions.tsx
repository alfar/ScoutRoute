import { useAppDispatch } from "../../store/hooks";
import { notFoundRouteStop, completeRouteStop } from "../../store/routeSlice";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircleCheck, faCircleXmark } from "@fortawesome/free-solid-svg-icons";
import NavigateButton from "../NavigateButton/NavigateButton";

interface StopActionsProps {
    id: string;
    name: string;
    onSaved: () => any;
}

export default function StopActions(props: StopActionsProps) {
    const dispatch = useAppDispatch();

    const pickupAction = () => {
        dispatch(completeRouteStop({ id: props.id }));
        props.onSaved();
    };

    const notFoundAction = () => {
        dispatch(notFoundRouteStop({ id: props.id }));
        props.onSaved();
    };

    return (
        <>
            <div className="flex gap-2">
                <NavigateButton name={props.name} />
                <button className="w-1/2 bg-green-500 rounded-md text-white p-2 text-2xl" onClick={pickupAction}><FontAwesomeIcon icon={faCircleCheck} /></button>
                <button className="w-1/2 bg-red-500 rounded-md text-white p-2 text-2xl" onClick={notFoundAction}><FontAwesomeIcon icon={faCircleXmark} /></button>
            </div>
        </>
    );
}