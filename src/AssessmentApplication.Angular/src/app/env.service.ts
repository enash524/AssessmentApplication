import { Injectable } from '@angular/core';


@Injectable({
    providedIn: 'root',
})
export class EnvService {

    public init(): Promise<void> {
        return new Promise(resolve => {
            resolve();
        });
    }

}
