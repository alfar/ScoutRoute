interface Stop {
    id: string;
    title: string;
    quantity: number;
    status: number;
    comment: string;

    coordinates: Tuple<number, number>;
}