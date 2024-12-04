import { Component } from '@angular/core';
import { AuthService } from '../../auth/service/auth/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  isLoggedIn: boolean = false;
  userName: string = '';
  private loginStatusSubscription!: Subscription;
  private userNameSubscription!: Subscription;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.loginStatusSubscription = this.authService.getAuthStatus().subscribe(status => {
      this.isLoggedIn = status;
    });

    this.userNameSubscription = this.authService.getUserNameObservable().subscribe(name => {
      this.userName = name;
    });
  }

  ngOnDestroy(): void {
    if (this.loginStatusSubscription) {
      this.loginStatusSubscription.unsubscribe();
    }
    if (this.userNameSubscription) {
      this.userNameSubscription.unsubscribe();
    }
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
