import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit'
import { RouteDto } from '../models';
import { assignTeam, getRoute, addComment, changeExtraStops, markComplete, markOverfilled, completeStop, notFoundStop, resetStatus } from '../api/scoutroute';
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

export const fetchRoute = createAsyncThunk<RouteDto, { projectId: string, routeId: string }, { state: RootState }>(
    'route/fetchRoute',
    ({ projectId, routeId }, _thunkApi) => {
        console.log("Oooh!");
        return getRoute(projectId, routeId).then(resp => resp.data);
    }
);

export const assignRouteTeam = createAsyncThunk<void, { projectId: string, teamId: string, routeId: string }, { state: RootState }>(
    'route/assignTeam',
    ({ projectId, teamId, routeId }, _thunkApi) => {
        return assignTeam(projectId, routeId, { teamId }).then(resp => resp.data);
    }
);

// Add a comment to a route
export const addRouteComment = createAsyncThunk(
    'route/addRouteComment',
    async ({
        projectId,
        routeId,
        comment,
    }: {
        projectId: string;
        routeId: string;
        comment: string;
    }) => {
        await addComment(projectId, routeId, { comment });
        return { projectId, routeId, comment };
    }
);

// Change extra stops for a route
export const changeRouteExtraStops = createAsyncThunk(
    'route/changeRouteExtraStops',
    async ({
        projectId,
        routeId,
        amount,
    }: {
        projectId: string;
        routeId: string;
        amount: number;
    }) => {
        await changeExtraStops(projectId, routeId, { extraStops: amount });
        return { projectId, routeId, amount };
    }
);

export const completeRoute = createAsyncThunk<void, RoutePayload, { state: RootState }>(
    'route/completeRoute',
    async ({ id }, thunkApi) => {
        const state = thunkApi.getState();
        const route = state.routes.routes.find(r => r.id === id);
        if (!route) throw new Error('Route not found');
        const projectId = (route as any).projectId || state.team.projectId;
        await markComplete(projectId, id);
        return;
    }
);

export const overfilledRoute = createAsyncThunk<void, RoutePayload, { state: RootState }>(
    'route/overfilledRoute',
    async ({ id }, thunkApi) => {
        const state = thunkApi.getState();
        const route = state.routes.routes.find(r => r.id === id);
        if (!route) throw new Error('Route not found');
        const projectId = (route as any).projectId || state.team.projectId;
        await markOverfilled(projectId, id);
        return;
    }
);

export const completeRouteStop = createAsyncThunk<void, UpdateStopPayload, { state: RootState }>(
    'route/completeStop',
    async ({ id }, thunkApi) => {
        const state = thunkApi.getState();
        const route = state.routes.routes.find(r => r.stops?.some(s => s.id === id));
        if (!route) throw new Error('Route not found');
        const projectId = (route as any).projectId || state.team.projectId;
        // Here you would call an API to mark the stop as completed
        await completeStop(projectId, id);
        return;
    }
);

export const notFoundRouteStop = createAsyncThunk<void, UpdateStopPayload, { state: RootState }>(
    'route/notFoundStop',
    async ({ id }, thunkApi) => {
        const state = thunkApi.getState();
        const route = state.routes.routes.find(r => r.stops?.some(s => s.id === id));
        if (!route) throw new Error('Route not found');
        const projectId = (route as any).projectId || state.team.projectId;
        // Here you would call an API to mark the stop as not found
        await notFoundStop(projectId, id);
        return;
    }
);

export const resetRouteStatus = createAsyncThunk<void, RoutePayload, { state: RootState }>(
    'route/resetRouteStatus',
    async ({ id }, thunkApi) => {
        const state = thunkApi.getState();
        const route = state.routes.routes.find(r => r.id === id);
        if (!route) throw new Error('Route not found');
        const projectId = (route as any).projectId || state.team.projectId;
        await resetStatus(projectId, id);
        return;
    }
);

export const routeSlice = createSlice({
    name: 'route',
    initialState,
    reducers: {
        resetRouteStatus: (state, action: PayloadAction<RoutePayload>) => {
            const route = state.routes.find(r => r.id === action.payload.id);
            if (route && route.status !== 0) {
                route.status = 0;

                state.routes.sort(r => r.status);
            }
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchTeam.fulfilled, (state, action) => {
            state.routes = action.payload.routes?.map(route => ({
                ...route,
                loaded: false,
                extraStops: 0,
                status: 0,
            })) || [];
        });

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

        builder.addCase(addRouteComment.fulfilled, (state, action) => {
            const route = state.routes.find(r => r.id === action.payload.routeId);
            if (route) {
                route.comments = route.comments || [];
                route.comments.push(action.payload.comment);
            }
        });

        builder.addCase(changeRouteExtraStops.fulfilled, (state, action) => {
            const route = state.routes.find(r => r.id === action.payload.routeId);
            if (route) {
                route.extraStops = action.payload.amount;
            }
        });

        builder.addCase(resetRouteStatus.fulfilled, (state, action) => {
            const route = state.routes.find(r => r.id === action.meta.arg.id);
            if (route) {
                route.status = 0; // Reset status
                state.routes.sort((a, b) => a.status - b.status); // Sort by status
            }
        });

        builder.addCase(completeRoute.fulfilled, (state, action) => {
            const route = state.routes.find(r => r.id === action.meta.arg.id);
            if (route) {
                route.status = 1; // Mark as complete
                state.routes.sort((a, b) => a.status - b.status); // Sort by status
            }
        });

        builder.addCase(overfilledRoute.fulfilled, (state, action) => {
            const route = state.routes.find(r => r.id === action.meta.arg.id);
            if (route) {
                route.status = 2; // Mark as overfilled
                state.routes.sort((a, b) => a.status - b.status); // Sort by status
            }
        });

        builder.addCase(completeRouteStop.fulfilled, (state, action) => {
            const route = state.routes.find(r => r.stops?.some(s => s.id === action.meta.arg.id));
            if (route) {
                const stop = route.stops?.find(s => s.id === action.meta.arg.id);
                if (stop) {
                    stop.status = 1; // Mark stop as completed
                }
            }
        });

        builder.addCase(notFoundRouteStop.fulfilled, (state, action) => {
            const route = state.routes.find(r => r.stops?.some(s => s.id === action.meta.arg.id));
            if (route) {
                const stop = route.stops?.find(s => s.id === action.meta.arg.id);
                if (stop) {
                    stop.status = 2; // Mark stop as not found
                }
            }
        });

        builder.addCase(assignRouteTeam.fulfilled, (state, action) => {
            const { routeId } = action.meta.arg;
            const route = state.routes.find(r => r.id === routeId);
            if (route) {
                route.loaded = false;
            }
            else {
                state.routes.push({
                    id: routeId,
                    loaded: false,
                    extraStops: 0,
                    status: 0,
                    stops: [],
                    comments: [],
                });
            }
        });
    },
});

export default routeSlice.reducer;