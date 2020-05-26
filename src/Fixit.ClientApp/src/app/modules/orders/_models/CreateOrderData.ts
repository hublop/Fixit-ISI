export interface CreateOrderData {
    description: string;
    subcategoryId: number;
    customerId: number;
    placeId: string;
    base64Photos: string[];
}
