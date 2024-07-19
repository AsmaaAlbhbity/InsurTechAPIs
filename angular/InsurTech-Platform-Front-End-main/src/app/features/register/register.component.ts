import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
  AsyncValidatorFn,
  ValidationErrors,
  ReactiveFormsModule,
  ValidatorFn,
} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { debounceTime, switchMap, catchError, map } from 'rxjs/operators';
import { CommonModule } from '@angular/common';
import { RegisterData } from '../../core/services/iregistration.service'; // Adjust the path as necessary
import { RegistrationService } from '../../core/services/registration.service'; // Adjust the path as necessary
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { InputMaskModule } from 'primeng/inputmask';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { HeaderComponent } from '../../shared/components/header/header.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    InputTextModule,
    PasswordModule,
    InputMaskModule,
    ButtonModule,
    ToastModule,
    HeaderComponent
  ],
  providers: [RegistrationService, MessageService, CookieService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class RegisterComponent {
  registerForm: FormGroup;
  processInProgress: string | null = null;

  constructor(
    private fb: FormBuilder,
    private registrationService: RegistrationService,
    private messageService: MessageService,
    private router: Router,
    private cookieService: CookieService
  ) {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      userName: [
        '',
        [Validators.required, this.usernameValidator()],
        [this.usernameAsyncValidator()],
      ],
      emailAddress: [
        '',
        [Validators.required, Validators.email],
        [this.emailAsyncValidator()],
      ],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          this.passwordValidator(),
        ],
      ],
      taxNumber: [
        '',
        [Validators.required, Validators.pattern(/^\d{9}$/)],
        [this.taxNumberAsyncValidator()],
      ],
      location: ['', Validators.required],
      phoneNumber: [
        '',
        [Validators.required, Validators.pattern(/^(011|012|010)\d{8}$/)],
      ],
    });
  }

  passwordValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const password = control.value;
      const hasUpperCase = /[A-Z]/.test(password);
      const hasNumber = /[0-9]/.test(password);
      const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(password);

      if (!hasUpperCase || !hasNumber || !hasSpecialChar) {
        return { passwordStrength: true };
      }
      return null;
    };
  }

  usernameValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const username = control.value;
      const isValid = /^[a-zA-Z0-9]+$/.test(username);

      if (!isValid) {
        return { invalidUsername: true };
      }
      return null;
    };
  }

  usernameAsyncValidator(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      return of(control.value).pipe(
        debounceTime(300),
        switchMap((username) =>
          this.registrationService.checkUsernameAvailability(username).pipe(
            map((isAvailable) =>
              isAvailable ? null : { usernameTaken: true }
            ),
            catchError(() => of(null))
          )
        )
      );
    };
  }

  emailAsyncValidator(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      return of(control.value).pipe(
        debounceTime(300),
        switchMap((email) =>
          this.registrationService.checkEmailAvailability(email).pipe(
            map((isAvailable) => (isAvailable ? null : { emailTaken: true })),
            catchError(() => of(null))
          )
        )
      );
    };
  }

  taxNumberAsyncValidator(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      return of(control.value).pipe(
        debounceTime(300),
        switchMap((taxNumber) =>
          this.registrationService.checkTaxNumberAvailability(taxNumber).pipe(
            map((isAvailable) =>
              isAvailable ? null : { taxNumberTaken: true }
            ),
            catchError(() => of(null))
          )
        )
      );
    };
  }

  onSubmit() {
    if (this.registerForm.invalid) {
      return;
    }

    const formData: RegisterData = {
      ...this.registerForm.value,
    };

    this.processInProgress = `Processing your request. Please check your email (${formData.emailAddress}) for further instructions.`;

    this.registrationService.registerUser(formData).subscribe({
      next: () => {
        this.handleSuccess(formData);
      },
      error: (error) => {
        this.handleSpecificErrors(error, formData);
      },
    });
  }

  handleSpecificErrors(error: any, formData: RegisterData): void {
    if (error.status === 400) {
      switch (error.error.message) {
        case 'Error in sending confirmation email':
          this.messageService.add({
            severity: 'warn',
            summary: 'Warning',
            detail:
              'Your account has been successfully created, but there was an error in sending the confirmation email. Please try to register again to receive your token.',
          });
          this.handleSuccess(formData);
          break;
        case 'Username already exists':
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'The username is already taken. Please choose another one.',
          });
          break;
        case 'Email already exists':
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail:
              'The email address is already registered. Please use another one.',
          });
          break;

        default:
          this.handleError(error);
          break;
      }
    } else {
      this.handleError(error);
    }
  }

  handleSuccess(response?: any): void {
    this.messageService.add({
      severity: 'success',
      summary: 'Registration Successful',
      detail:
        'Your account has been successfully created. Please check your email for further instructions to complete your registration.',
    });
    const userData = response || this.registerForm.value;
    this.cookieService.set('user', JSON.stringify(userData)); // Save user data in a cookie
  }

  handleError(error: any): void {
    this.messageService.add({
      severity: 'error',
      summary: 'Registration Failed',
      detail: 'An error occurred during registration. Please try again.',
    });
    console.error('Registration failed', error);
  }
}
