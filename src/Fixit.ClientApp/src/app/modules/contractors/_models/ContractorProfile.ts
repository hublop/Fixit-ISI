import { CategoryInProfile } from './CategoryInProfile';
import { OpinionInProfile } from './OpinionInProfile';
export interface ContractorProfile {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    companyName: string;
    selfDescription: string;
    contractorFrom: Date;
    city: string;
    placeId: string;
    avgQuality: number;
    avgPunctuality: number;
    avgInvolvement: number;
    avgRating: number;
    categories: CategoryInProfile[];
    opinions: OpinionInProfile[];
    imageUrl: string;
}
