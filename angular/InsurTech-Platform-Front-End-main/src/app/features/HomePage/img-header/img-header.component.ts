import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import {  GalleriaModule } from 'primeng/galleria';
import $ from 'jquery';

@Component({
  selector: 'app-img-header',
  standalone: true,
  imports: [ButtonModule, GalleriaModule],
  templateUrl: './img-header.component.html',
  styleUrl: './img-header.component.css'
})
export class ImgHeaderComponent  {
  images: any[] | undefined =["../../../../assets/images/CarInsuranceHomePage.jpg","../../../../assets/images/HomeInsuranceHomePage.jpg","../../../../assets/images/MotorInsuranceHomePage.jpg"]
  

  responsiveOptions: any[] = [
      {
          breakpoint: '1024px',
          numVisible: 5
      },
      {
          breakpoint: '768px',
          numVisible: 3
      },
      {
          breakpoint: '560px',
          numVisible: 1
      }
  ];


  getscrolly(id:string){
   $('html,body').scrollTop($(`#${id}`).offset()?.top as number)
  }
 
}
