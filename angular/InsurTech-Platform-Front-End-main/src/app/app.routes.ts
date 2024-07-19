import { Routes } from '@angular/router';
import { companydashbordcomponant } from './features/company-dashboard/company-dashboard';
import { EditHealthInsurancePlanComponent } from './features/company-dashboard/Componants/Helathinsurance/edithealthinsurance/edithealthinsurance.component';
import { EdithomeinsuranceComponent } from './features/company-dashboard/Componants/Homeinsurance/edithomeinsurance/edithomeinsurance.component';
import { EditmotorinsuranceComponent } from './features/company-dashboard/Componants/Motorinsurance/editmotorinsurance/editmotorinsurance.component';
import { RegisterComponent } from './features/register/register.component';
import { RegisterUserComponent } from './features/register-user/register-user.component';
import { ApplyForInsuranceComponent } from './features/apply-for-insurance/apply-for-insurance.component';
import { DisplayComponent } from './features/categories/display/display.component';
import { SubCategoryHealthComponent } from './features/categories/sub-Healthcategory/sub-category.component';
import { SubCategoryMotorComponent } from './features/categories/sub-Motorcategory/sub-category.component';
import { AppComponent } from './app.component';
import { LoginComponent } from './features/Account/login/login.component';
import { ForgetPasswordComponent } from './features/Account/forget-password/forget-password.component';
import { ResetpasswordComponent } from './features/Account/resetpassword/resetpassword.component';
import { ApplyForInsuranceV2Component } from './features/apply-for-insurance-v2/apply-for-insurance-v2.component';
import { QuestionsListComponent } from './features/apply-for-insurance-v2/questions-list/questions-list.component';
import { authAdminGuard } from './core/guards/authAdmin.guard';
import { HomeComponent } from './features/home/home.component';
import { CompanyInsurancePlanComponent } from './features/company/company-insurance-plan/company-insurance-plan.component';
import { SubCategoryHomeComponent } from './features/categories/sub-Homecategory/sub-category.component';
import { CompanyRequestComponent } from './features/company-request/Component/requests/company-request.component';
import { InsurancecollectionComponent } from './features/company-dashboard/Componants/insurancecollection/insurancecollection.component';
import { HomeinsuranceComponent } from './features/company-dashboard/Componants/Homeinsurance/homeinsurance/homeinsurance.component';
import { HealthinsuranceComponent } from './features/company-dashboard/Componants/Helathinsurance/healthinsurance/healthinsurance.component';
import { MotorinsuranceComponent } from './features/company-dashboard/Componants/Motorinsurance/motorinsurance/motorinsurance.component';
import { InsurancePlansComponent } from './features/apply-for-insurance-v2/insurance-plans/insurance-plans.component';
import { authUserGuard } from './core/guards/authUser.guard';
import { SuccessPurchasingComponent } from './core/components/success-purchasing/success-purchasing.component';
import { PageNotFoundComponent } from './core/components/page-not-found/page-not-found/page-not-found.component';
import { SuccessPageComponent } from './features/apply-for-insurance-v2/success-page/success-page.component';
import { InsurancePlanCardComponent } from './features/apply-for-insurance-v2/insurance-plan-card/insurance-plan-card.component';
import { authCompanyGuard } from './core/guards/authCompany.guard';
import { FAQComponent } from './features/faq/faq.component';
import { AllarticlesComponent } from './features/allarticles/allarticles.component';
import { HomearticlesComponent } from './features/homearticles/homearticles.component';
import { PaymentPageComponent } from './features/paymen-gateaway/payment-page/payment-page.component';
import { PaymentSelectionComponent } from './features/paymen-gateaway/payment-selection/payment-selection.component';
import { ElementTestComponent } from './features/element-test/element-test.component';

import { HomeDetailsComponent } from './features/home-details/home-details.component';
import { HealthDetailsComponent } from './features/health-details/health-details.component';
import { MotorDetailsComponent } from './features/motor-details/motor-details.component';
import { UserDashboardComponent } from './features/userProfile/user-dashboard/user-dashboard.component';
import { ProfileComponent } from './features/userProfile/profile/profile.component';
import { BillingComponent } from './features/userProfile/billing/billing.component';
import { PlansComponent } from './features/userProfile/plans/plans.component';


