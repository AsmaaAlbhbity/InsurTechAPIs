import { Routes } from '@angular/router';
import { RegistrationRequestsComponent } from './components/registration-requests/registration-requests.component';
import { ArticleComponent } from './components/article/article.component';
import { AppLayoutComponent } from './layout/app.layout.component';
import { authAdminGuard } from '../../core/guards/authAdmin.guard';
import { AdminCrudUsersComponent } from './components/admin-crud/admin-crud-users.component';
import { ChartPieDemo } from './components/admin-graphs/admin-graphs.component';
import { QuestionCardComponent } from '../apply-for-insurance-v2/question-card/question-card.component';
import { QuestionsCrudComponent } from './questions-crud/questions-crud.component';

export const adminDashboardRoutes: Routes = [
  { path: '', component: AppLayoutComponent,
        children: [
            { path: 'registration-requests', component: RegistrationRequestsComponent, canActivate: [authAdminGuard]},
            { path: 'article', component: ArticleComponent, canActivate: [authAdminGuard] },
            { path: 'crud', component: AdminCrudUsersComponent, canActivate: [authAdminGuard] },
            {path:'dashboard',component:ChartPieDemo,canActivate:[authAdminGuard]},
            {path:'question-crud',component:QuestionsCrudComponent}
        ]
    },
];
