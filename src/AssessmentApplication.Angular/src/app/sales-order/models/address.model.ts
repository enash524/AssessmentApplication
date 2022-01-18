export interface AddressModel {
  addressId: number;
  address1: string;
  address2?: string;
  city: string;
  stateOrProvinceCode: string;
  postalCode: string;
}
