export interface ContractorForList {
    id: number;
    firstName: string;
    lastName: string;
    companyName: string;
    placeId: string;
    avgRating: number;
    opinionsCount: number;
    newestOpinion: string;
    specializations: string[];
    subscriptionStatus: string;
    imageUrl: string;
}
