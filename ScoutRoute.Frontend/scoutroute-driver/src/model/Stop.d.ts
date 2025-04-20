interface Stop {
    id: string;
    name: string;
    amount: number;
    status: number;
    comment: string;

    coordinates: Tuple<number, number>;
}