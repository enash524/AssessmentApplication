export class PersonModel {
    public businessEntityId: number | null;
    public title: string | null;
    public firstName: string | null;
    public middleName: string | null;
    public lastName: string | null;
    public suffix: string | null;

    get fullName(): string {
        return [this.title, this.firstName, this.middleName, this.lastName, this.suffix].join(' ');
    }

    constructor() {
        this.businessEntityId = null;
        this.title = null;
        this.firstName = null;
        this.middleName = null;
        this.lastName = null;
        this.suffix = null;
    }
}