import { Pipe, PipeTransform } from '@angular/core';
import { RequestStatus } from '../features/company-request/Models/company-requests';

@Pipe({
  name: 'statusLabel',
  standalone: true

})
export class StatusLabelPipe implements PipeTransform {
  transform(value: RequestStatus): string {
    switch (value) {
      case RequestStatus.Pending:
        return 'Pending';
      case RequestStatus.Approved:
        return 'Approved';
      case RequestStatus.Rejected:
        return 'Rejected';
      default:
        return 'Unknown';
    }
  }
}
