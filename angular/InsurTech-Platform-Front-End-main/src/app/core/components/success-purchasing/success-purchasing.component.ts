import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HeaderComponent } from '../../../shared/components/header/header.component';
import { FooterComponent } from '../../../shared/components/footer/footer.component';
import { ButtonModule } from 'primeng/button';
import { RouterLink } from '@angular/router';
import { PanelModule } from 'primeng/panel';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { RatingModule } from 'ngx-bootstrap/rating'; // Import RatingModule from ngx-bootstrap
import { DividerModule } from 'primeng/divider';
import { HealthinsuranceService } from '../../../features/company-dashboard/Services/ManageHealthServices/healthinsurance.service';
import { MotorinsuranceService } from '../../../features/company-dashboard/Services/ManageMotorServices/motorinsurance.service';
import { HomeinsuranceService } from '../../../features/company-dashboard/Services/ManageHomeServices/homeinsurance.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-success-purchasing',
  standalone: true,
  imports: [
    PanelModule,
    FormsModule, // Import FormsModule
    RatingModule, // Import RatingModule from ngx-bootstrap
    HeaderComponent,
    FooterComponent,
    ButtonModule,
    RouterLink,
    ButtonModule,
  ],
  templateUrl: './success-purchasing.component.html',
  styleUrls: ['./success-purchasing.component.css'],
})
export class SuccessPurchasingComponent implements OnInit {
  id: any;
  catid: any;
  plan: any;

  constructor(
    private route: ActivatedRoute,
    public health: HealthinsuranceService,
    public motor: MotorinsuranceService,
    public home: HomeinsuranceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.id = params['id'];
      this.catid = params['catId'];
      console.log(this.id, this.catid);
    });

    if (this.catid == 1) {
      this.health.getById(this.id).subscribe({
        next: (data) => {
          this.plan = data;
          console.log('Health insurance fetched successfully', data);
        },
        error: (error) => {
          console.error('Error fetching health insurance', error);
        },
      });
    } else if (this.catid == 2) {
      this.home.getById(this.id).subscribe({
        next: (data) => {
          this.plan = data;
          console.log('Home insurance fetched successfully', data);
        },
        error: (error) => {
          console.error('Error fetching home insurance', error);
        },
      });
    } else if (this.catid == 3) {
      this.motor.getById(this.id).subscribe({
        next: (data) => {
          this.plan = data;
          console.log('Motor insurance fetched successfully', data);
        },
        error: (error) => {
          console.error('Error fetching motor insurance', error);
        },
      });
    }
  }
  navigateToHome() {
    this.router.navigate(['/']);
  }

  getShareText(): string {
    if (!this.plan) return '';

    const commonDetails = `
      🏷️ *Category:* ${this.plan.category}\n
      📊 *Level:* ${this.plan.level}\n
      💵 *Yearly Coverage:* EGP ${this.plan.yearlyCoverage}\n
      📄 *More details at:* ${window.location.href}\n\n
      *Thank you for choosing InsurTech!*
    `;

    let additionalDetails = '';

    if (this.catid == 1) {
      additionalDetails = `
        🔹 ${this.plan.waterDamage ? 'Water Damage: Yes' : 'Water Damage: No'}\n
        🔹 ${this.plan.quotation ? 'Quotation: Yes' : 'Quotation: No'}\n
        🔹 ${
          this.plan.glassBreakage ? 'Glass Breakage: Yes' : 'Glass Breakage: No'
        }\n
        🔹 ${
          this.plan.naturalHazard ? 'Natural Hazard: Yes' : 'Natural Hazard: No'
        }\n
        🔹 ${
          this.plan.attemptedTheft
            ? 'Attempted Theft: Yes'
            : 'Attempted Theft: No'
        }\n
        🔹 ${
          this.plan.firesAndExplosion
            ? 'Fires and Explosion: Yes'
            : 'Fires and Explosion: No'
        }\n
      `;
    } else {
      additionalDetails = `
        🔹 ${
          this.plan.legalExpenses ? 'Legal Expenses: Yes' : 'Legal Expenses: No'
        }\n
        🔹 ${this.plan.quotation ? 'Quotation: Yes' : 'Quotation: No'}\n
        🔹 ${this.plan.ownDamage ? 'Own Damage: Yes' : 'Own Damage: No'}\n
        🔹 ${
          this.plan.personalAccident
            ? 'Personal Accident: Yes'
            : 'Personal Accident: No'
        }\n
        🔹 ${this.plan.theft ? 'Theft: Yes' : 'Theft: No'}\n
        🔹 ${
          this.plan.thirdPartyLiability
            ? 'Third Party Liability: Yes'
            : 'Third Party Liability: No'
        }\n
      `;
    }

    return `*Check out this insurance plan:*\n\n${additionalDetails}${commonDetails}`;
  }

  shareOnFacebook(): void {
    const text = this.getShareText();
    if (!text) return;

    const encodedText = encodeURIComponent(text);
    const dummyUrl = 'https://example.com';
    const shareaholicUrl = `https://www.shareaholic.com/api/share/?v=1&apitype=1&apikey=8943b7fd64cd8b1770ff5affa9a9437b&service=facebook&link=${dummyUrl}&notes=${encodedText}`;

    window.open(shareaholicUrl, '_blank');
  }

  shareOnTelegram(): void {
    const text = this.getShareText();
    if (!text) return;

    const encodedText = encodeURIComponent(text);
    const telegramUrl = `https://t.me/share/url?url=${encodeURIComponent(
      window.location.href
    )}&text=${encodedText}`;

    window.open(telegramUrl, '_blank');
  }

  shareOnTwitter(): void {
    const text = this.getShareText();
    if (!text) return;

    const encodedText = encodeURIComponent(text);
    const twitterUrl = `https://twitter.com/intent/tweet?text=${encodedText}&url=${encodeURIComponent(
      window.location.href
    )}`;

    window.open(twitterUrl, '_blank');
  }

  shareOnLinkedIn(): void {
    const text = this.getShareText();
    if (!text) return;

    const encodedText = encodeURIComponent(text);
    const linkedInUrl = `https://www.linkedin.com/sharing/share-offsite/?text=${encodedText}`;

    window.open(linkedInUrl, '_blank');
  }

  shareOnWhatsApp(): void {
    const text = this.getShareText();
    if (!text) return;

    const encodedText = encodeURIComponent(text);
    const whatsappUrl = `https://api.whatsapp.com/send?text=${encodedText}`;

    window.open(whatsappUrl, '_blank');
  }
}
