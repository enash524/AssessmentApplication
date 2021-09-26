export class AddressModel {
    public addressId: number | null;
    public address1: string | null;
    public address2?: string;
    public city: string | null;
    public stateOrProvinceCode: string | null;
    public postalCode: string | null;

    constructor() {
        this.addressId = null;
        this.address1 = null;
        this.city = null;
        this.stateOrProvinceCode = null;
        this.postalCode = null;
    }
}
