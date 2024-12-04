import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomValidators } from '../Validators/custom-validators';
import { AuthService } from '../service/auth/auth.service';
import { response } from 'express';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  form!: FormGroup;
  errorMessage: string | null = null;

  constructor(
    private authService: AuthService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {this.buildForm()}

  public buildForm(): void {
    this.form = this.formBuilder.group({
      email: ['', [
        Validators.required,
        Validators.email,
        Validators.minLength(5),
        Validators.maxLength(100)
      ]],
      password: ['', [Validators.required, CustomValidators.passwordValidator()]],
    },
  )
  }

  loginUser(): void {
    this.markFormAsTouched();
    if (this.form.valid) {
      this.authService.login(this.form.value).subscribe({
        next: () => this.router.navigate(['/']),
        error: (err) => this.handleError(err)
      });
    } else {
      console.error('Invalid form');
    }
  }

  goBack(): void {
    this.router.navigate(['/'])
  }

  private markFormAsTouched(): void {
    Object.values(this.form.controls).forEach(control => control.markAsTouched());
  }

  private handleError(error: any): void {
    
    if (error.status === 401) {
      console.error("Invalid email or password.");
      this.errorMessage = 'Invalid email or password. Please try again.';
    } else {
      console.error("Unexpected error.");
      this.errorMessage = 'Unexpected error. Please try later.';
    }
  }
}
