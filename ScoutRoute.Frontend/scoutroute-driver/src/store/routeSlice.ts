import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit'
import { RouteDto } from '../models';
import { assignTeam, getRoute } from '../api/scoutroute';
import { RootState } from './store';
import { fetchTeam } from './teamSlice';

interface RouteState {
    routes: Route[];
}

const initialState: RouteState = {
    routes: [],
}

interface UpdateStopPayload {
    id: string;
}

interface RoutePayload {
    id: string;
}

interface AddCommentPayload {
    id: string;
    comment: string;
}

export const fetchRoute = createAsyncThunk<RouteDto, { projectId: string, routeId: string }, { state: RootState }>(
    'route/fetchRoute',
    ({ projectId, routeId }, _thunkApi) => {
        return getRoute(projectId, routeId).then(resp => resp.data);
    }
);

export const assignRouteTeam = createAsyncThunk<void, { projectId: string, teamId: string, routeId: string }, { state: RootState }>(
    'route/assignTeam',
    ({ projectId, teamId, routeId }, _thunkApi) => {
        return assignTeam(projectId, routeId, { teamId }).then(resp => resp.data);
    }
);

export const routeSlice = createSlice({
    name: 'route',
    initialState,
    reducers: {
        addRoute: (state, action: PayloadAction<Route>) => {
            state.routes.push(action.payload);
        },
        addComment: (state, action: PayloadAction<AddCommentPayload>) => {
            const route = state.routes.find(r => r.id === action.payload.id);
            if (route) {
                route.comments?.push(action.payload.comment);
            }
        },
        addExtraStop: (state, action: PayloadAction<RoutePayload>) => {
            const route = state.routes.find(r => r.id === action.payload.id);
            if (route) {
                route.extraStops += 1;
            }
        },
        removeExtraStop: (state, action: PayloadAction<RoutePayload>) => {
            const route = state.routes.find(r => r.id === action.payload.id);
            if (route && route.extraStops > 0) {
                route.extraStops -= 1;
            }
        },
        completeRoute: (state, action: PayloadAction<RoutePayload>) => {
            const route = state.routes.find(r => r.id === action.payload.id);
            if (route && route.status === 0) {
                route.status = 1;

                state.routes.sort(r => r.status);
            }
        },
        overfilledRoute: (state, action: PayloadAction<RoutePayload>) => {
            const route = state.routes.find(r => r.id === action.payload.id);
            if (route && route.status === 0) {
                route.status = 2;

                state.routes.sort(r => r.status);
            }
        },
        resetRouteStatus: (state, action: PayloadAction<RoutePayload>) => {
            const route = state.routes.find(r => r.id === action.payload.id);
            if (route && route.status !== 0) {
                route.status = 0;

                state.routes.sort(r => r.status);
            }
        },
        pickupStop: (state, action: PayloadAction<UpdateStopPayload>) => {
            state.routes.map(r => {
                const stop = r.stops?.find(s => s.id === action.payload.id);
                if (stop) {
                    stop.status = 1;
                }
            });
        },
        notFoundStop: (state, action: PayloadAction<UpdateStopPayload>) => {
            state.routes.map(r => {
                const stop = r.stops?.find(s => s.id === action.payload.id);
                if (stop) {
                    stop.status = 2;
                }
            });
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchTeam.fulfilled, (state, action) => {
            debugger;
            state.routes = action.payload.routes?.map(route => ({
                ...route,
                loaded: false,
                extraStops: 0,
                status: 0,
            })) || [];
        });

        //builder.addCase(fetchRoute.pending, (state, action) => {});

        builder.addCase(fetchRoute.fulfilled, (state, action) => {
            const route = state.routes.find(r => r.id === action.payload.id);
            if (route) {
                Object.assign(route, action.payload);
                route.loaded = true;
            } else {
                state.routes.push({
                    ...action.payload,
                    loaded: true,
                    extraStops: 0,
                    status: 0,
                });
            }
        });
    },
});

export const { addRoute, addComment, addExtraStop, removeExtraStop, completeRoute, overfilledRoute, resetRouteStatus, pickupStop, notFoundStop } = routeSlice.actions;
export default routeSlice.reducer;