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
            <div>{props.stop.title}</div>
            <div>{t("amount", { count: props.stop.quantity })}</div>
            <div><StopActions id={props.stop.id} name={props.stop.title} onSaved={() => { map.closePopup(); }} /></div>
        </Popup>
    )
}