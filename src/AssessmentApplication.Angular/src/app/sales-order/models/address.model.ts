export interface AddressModel {
    addressId: number | null;
    address1: string | null;
    address2?: string;
    city: string | null;
    stateOrProvinceCode: string | null;
    postalCode: string | null;
}
