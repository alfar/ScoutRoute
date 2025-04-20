import { createSlice, PayloadAction } from '@reduxjs/toolkit'

interface RouteState {
    routes: Route[];
}

const initialState: RouteState = {
    routes: [
        {
            id: "route1",
            name: "Vesterbakken",
            stops: [
                {
                    id: "stop1",
                    name: "Lille Ballevej 24",
                    amount: 1,
                    status: 0,
                    comment: "",
                    coordinates: [
                        56.18381073,
                        9.51633061,
                    ],
                },
                {
                    id: "stop2",
                    name: "Vesterbakken 30",
                    amount: 1,
                    status: 0,
                    comment: "",
                    coordinates: [
                        56.18353861,
                        9.51727007,
                    ],
                },
                {
                    id: "stop3",
                    name: "Vesterbakken 29",
                    amount: 1,
                    status: 0,
                    comment: "",
                    coordinates: [
                        56.18355251,
                        9.51663018,
                    ],
                },
                {
                    id: "stop4",
                    name: "Vesterbakken 28",
                    amount: 1,
                    status: 0,
                    comment: "",
                    coordinates: [
                        56.18337511,
                        9.51749841,
                    ],
                },
                {
                    id: "stop5",
                    name: "Vesterbakken 27",
                    amount: 1,
                    status: 0,
                    comment: "",
                    coordinates: [
                        56.18338716,
                        9.51683127,
                    ],
                },
                {
                    id: "stop6",
                    name: "Vesterbakken 26",
                    amount: 2,
                    status: 0,
                    comment: "",
                    coordinates: [
                        56.18321469,
                        9.51772518,
                    ],
                },
                {
                    id: "stop7",
                    name: "Vesterbakken 23",
                    amount: 1,
                    status: 0,
                    comment: "",
                    coordinates: [
                        56.18311884,
                        9.51724799,
                    ],
                },

            ]
        }
    ],
}

interface UpdateStopPayload {
    id: string;
    comment: string;
}

export const routeSlice = createSlice({
    name: 'route',
    initialState,
    reducers: {
        add: (state, action: PayloadAction<Route>) => {
            state.routes.push(action.payload);
        },
        pickupStop: (state, action: PayloadAction<UpdateStopPayload>) => {
            state.routes.map(r => {
                const stop = r.stops.find(s => s.id === action.payload.id);
                if (stop) {
                    stop.comment = action.payload.comment;
                    stop.status = 1;
                }
            });
        },
        notFoundStop: (state, action: PayloadAction<UpdateStopPayload>) => {
            state.routes.map(r => {
                const stop = r.stops.find(s => s.id === action.payload.id);
                if (stop) {
                    stop.comment = action.payload.comment;
                    stop.status = 2;
                }
            });
        },
    }
});

export const { add, pickupStop, notFoundStop } = routeSlice.actions
export default routeSlice.reducer