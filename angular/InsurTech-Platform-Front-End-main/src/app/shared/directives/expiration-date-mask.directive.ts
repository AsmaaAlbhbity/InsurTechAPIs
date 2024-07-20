import { Directive, ElementRef, HostListener, Renderer2 } from '@angular/core';

@Directive({
  selector: '[expirationDateMask]',
  standalone: true
})
export class ExpirationDateMaskDirective {
  private errorElement: HTMLElement;

  constructor(private el: ElementRef, private renderer: Renderer2) {
    this.el.nativeElement.placeholder = 'MM/YY';
    this.errorElement = this.renderer.createElement('div');
    this.renderer.setStyle(this.errorElement, 'color', 'red');
    this.renderer.setStyle(this.errorElement, 'font-size', '12px');
    this.renderer.setStyle(this.errorElement, 'position', 'absolute');
    this.renderer.setStyle(this.errorElement, 'top', '100%');
    this.renderer.setStyle(this.errorElement, 'left', '0');
    this.renderer.appendChild(this.el.nativeElement.parentNode, this.errorElement);
  }

  @HostListener('input', ['$event'])
  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    let value = input.value.replace(/\D/g, ''); // Remove non-digit characters

    // Automatically add a leading zero for single-digit months
    if (value.length === 1 && parseInt(value) > 1) {
      value = '0' + value;
    }

    // Ensure month is between 01 and 12
    if (value.length >= 2) {
      let month = parseInt(value.slice(0, 2));
      if (month > 12) {
        month = 12;
      }
      if (month === 0) {
        month = 1;
      }
      value = month.toString().padStart(2, '0') + value.slice(2);
    }

    // Add slash after MM
    if (value.length > 2) {
      value = `${value.slice(0, 2)}/${value.slice(2, 4)}`;
    }

    // Ensure only 5 characters for MM/YY
    if (value.length > 5) {
      value = value.slice(0, 5);
    }

    input.value = value;

    // Validate the expiration date
    if (value.length === 5) {
      this.validateExpirationDate(value);
    } else {
      this.renderer.setProperty(this.errorElement, 'textContent', '');
    }
  }

  @HostListener('keydown', ['$event'])
  onKeydown(event: KeyboardEvent): void {
    const input = event.target as HTMLInputElement;
    const value = input.value;

    // Automatically add leading zero for single-digit months when typing '/'
    if (event.key === '/' && value.length === 1) {
      event.preventDefault();
      input.value = '0' + value + '/';
    }
  }

  private validateExpirationDate(value: string) {
    const [month, year] = value.split('/').map((val: string) => parseInt(val, 10));

    if (!month || !year) {
      this.renderer.setProperty(this.errorElement, 'textContent', 'Invalid date');
      return;
    }

    const currentDate = new Date();
    const currentMonth = currentDate.getMonth() + 1; // Months are zero-based
    const currentYear = currentDate.getFullYear() % 100; // Get last two digits of the year

    if ((year < currentYear) || (year === currentYear && month < currentMonth)) {
      this.renderer.setProperty(this.errorElement, 'textContent', 'The card has expired ');
    } else {
      this.renderer.setProperty(this.errorElement, 'textContent', '');
    }
  }
}
