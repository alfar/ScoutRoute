'use client'

import { useTranslation } from "react-i18next";
import StatusIcon from "../StatusIcon/StatusIcon";
import { useState } from "react";
import StopActions from "../StopActions/StopActions";

interface StopCardProps {
    stop: Stop;
}

export default function StopCard(props: StopCardProps) {
    const { t } = useTranslation();
    const [expanded, setExpanded] = useState(false);

    const background = props.stop.status === 0 ? "bg-white" : "bg-gray-100";

    const drawerClasses = expanded ? "overflow-hidden h-55 transition-all duration-500" : "overflow-hidden h-0 transition-all duration-500";

    return (
        <div className={"w-full grid auto-cols-auto border-gray-200 border-2 rounded-md p-3 gap-1 " + background} onClick={() => setExpanded(!expanded)}>
            <div className="text-2xl">{props.stop.name}</div>
            <div className="row-span-2 text-right"><StatusIcon status={props.stop.status} /></div>
            <div>{t('amount', { count: props.stop.amount })}</div>
            <div className="col-span-2">
                <div className={drawerClasses} onClick={e => { e.preventDefault(); e.stopPropagation(); }}>
                    <StopActions id={props.stop.id} comment={props.stop.comment} onSaved={() => setExpanded(false)} />
                </div>
            </div>
        </div>
    );
}