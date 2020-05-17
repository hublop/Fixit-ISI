import { SubcategoryInProfile } from './SubcategoryInProfile';
import { RatingInProfile } from './RatingInProfile';
import { CustomerInProfile } from './CustomerInProfile';
export interface OpinionInProfile {
    id: number;
    rating: RatingInProfile;
    comment: string;
    customer: CustomerInProfile;
    createdOn: Date;
    subcategory: SubcategoryInProfile;
}
