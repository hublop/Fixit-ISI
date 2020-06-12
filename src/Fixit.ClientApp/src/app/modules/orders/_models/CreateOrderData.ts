export interface CreateOrderData {
    description: string;
    subcategoryId: number;
    customerId: number;
    placeId: string;
    latitude: number;
    longitude: number;
    base64Photos: string[];
}
