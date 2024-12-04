import { Component } from '@angular/core';
import { AuthService } from '../auth/service/auth/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  isLoggedIn: boolean = false;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.getAuthStatus().subscribe(status => {
      this.isLoggedIn = status;
    });
  }
}
