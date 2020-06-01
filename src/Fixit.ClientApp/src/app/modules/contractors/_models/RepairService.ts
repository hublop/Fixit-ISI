import {SubcategoryInOrder} from "./SubcategoryInOrder";

export interface RepairService {
  contractorId: number;
  subCategoryId: number;
  price: number;
  subCategory: SubcategoryInOrder;
  
}
