import type { FormEvent } from "react";
import { useAuth } from "@frontegg/react";
import { useCreateProject } from "../../../api/scoutroute";
import { useQueryClient } from "@tanstack/react-query";

export default function ProjectCreator() {
    const { user } = useAuth();
    const queryClient = useQueryClient();

    const mutation = useCreateProject({ axios: { headers: { "Authorization": `Bearer ${user?.accessToken}` }, }, mutation: { onSuccess: () => { queryClient.invalidateQueries({ queryKey: ['projects'] }); } } });

    const createProject = async (e: FormEvent) => {
        e.preventDefault();
        var data = new FormData(e.target as HTMLFormElement);

        var guid = crypto.randomUUID();

        await mutation.mutateAsync({ data: { id: guid, name: data.get("name")?.toString() ?? "New project" } });
    };

    return (
        <form onSubmit={createProject}>
            <div>
                <label>Navn</label>
                <input type="text" name="name" className="border-1 border-gray-400 rounded-md p-2 ml-2" />
            </div>
            <div>
                <button type="submit">Opret</button>
            </div>
        </form>
    );
}