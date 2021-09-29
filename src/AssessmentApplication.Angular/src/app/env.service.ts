import { Injectable } from '@angular/core';


@Injectable({
    providedIn: 'root',
})
export class EnvService {

    private _api: string = '';

    public get api(): string {
        return this._api;
    }

    public init(): Promise<void> {
        return new Promise(resolve => {
            this._api = '';
            resolve();
        });
    }

}
