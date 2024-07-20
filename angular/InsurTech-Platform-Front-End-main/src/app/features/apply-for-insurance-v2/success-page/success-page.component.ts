import { Component, OnInit } from '@angular/core';
import { Fireworks } from 'fireworks-js';


@Component({
  selector: 'app-success-page',
  templateUrl: './success-page.component.html',
  styleUrls: ['./success-page.component.css']
})
export class SuccessPageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    this.startFireworks();
  }

  startFireworks(): void {
    const fireworkContainer = document.querySelector('.fireworks-container') as HTMLElement;

    const fireworks = new Fireworks(fireworkContainer, {
      // speed: 2, // Reduced speed
      acceleration: 1.02, 
      friction: 0.97, 
      gravity: 1.5, 
      particles: 100, 
      explosion: 5 
    });

    fireworks.start();
  }

  
}
