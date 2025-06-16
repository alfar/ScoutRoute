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

    const drawerClasses = expanded ? "overflow-hidden h-12 transition-all duration-500 mt-2" : "overflow-hidden h-0 transition-all duration-500";

    return (
        <div className={"w-full border-gray-200 border-2 rounded-md p-3 gap-1 " + background} onClick={() => setExpanded(!expanded)}>
            <div className="flex justify-between">
                <div className="text-2xl">{props.stop.title}</div>
                <div className="text-right"><StatusIcon status={props.stop.status} /></div>
            </div>
            <div>{t('amount', { count: props.stop.quantity })}</div>
            <div className="col-span-2">
                <div className={drawerClasses} onClick={e => { e.stopPropagation(); }}>
                    <StopActions id={props.stop.id} name={props.stop.title} onSaved={() => setExpanded(false)} />
                </div>
            </div>
        </div>
    );
}