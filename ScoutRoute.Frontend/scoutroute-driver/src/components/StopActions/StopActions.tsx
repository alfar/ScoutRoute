import { useTranslation } from "react-i18next";
import { useAppDispatch } from "../../store/hooks";
import { notFoundStop, pickupStop } from "../../store/routeSlice";
import { useState } from "react";

interface StopActionsProps {
    id: string;
    comment: string;
    onSaved: () => any;
}

export default function StopActions(props: StopActionsProps) {
    const { t } = useTranslation();
    const [comment, setComment] = useState(props.comment);
    const dispatch = useAppDispatch();

    const pickup = () => {
        dispatch(pickupStop({ id: props.id, comment }));
        props.onSaved();
    };

    const notFound = () => {
        dispatch(notFoundStop({ id: props.id, comment }));
        props.onSaved();
    };

    return (
        <>
            <label>{t("comment")}</label>
            <textarea className="w-full h-32 border-2 border-gray-300 p-1" value={comment} onChange={(e) => setComment(e.currentTarget.value)} />
            <div className="flex gap-2">
                <button className="w-1/2 bg-green-500 rounded-md text-white p-2 text-md" onClick={pickup}>{t("taken")}</button>
                <button className="w-1/2 bg-red-500 rounded-md text-white p-2 text-md" onClick={notFound}>{t("notFound")}</button>
            </div>
        </>
    );
}