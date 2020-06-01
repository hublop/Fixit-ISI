import { CategoryInProfile } from './CategoryInProfile';
import { OpinionInProfile } from './OpinionInProfile';
export class ContractorProfile {
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
    contractorUUID: string;
    imageUrl: string;
    subscriptionStatus: string;
    subscriptionUUID: string;
    nextPaymentDate: Date;
    repairServices: RepairService[];
}
