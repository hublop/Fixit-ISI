import { SubcategoryInProfile } from './SubcategoryInProfile';
export interface CategoryInProfile {
    id: number;
    name: string;
    subcategories: SubcategoryInProfile[];
}
