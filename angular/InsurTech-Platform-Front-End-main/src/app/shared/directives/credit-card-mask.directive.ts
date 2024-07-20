import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[creditCardMask]',
  standalone: true
})
export class CreditCardMaskDirective {
  constructor(private el: ElementRef) {
    this.el.nativeElement.placeholder = 'XXXX XXXX XXXX XXXX';
  }

  @HostListener('input', ['$event'])
  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    let value = input.value.replace(/\D/g, ''); // Remove non-digit characters

    if (value.length > 16) {
      value = value.slice(0, 16); // Ensure maximum of 16 digits
    }

    let formattedValue = '';
    for (let i = 0; i < value.length; i++) {
      if (i > 0 && i % 4 === 0) {
        formattedValue += ' ';
      }
      formattedValue += value[i];
    }

    input.value = formattedValue;
  }
}
