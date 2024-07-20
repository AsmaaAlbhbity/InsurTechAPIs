
declare var google:any;
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { LoginService } from '../../../core/services/account/login.service'
import { RouterLink ,Router } from '@angular/router';
import { Googlelogin } from '../../../core/models/googlelogin';
import Swal from 'sweetalert2';
import { HeaderComponent } from '../../../shared/components/header/header.component';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule ,CommonModule,RouterLink, HeaderComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  
  ngOnInit(): void {
    google.accounts.id.initialize({
      client_id:'720571637733-0dkqjkrolhqs2iq1vbbb4bigmacf4sje.apps.googleusercontent.com',
      callback:(res:any)=>this.handlelogin(res)
    })

    google.accounts.id.renderButton(document.getElementById("google-btn"),{
    theme:'filled_white',
    size:'small',
    shape:'rectangle',
    width:50


    })

  }
  private decodeToken(token :string){
    return JSON.parse(atob(token.split(".")[1]))
  }
  handlelogin(response: any) {
    if (response) {
      const payload = this.decodeToken(response.credential);
      const googleObj = new Googlelogin(payload.email); // Adjust based on your Googlelogin model
  
      this.loginService.googlelog(googleObj).subscribe({
        next: (data) => {
          localStorage.setItem('userData', JSON.stringify(data));
          console.log('Login successful:', data);
          if(data.userType=="0"){
            console.log("customer")
           this.router.navigate(['/HomePage']);  // Navigate to the desired route
          }
          else if(data.userType=="1"){
            console.log("company")
           this.router.navigate(['/HomePage']);  // Navigate to the desired route
          }
          else if(data.userType=="2"){
            console.log("admin")
            this.router.navigate(['/dashboard']);  // Navigate to the desired route
           
          }
          
        },
        error: (error) => {
          Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "Email Or Password IS wrong",
          });  
          console.error('Login error:', error);
         
        }
      });
    }
  }
  Loginform :FormGroup=new FormGroup({
    Email :new FormControl(),
    Password : new FormControl()
  })
  constructor(private loginService: LoginService ,private router: Router) {}
  login() {
    const email = this.Loginform.get('Email')?.value;
    const password = this.Loginform.get('Password')?.value;

      this.loginService.login(email, password).subscribe(
        {next:(data)=>{
          localStorage.setItem('userData', JSON.stringify(data));
          localStorage.setItem('token', data.token);
          if(data.userType=="0"){
           this.router.navigate(['']);  // Navigate to the desired route
          }
          else if(data.userType=="1"){
            console.log("company")
           this.router.navigate(['/dashboard']);  // Navigate to the desired route
          }
          else if(data.userType=="2"){
            console.log("admin")
             this.router.navigate(['/admin']);  // Navigate to the desired route
            
          }
        },
        error:(error) => {
          Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "Email Or Password IS Wrong",
          }); 
          console.error('Login error:', error);
        }
      }
     
      );
    
  }

}
