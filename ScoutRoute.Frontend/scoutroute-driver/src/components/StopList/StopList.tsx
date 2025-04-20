'use client'

import StopCard from "../StopCard/StopCard";

interface StopListProps {
    stops: Stop[];
}

export default function StopList(props: StopListProps) {
    return (
        <ul className="w-full flex flex-col gap-1">
            {props.stops.map(stop => (
                <li key={stop.id}><StopCard stop={stop} /></li>
            ))}
        </ul>
    );
}