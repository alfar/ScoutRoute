import { faRoute } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

interface NavigateButtonProps
{
    name: string;    
}


export default function NavigateButton(props: NavigateButtonProps)
{
    return (
        <a href={"https://www.google.com/maps?q=" + encodeURIComponent(props.name) + "%2C%208600%20Silkeborg"} target="_blank" className="border-1 border-blue-600 rounded-md text-blue-600 p-2 text-lg"><FontAwesomeIcon icon={faRoute} /></a>
    )
}