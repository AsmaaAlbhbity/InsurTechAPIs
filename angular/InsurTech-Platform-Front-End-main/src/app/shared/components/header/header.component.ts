import { CommonModule } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/authantication.service';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  isScrolled = false;
  isLoggedIn = false;
  userData: any = null;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.isLoggedIn().subscribe((status) => {
      this.isLoggedIn = status;
    });
    this.authService.getUserData().subscribe((data) => {
      this.userData = data;
    });

    // Check login status on component initialization
    this.authService.checkLoginStatus();

    // Subscribe to router events to check login status on navigation
    this.router.events.subscribe(() => {
      this.authService.checkLoginStatus();
    });
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    this.isScrolled = window.scrollY > 20;
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
