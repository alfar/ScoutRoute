import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircleCheck } from "@fortawesome/free-regular-svg-icons/faCircleCheck";
import { faCircleXmark } from "@fortawesome/free-regular-svg-icons/faCircleXmark";
import { faCircle } from "@fortawesome/free-regular-svg-icons";

interface StatusIconProps
{
    status: number;
}

export default function StatusIcon(props: StatusIconProps)
{
    const [mark, color] = props.status === 0 ? [faCircle, ""] : props.status === 1 ? [faCircleCheck, "text-green-500"] : [faCircleXmark, "text-red-500"]

    return (
        <span className={color + " text-4xl"}><FontAwesomeIcon icon={mark} /></span>
    );
}