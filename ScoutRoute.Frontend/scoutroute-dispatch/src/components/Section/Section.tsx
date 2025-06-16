interface SectionProps {
    title?: string;
}

export default function Section(props: React.PropsWithChildren<SectionProps>) {
    return (
        <section className="border-2 border-gray-200 rounded-lg p-4 m-4">
            {props.title && <h1 className="text-xl font-bold mb-2">{props.title}</h1>}
            {props.children}
        </section>
    );
}