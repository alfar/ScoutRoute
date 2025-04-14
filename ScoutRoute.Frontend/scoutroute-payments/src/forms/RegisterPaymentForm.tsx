import { useMutation } from "@tanstack/react-query";
import { Payment, registerPayment } from "../api/payments";

export default function RegisterPaymentForm()
{
    const registerPaymentMutation = useMutation({
        mutationFn: (payment: Payment) => {
            return registerPayment(payment);
        }
    });

    const send = () => {
        registerPaymentMutation.mutate({ id: crypto.randomUUID(), message: "Hello", amount: 40, received: new Date() })
    };

    return (
        <form className="flex flex-col gap-4">
            { registerPaymentMutation.isPending && <>Venter</>}
            <input type="datetime-local" placeholder="Modtaget" disabled={registerPaymentMutation.isPending} />
            <input type="tel" placeholder="Telefon" disabled={registerPaymentMutation.isPending} />
            <input type="text" placeholder="Navn" disabled={registerPaymentMutation.isPending} />
            <input type="text" placeholder="Besked" disabled={registerPaymentMutation.isPending} />
            <input type="number" placeholder="Beløb" disabled={registerPaymentMutation.isPending} />
            <button type="button" onClick={send} disabled={registerPaymentMutation.isPending}>Gem</button>
        </form>
    );
}