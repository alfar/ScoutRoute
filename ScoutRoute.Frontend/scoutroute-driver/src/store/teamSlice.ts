import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit'
import { getTeam, updateTeamName, updateTeamLead, updateTeamPhone, addTeamMember, removeTeamMember, updateTeamTrailerType } from '../api/scoutroute';
import { RootState } from './store';
import { TeamDto } from '../models';

enum TrailerType {
    Small = 1, // Small Trailer
    Large = 2, // Large Trailer
    Buggy = 3  // Boogie Trailer
}

interface TeamState {
    projectId: string;
    id: string;
    loaded: boolean;
    name?: string | null | undefined;
    teamLead?: string | null | undefined;
    trailerType?: TrailerType | null | undefined;
    phone?: string | null | undefined;
    members?: string[] | null | undefined;
}

const storedTeam = localStorage.getItem("team");
const initialState: TeamState = storedTeam ? { ...JSON.parse(storedTeam), loaded: false } : {
    projectId: "00000000-0000-0000-0000-000000000000",
    id: "9760d510-4199-47c6-965e-c8151fe21808",
    loaded: true,
    name: "Hamstrene",
    teamLead: "Lasse Lundby",
    trailerType: 3,
    phone: "53674848",
    members: [
        "Rune",
        "Liv",
        "Asger"
    ]
};

export const fetchTeam = createAsyncThunk<TeamDto, { projectId: string, id: string }, { state: RootState }>(
    'team/fetchTeam',
    ({ projectId, id }, _thunkApi) => {
        return getTeam(projectId, id).then(resp => resp.data);
    }
);

export const mutateTeamName = createAsyncThunk<void, string, { state: RootState }>(
    'team/updateName',
    (name, _thunkApi) => {
        const team = _thunkApi.getState().team;
        return updateTeamName(team.projectId, team.id, { name }).then(resp => resp.data);
    }
);

export const mutateTeamLead = createAsyncThunk<void, string, { state: RootState }>(
    'team/updateTeamLead',
    (teamLead, _thunkApi) => {
        const team = _thunkApi.getState().team;
        return updateTeamLead(team.projectId, team.id, { teamLead }).then(resp => resp.data);
    }
);

export const mutateTeamPhone = createAsyncThunk<void, string, { state: RootState }>(
    'team/updateTeamPhone',
    (phone, _thunkApi) => {
        const team = _thunkApi.getState().team;
        return updateTeamPhone(team.projectId, team.id, { phone }).then(resp => resp.data);
    }
);

export const mutateTeamTrailerType = createAsyncThunk<void, number, { state: RootState }>(
    'team/updateTeamTrailerType',
    (trailerType, _thunkApi) => {
        const team = _thunkApi.getState().team;
        return updateTeamTrailerType(team.projectId, team.id, { trailerType }).then(resp => resp.data);
    }
);

export const addMember = createAsyncThunk<void, string, { state: RootState }>(
    'team/addMember',
    (memberName, _thunkApi) => {
        const team = _thunkApi.getState().team;
        return addTeamMember(team.projectId, team.id, { memberName }).then(resp => resp.data);
    }
);

export const removeMember = createAsyncThunk<void, number, { state: RootState }>(
    'team/removeMember',
    (index, _thunkApi) => {
        const team = _thunkApi.getState().team;
        if (team.members && index >= 0 && index < team.members.length) {
            return removeTeamMember(team.projectId, team.id, team.members[index]).then(resp => resp.data);
        }
        return Promise.reject(new Error("Invalid member index"));
    }
);

export const teamSlice = createSlice({
    name: "team",
    initialState,
    reducers: {
    },
    extraReducers: (builder) => {
        builder.addCase(fetchTeam.pending, (state, action: PayloadAction<undefined, string, { arg: { projectId: string, id: string }; requestId: string; requestStatus: "pending"; }, never>) => {
            state.projectId = action.meta.arg.projectId;
            state.id = action.meta.arg.id;
            state.loaded = false;
            localStorage.setItem("team", JSON.stringify(state));
        })
        builder.addCase(fetchTeam.fulfilled, (state, action: PayloadAction<TeamDto>) => {
            state.name = action.payload.name;
            state.teamLead = action.payload.teamLead;
            state.members = action.payload.members;
            state.phone = action.payload.phone;
            state.trailerType = action.payload.trailerType;
            state.loaded = true;
            localStorage.setItem("team", JSON.stringify(state));
        });

        builder.addCase(mutateTeamName.pending, (state, action: PayloadAction<undefined, string, { arg: string; requestId: string; requestStatus: "pending"; }, never>) => {
            state.name = action.meta.arg; // Optimistically update the name
        });
        builder.addCase(mutateTeamName.fulfilled, (_state, _action: PayloadAction<void>) => {
            // No need to update state here, as the name is already updated in the state
        });

        builder.addCase(mutateTeamLead.pending, (state, action: PayloadAction<undefined, string, { arg: string; requestId: string; requestStatus: "pending"; }, never>) => {
            state.teamLead = action.meta.arg; // Optimistically update the team lead
        });
        builder.addCase(mutateTeamLead.fulfilled, (_state, _action: PayloadAction<void>) => {
            // No need to update state here, as the team lead is already updated in the state
        });

        builder.addCase(mutateTeamPhone.pending, (state, action: PayloadAction<undefined, string, { arg: string; requestId: string; requestStatus: "pending"; }, never>) => {
            state.phone = action.meta.arg; // Optimistically update the phone
        });
        builder.addCase(mutateTeamPhone.fulfilled, (_state, _action: PayloadAction<void>) => {
            // No need to update state here, as the phone is already updated in the state
        });

        builder.addCase(mutateTeamTrailerType.pending, (state, action: PayloadAction<undefined, string, { arg: number; requestId: string; requestStatus: "pending"; }, never>) => {
            state.trailerType = action.meta.arg; // Optimistically update the trailer type
        });

        builder.addCase(addMember.pending, (state, action: PayloadAction<undefined, string, { arg: string; requestId: string; requestStatus: "pending"; }, never>) => {
            state.members?.push(action.meta.arg); // Optimistically add the member
        });
        builder.addCase(addMember.fulfilled, (_state, _action: PayloadAction<void>) => {
            // No need to update state here, as the member is already added in the state
        });

        builder.addCase(removeMember.pending, (_state, _action: PayloadAction<undefined, string, { arg: number; requestId: string; requestStatus: "pending"; }, never>) => {
            // Too early to optimistically remove the member, we will handle this in the fulfilled case
        });

        builder.addCase(removeMember.fulfilled, (state, action: PayloadAction<void, string, { arg: number; requestId: string; requestStatus: "fulfilled"; }, never>) => {
            if (state.members && state.members.length > action.meta.arg) {
                state.members?.splice(action.meta.arg, 1); // Optimistically remove the member
            }
        });
    }
});

export default teamSlice.reducer;
