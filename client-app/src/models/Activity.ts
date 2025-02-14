export type Activity = {
    id: string;
    title: string;
    date: Date| undefined;
    description: string | null;
    category: string | null;
    city: string | null;
    venue: string | null;
};