import { Pipe, PipeTransform } from '@angular/core';
import { InsurancePlanLevel } from '../features/company-request/Models/company-requests';

@Pipe({
  name: 'levelInsurance',
  standalone: true
})
export class LevelInsurancePipe implements PipeTransform {

  transform(value: InsurancePlanLevel): string {
    switch (value) {
      case InsurancePlanLevel.basic:
        return 'Basic';
      case InsurancePlanLevel.Standard:
        return 'Standard';
      case InsurancePlanLevel.Premium:
        return 'Premium';
      default:
        return 'Unknown';
    }
  }

}
