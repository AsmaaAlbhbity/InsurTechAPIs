import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[cvvMask]'
})
export class CvvMaskDirective {
  private previousValue = '';

  constructor(private el: ElementRef) {}

  @HostListener('input', ['$event'])
  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    let value = input.value.replace(/\D/g, '');

    if (value.length > 3) {
      value = value.slice(0, 3);
    }

    if (this.previousValue !== value) {
      this.previousValue = value;
      input.value = value;
    }
  }
}
