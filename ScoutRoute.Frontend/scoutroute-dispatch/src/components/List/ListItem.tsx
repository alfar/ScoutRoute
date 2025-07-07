import type { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import type { PropsWithChildren } from "react";

interface ListItemProps {
    icon: IconDefinition;
}

export default function ListItem(props: PropsWithChildren<ListItemProps>)
{
    return (
        <li className="border-1 rounded-lg border-gray-200 p-2 flex">
            <FontAwesomeIcon icon={props.icon} className="text-3xl mr-2 text-gray-500" />
            {props.children}
        </li>
    );
}