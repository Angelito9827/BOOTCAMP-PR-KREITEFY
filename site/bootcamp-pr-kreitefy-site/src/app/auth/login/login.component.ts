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

  private initializeUser(): {email:string; password:string}{
    const{email, password} = this.form?.value;
    return {  
      email,   
      password,
    };
  }

  insertSesion(credentials: {email:string; password:string}): void {
    this.authService.login(credentials).subscribe({
      next:(response) => {
        const token = response.token;
        this.authService.saveToken(token);
        this.router.navigate(['/']);
      },
      error:(err) => {this.handleError(err)}
    })
  }

  public loginUser(): void {
    this.markFormAsTouched();
    if (this.form.valid) {
      const credentials = this.initializeUser();
      this.insertSesion(credentials);
    } else {
      console.error("Invalid form")
    }
  }

  goBack(): void {
    this.router.navigate(['/'])
  }

  private markFormAsTouched(): void {
    Object.values(this.form.controls).forEach(control => control.markAsTouched());
  }

  private handleError(error: any): void {
    console.error(error);
    this.errorMessage = error.status === 401
    ? "Incorrect credentials. Please verify your email and password."
    : "An unexpected error occurred. Please try again.";    
  }
}
