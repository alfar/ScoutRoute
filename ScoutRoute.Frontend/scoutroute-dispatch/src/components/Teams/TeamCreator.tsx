import type { FormEvent } from "react";
import { useAuth } from "@frontegg/react";
import { useCreateTeam } from "../../api/scoutroute";
import { useQueryClient } from "@tanstack/react-query";

interface TeamCreatorProps {
    projectId: string;
}

export default function TeamCreator(props: TeamCreatorProps) {
    const { user } = useAuth();
    const queryClient = useQueryClient();

    const mutation = useCreateTeam({ axios: { headers: { "Authorization": `Bearer ${user?.accessToken}` }, }, mutation: { onSuccess: () => { queryClient.invalidateQueries({ queryKey: [`teams/${props.projectId}`] }); } } });

    const createTeam = async (e: FormEvent) => {
        e.preventDefault();

        var guid = crypto.randomUUID();

        await mutation.mutateAsync({ projectId: props.projectId, data: { teamId: guid } });
    };

    return (
        <form onSubmit={createTeam}>
            <div>
                <button type="submit">Opret</button>
            </div>
        </form>
    );
}