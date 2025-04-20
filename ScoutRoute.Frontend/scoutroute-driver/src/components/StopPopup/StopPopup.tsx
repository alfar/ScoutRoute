import { Popup, useMap } from "react-leaflet";
import StopActions from "../StopActions/StopActions";
import { useTranslation } from "react-i18next";

interface StopPopupProps {
    stop: Stop;
}

export default function StopPopup(props: StopPopupProps) {
    const map = useMap();
    const { t } = useTranslation();

    return (
        <Popup>
            <div>{props.stop.name}</div>
            <div>{t("amount", { count: props.stop.amount })}</div>
            <div><StopActions id={props.stop.id} comment={props.stop.comment} onSaved={() => { map.closePopup(); }} /></div>
        </Popup>
    )
}