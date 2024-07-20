import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ForgotpasswordService } from '../../../core/services/account/forgotpassword.service';
import { CommonModule } from '@angular/common';
import { Router,ActivatedRoute } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-forget-password',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css'] // Corrected to styleUrls
})
export class ForgetPasswordComponent {
  forgotpass: FormGroup;

  constructor(private forgotpasswordService: ForgotpasswordService,private route: ActivatedRoute,
    private router: Router) {
    this.forgotpass = new FormGroup({
      email: new FormControl('', [
        Validators.required,
        Validators.email // Custom pattern to ensure it contains '@' and '.com'
      ])
    });
  }

  forgot() {console.log("in forgot")
    if (this.forgotpass.valid) {
      const email: string = this.forgotpass.get('email')?.value;
      this.forgotpasswordService.forgot(email).subscribe(
        {
          next:(data)=>{
            console.log("send success", data);
            Swal.fire({
              position: "top-end",
              icon: "success",
              title: "Email sent successfully , Check your email",
              showConfirmButton: false,
              timer: 1500
            }).then(() => {
              this.router.navigate(['/login']); // Redirect to login page
            }); ;
          },
          error:(error) => {
            Swal.fire({
              icon: "error",
              title: "Oops...",
              text: "User not found",
            });          }
        }
      );
    } else {
      console.log('Form is invalid');
    }
  }

  get email() {
    return this.forgotpass.get('email');
  }
}