import { UserFeedBackComponent } from './features/user-feed-back/user-feed-back.component';
import { ComparingInsuranceComponent } from './features/comparing-insurance/comparing-insurance.component';
export const routes: Routes = [
  {
    path: 'admin',
    loadChildren: () =>
      import('./features/admin-dashboard/layout/app.layout.module').then(
        (m) => m.AppLayoutModule
      ),
    canActivate: [authAdminGuard],
  },

  { path: 'apply-for-insurance', component: ApplyForInsuranceComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'register-user', component: RegisterUserComponent },
  { path: 'login', component: LoginComponent },
  { path: 'login/:authorization', component: LoginComponent },
  { path: 'forgetpassword', component: ForgetPasswordComponent },
  { path: 'resetpassword', component: ResetpasswordComponent },
  { path: 'allarticles', component: AllarticlesComponent },
  { path: 'homearticle', component: HomearticlesComponent },
  {
    path: 'insurance',
    component: AppComponent,
    children: [
      { path: '', component: DisplayComponent },
      { path: 'health', component: SubCategoryHealthComponent },
      { path: 'home', component: SubCategoryHomeComponent },
      { path: 'motor', component: SubCategoryMotorComponent },
    ],
  },
  {
    path: 'companyinsurance',
    component: CompanyInsurancePlanComponent,
    children: [{ path: '', component: CompanyInsurancePlanComponent }],
  },
  {
    path: 'faqs',
    component: FAQComponent,
    children: [{ path: '', component: FAQComponent }],
  },
  {
    path: 'company',
    component: companydashbordcomponant,
    canActivate: [authCompanyGuard],
  },
  {
    path:'homedetails',
    component:HomeDetailsComponent
  },
  {
    path:'healthdetails',
    component:HealthDetailsComponent
  },
  {
    path:'motordetails',
    component:MotorDetailsComponent
  },
  {
    path: 'edithealthinsurance/:id',
    component: EditHealthInsurancePlanComponent,
  },
  { path: 'edithomeinsurance/:id', component: EdithomeinsuranceComponent },
  { path: 'editmotorinsurance/:id', component: EditmotorinsuranceComponent },
  { path: 'HomePage', component: HomeComponent },
  {
    path: 'apply-for-insuranceV2',
    component: ApplyForInsuranceV2Component,
    canActivate: [authUserGuard],
  },
  {
    path: 'questions-list/:id',
    component: QuestionsListComponent,
    canActivate: [authUserGuard],
  },
  {
    path: 'insurancePlanCard',
    component: InsurancePlanCardComponent,
    canActivate: [authUserGuard],
  },
  {
    path: 'insurancePlans',
    component: InsurancePlansComponent,
    canActivate: [authUserGuard],
  },
  {
    path: 'success',
    component: SuccessPageComponent,
  },
  {
    path: 'request',
    component: CompanyRequestComponent,
  },
  {
    path: 'dashboard',
    redirectTo: 'company',
  },
  {
    path: 'insurancecollection',
    component: InsurancecollectionComponent,
    children: [
      {
        path: 'homeinsurance', component: HomeinsuranceComponent
      },
      {
        path: 'healthinsurance',
        component: HealthinsuranceComponent,
      },
      {
        path: 'motorinsurance',
        component: MotorinsuranceComponent,
      },
    ],
  },
  { path: 'payment', component: PaymentPageComponent },

  {
    path: 'successpurchasing/:id/:catId',
    component: SuccessPurchasingComponent,
  },
  { path: 'payment', component: PaymentPageComponent },
  { path: 'payment-selection', component: PaymentSelectionComponent },
  // { path: 'payments', component: PaymentComponent },
    {
    path: 'userProfile',
    component: UserDashboardComponent,
    children: [
      { path: 'profile', component: ProfileComponent },
      { path: 'billing', component: BillingComponent },
      { path: 'plans', component: PlansComponent },
      // { path: '', redirectTo: '/profile', pathMatch: 'full' }, // Default route
    ],
  },

  { path: '404', component: PageNotFoundComponent },
  { path: 'test', component: ElementTestComponent },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  {
    path: 'feedback',
    component: UserFeedBackComponent,
    canActivate: [authUserGuard],
  },
  { path: 'compare', component: ComparingInsuranceComponent },
  { path: '**', redirectTo: '/404' },

];
