import type { PropsWithChildren } from "react";

interface ListProps {
}

export default function List(props: PropsWithChildren<ListProps>) {
    return (
        <ul className="flex flex-col gap-2">
            {props.children}
        </ul>
    );
}