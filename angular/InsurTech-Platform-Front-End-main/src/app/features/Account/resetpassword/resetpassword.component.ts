import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router,ActivatedRoute } from '@angular/router';
import { ForgotpasswordService } from '../../../core/services/account/forgotpassword.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-resetpassword',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.css']
})
export class ResetpasswordComponent implements OnInit {
  token: string = "";
  email: string = "";
  resetpass: FormGroup = new FormGroup({
    password: new FormControl("", [Validators.required]),
    confirmpassword: new FormControl("", Validators.required)
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router, 
    private forgotpasswordService: ForgotpasswordService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.token = params['token'];
      this.email = params['email'];
    });
  }

  reset() {
    const pass = this.resetpass.get('password')?.value;
    const confirmpass = this.resetpass.get('confirmpassword')?.value;
    if (pass === confirmpass) {
      this.forgotpasswordService.reset(this.email, this.token, pass).subscribe({
        next: (data) => {
          console.log("send success", data);
          Swal.fire({
            position: "top-end",
            icon: "success",
            title: "Password Reset Successfully",
            showConfirmButton: false,
            timer: 1500
          }).then(() => {
            this.router.navigate(['/login']); // Redirect to login page
          }); 
        },
        error: (error) => {
          console.log(error);
        }
      });
    } else if (!pass || !confirmpass) {
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Password and Confirm Password Required",
      });
    } else {
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Password and Confirm Password do not match",
      });
    }
  }
}
