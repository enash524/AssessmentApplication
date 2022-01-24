import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'fullAddress' })
export class FullAddressPipe implements PipeTransform {
  transform(
    address1: string,
    city: string,
    stateOrProvinceCode: string,
    postalCode: string,
    address2: string | null = null
  ): string {
    return [address1, address2, `${city}, ${stateOrProvinceCode} ${postalCode}`]
      .filter((str: string | null) => (str || '').length > 0)
      .join('<br />');
  }
}
