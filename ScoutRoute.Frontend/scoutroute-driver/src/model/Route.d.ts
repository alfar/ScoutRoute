interface Route {
    id: string;
    loaded: boolean;
    name?: string;
    stops?: Stop[];
    extraStops: number;    
    comments?: string[];
    status: number;
}