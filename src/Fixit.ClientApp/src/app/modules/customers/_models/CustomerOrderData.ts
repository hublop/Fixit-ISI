export interface CustomerOrderData {
  id: number;
  description: string;
  placeId: string;
  subcategoryName: string;
  categoryName: string;
  creationDate: string;
  photoUrls: string[];
  orderOffers: OrderOfferData[];
}

export interface OrderOfferData {
  contractor: ContractorData;
  comment: string;
  predictedPrice: number;
}

export interface ContractorData {
  id: number;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
  photoUrl: string;
}
