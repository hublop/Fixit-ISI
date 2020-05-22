export interface AddOpinionData {
    punctuality: number;
    quality: number;
    involvement: number;
    comment: string;
    contractorId: number;
    customerId?: number;
    subcategoryId: number;
}
