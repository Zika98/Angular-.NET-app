import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model: any = {}
loggedIn: boolean;

  constructor(private accountServie: AccountService) { }

  ngOnInit(): void {
  }

  login()
  {
    this.accountServie.login(this.model).subscribe(response => {
      console.log(response);
      this.loggedIn = true;
    }, error => {
      console.log(error);
    })
  }

  logout()
  {
    this.accountServie.logout();
    this.loggedIn = false;
  }

  //ako je null, !! vraca false, a ako ima nes u useru bice sve ok i vraca true
  getCurrentUser()
  {
    this.accountServie.currentUser$.subscribe(user => {
      this.loggedIn = !!user;
    }, error =>
    {
      console.log(error);
    });
    
  }


}
