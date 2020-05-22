import { SubcategoryInEditServices } from './SubcategoryInEditServices';

export interface CategoryInEditServices {
    id: number;
    name: string;
    description: string;
    subCategories: SubcategoryInEditServices[];
}
