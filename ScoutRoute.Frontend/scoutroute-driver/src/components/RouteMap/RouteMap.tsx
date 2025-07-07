import { MapContainer, Marker, TileLayer } from "react-leaflet";

import 'leaflet/dist/leaflet.css';
import StopPopup from "../StopPopup/StopPopup";
import L from "leaflet";

interface RouteMapProps {
    route: Route;
}

export default function RouteMap(props: RouteMapProps) {
    const blueIcon = L.icon({
        iconUrl: "assets/blue_marker.png",
        iconSize: [24, 37]
    });

    const greenIcon = L.icon({
        iconUrl: "assets/green_marker.png",
        iconSize: [24, 37]
    });

    const redIcon = L.icon({
        iconUrl: "assets/red_marker.png",
        iconSize: [24, 37]
    });

    return (
        <div className="w-full h-96">
            <MapContainer center={props.route.stops[0].coordinates} zoom={16} style={{ height: "100%" }}>
                <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                {props.route.stops.map(s => (
                    <Marker position={s.coordinates} key={s.id} icon={s.status === 0 ? blueIcon : s.status === 1 ? greenIcon : redIcon}>
                        <StopPopup stop={s} />
                    </Marker>
                ))}
            </MapContainer>
        </div>
    );
}