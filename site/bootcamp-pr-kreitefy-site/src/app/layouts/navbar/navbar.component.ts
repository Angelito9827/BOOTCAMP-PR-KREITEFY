import { Component } from '@angular/core';
import { AuthService } from '../../auth/service/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  isLoggedIn: boolean = false;
  userName: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.authService.getLoggedInStatus().subscribe(status => {
      this.isLoggedIn = status;
    });

    this.authService.getUserNameObservable().subscribe(name => {
      this.userName = name;
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  handleLoginLogout(): void {
    if (this.isLoggedIn) {
      this.logout(); 
    } else {
      this.router.navigate(['/login']);
    }
  }

}
