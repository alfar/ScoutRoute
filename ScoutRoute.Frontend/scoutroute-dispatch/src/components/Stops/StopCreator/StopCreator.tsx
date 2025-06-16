import React from 'react';
import { useCreateStop } from '../../../api/scoutroute';
import type { CreateStopCommand } from '../../../models';
import { useAuth } from '@frontegg/react';
import { useQueryClient } from '@tanstack/react-query';
import Section from '../../Section/Section';

interface StopCreatorProps {
    projectId: string;
}

export default function StopCreator(props: StopCreatorProps) {
    const { user } = useAuth();
    const queryClient = useQueryClient();
    const { mutateAsync: createStop, isPending } = useCreateStop({ axios: { headers: { Authorization: `Bearer ${user?.accessToken}` } } });

    const onSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const stopData: CreateStopCommand = {
            id: crypto.randomUUID(), // Generate random GUID for id
            title: formData.get('title') as string,
            quantity: Number(formData.get('quantity')),
            comment: formData.get('comment') as string,
            latitude: Number(formData.get('latitude')),
            longitude: Number(formData.get('longitude')),
            contactPerson: {
                name: formData.get('contactPerson.name') as string,
                phoneNumber: formData.get('contactPerson.phoneNumber') as string,
            },
        };

        try {
            await createStop({ projectId: props.projectId, data: stopData });
            queryClient.invalidateQueries({ queryKey: [`unassigned-stops/${props.projectId}`] });
            event.currentTarget.reset();
        } catch (error) {
            console.error('Failed to create stop:', error);
        }
    };

    return (
        <Section title="Opret stop">
            <form onSubmit={onSubmit}>
                <div>
                    <label htmlFor="title">Title:</label>
                    <input id="title" name="title" type="text" required />
                </div>
                <div>
                    <label htmlFor="quantity">Quantity:</label>
                    <input id="quantity" name="quantity" type="number" required />
                </div>
                <div>
                    <label htmlFor="comment">Comment:</label>
                    <textarea id="comment" name="comment" />
                </div>
                <div>
                    <label htmlFor="latitude">Latitude:</label>
                    <input id="latitude" name="latitude" type="text" required />
                </div>
                <div>
                    <label htmlFor="longitude">Longitude:</label>
                    <input id="longitude" name="longitude" type="text" required />
                </div>
                <fieldset>
                    <legend>Contact Person</legend>
                    <div>
                        <label htmlFor="contactPerson.name">Name:</label>
                        <input id="contactPerson.name" name="contactPerson.name" type="text" required />
                    </div>
                    <div>
                        <label htmlFor="contactPerson.phoneNumber">Phone Number:</label>
                        <input id="contactPerson.phoneNumber" name="contactPerson.phoneNumber" type="text" required />
                    </div>
                </fieldset>
                <button type="submit" disabled={isPending}>
                    {isPending ? 'Submitting...' : 'Create Stop'}
                </button>
            </form>
        </Section>
    );
};

