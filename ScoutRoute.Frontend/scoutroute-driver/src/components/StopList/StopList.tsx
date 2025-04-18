import { useTranslations } from "next-intl";

interface StopListProps {
    stops: Stop[];
}

export default function StopList(props: StopListProps)
{
    const t = useTranslations("Index");

    return (
        <table>
            <tr><th>{t("addressHeader")}</th><th>{t("amountHeader")}</th><th>{t("statusHeader")}</th></tr>
            {props.stops.map(stop => (
                <tr key={stop.id}><td>{stop.name}</td><td>{stop.amount}</td><td>{stop.status}</td></tr>
            ))}
        </table>

    );
}