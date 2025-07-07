import React from 'react';
import { useAuth } from '@frontegg/react';
import { useQueryClient } from '@tanstack/react-query';
import { useCreateRoute } from '../../../api/scoutroute';
import type { CreateRouteCommand } from '../../../models';
import Section from '../../Section/Section';

interface RouteCreatorProps {
    projectId: string;
    selectedStops: string[];
}

export default function RouteCreator(props: RouteCreatorProps) {
    const { user } = useAuth();
    const queryClient = useQueryClient();
    const { mutateAsync: createRoute, isPending } = useCreateRoute({ axios: { headers: { Authorization: `Bearer ${user?.accessToken}` } } });

    const onSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const RouteData: CreateRouteCommand = {
            id: crypto.randomUUID(), // Generate random GUID for id
            name: formData.get('name') as string,
            stops: props.selectedStops
        };

        try {
            await createRoute({ projectId: props.projectId, data: RouteData });
            queryClient.invalidateQueries({ queryKey: [`routes/${props.projectId}`] });
            event.currentTarget.reset();
        } catch (error) {
            console.error('Failed to create Route:', error);
        }
    };

    return (
        <Section title="Opret rute">
            <form onSubmit={onSubmit}>
                <div>
                    <label htmlFor="name">Name:</label>
                    <input id="name" name="name" type="text" required />
                </div>
                <button type="submit" disabled={isPending}>
                    {isPending ? 'Submitting...' : 'Create Route'}
                </button>
            </form>
        </Section>
    );
};

