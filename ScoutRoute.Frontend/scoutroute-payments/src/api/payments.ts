const base = "https://localhost:7520/";

const registerPaymentEndpoint = base + "payments";


export interface Payment 
{
    id: string;
    message: string;
    received: Date;
    amount: number;
}

export const registerPayment = (payment: Payment) =>
{
    return fetch(registerPaymentEndpoint, {
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        },
        body: JSON.stringify(payment),
        method: "POST",
        mode: "cors"
    });
}