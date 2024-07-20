import { Component, OnDestroy, OnInit } from '@angular/core';
import { CompanyUsers } from '../../Model/company/CompanyUser';
import { CompanyService } from '../../Services/CompanyServices/company.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-users-company',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './users-company.component.html',
  styleUrls: ['./users-company.component.css']
})
export class UsersCompanyComponent implements OnInit, OnDestroy {

  public UsersList: CompanyUsers[] = [];
  Id = JSON.parse(localStorage.getItem('userData') || '{id: 1}').id;
  searchQuery = '';
  filteredUsersList = this.UsersList;
  sub: Subscription = new Subscription();



  constructor(public companyService: CompanyService) { }

  ngOnInit(): void {
    this.companyService.GetCompanyUsers(this.Id).subscribe(
      {
        next: (data) => {
          this.UsersList = data;
          this.filterUsers();

        }
      }
    );
  }

  filterUsers() {
    const query = this.searchQuery.toLowerCase().trim();

    if (query === '') {
      this.filteredUsersList = this.UsersList;
      console.log("q",this.filteredUsersList);
      console.log("q",this.UsersList);

      
    } else {
      this.filteredUsersList = this.UsersList.filter(user =>
        user.name.toLowerCase().includes(query) ||
        user.email.toLowerCase().includes(query) ||
        user.phone.toLowerCase().includes(query)
      );
      console.log("Aq",this.filteredUsersList);

    }
  }

  DownloadUserData(Id: string) {
    console.log(this.UsersList);
    
    this.sub = this.companyService.GetUserArchive(Id).subscribe(res=>
      {
        let blob:Blob=res.body as Blob;
        let url=window.URL.createObjectURL(blob);
        let a=document.createElement('a');
        a.download="userdata";
        a.href=url;
        a.click();

      }
    );
  }

  PrintUserData(Id: string) {
    console.log(this.UsersList);
    
    this.sub = this.companyService.GetUserArchive(Id).subscribe(res=>
      {
        let blob:Blob=res.body as Blob;
        let url=window.URL.createObjectURL(blob);
        window.open(url);

      }
    );
  }
  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
