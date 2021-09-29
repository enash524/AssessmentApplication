export class PersonModel {
    public businessEntityId: number | null = null;
    public title: string | null = null;
    public firstName: string | null = null;
    public middleName: string | null = null;
    public lastName: string | null = null;
    public suffix: string | null = null;

    get fullName(): string {
        return [this.title, this.firstName, this.middleName, this.lastName, this.suffix].join(' ');
    }
}
