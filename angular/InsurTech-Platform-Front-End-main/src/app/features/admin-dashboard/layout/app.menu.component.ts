import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
  selector: 'app-menu',
  templateUrl: './app.menu.component.html',
})
export class AppMenuComponent implements OnInit {
  model: any[] = [];

  constructor(public layoutService: LayoutService) {}

  ngOnInit() {
        this.model = [
            {
                label: 'Home',
                items: [
                    { label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/admin/dashboard'] }
                ]
            },
            {
                label: 'Company',
                items: [
                    { label: 'Registration Request', icon: 'pi pi-fw pi-user', routerLink: ['/admin/registration-requests'] }
                ]
            },
            {
                label: 'User',
                items: [
                    { label: 'Article', icon: 'pi pi-fw pi-book', routerLink: ['/admin/article'] },
                    { label: 'Details', icon: 'pi pi-fw pi-database', routerLink: ['/admin/crud'] }
                ]
            }
        ];
    }
}
