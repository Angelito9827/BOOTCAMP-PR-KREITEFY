import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth/auth.service';
import { CustomValidators } from '../Validators/custom-validators';
import { UserRegister } from '../model/user-register.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  form!: FormGroup;
  errorMessage: string | null = null;

  constructor(
    private authService: AuthService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.buildForm();
  }

  private buildForm(): void {
    this.form = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      lastName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      email: ['', [
        Validators.required,
        Validators.email,
        Validators.minLength(5),
        Validators.maxLength(100)
      ]],
      password: ['', [Validators.required, CustomValidators.passwordValidator()]],
      rePassword: ['', [Validators.required]],
    }, {
      validators: CustomValidators.passwordsMatchValidator('password', 'rePassword')
    });
  }

  registerUser(): void {
    this.markFormAsTouched();
    if (this.form.valid) {
      const request : UserRegister = {
        email : this.form.controls?.['email'].value,
        lastName : this.form.controls?.['lastName'].value,
        name : this.form.controls?.['name'].value,
        password : this.form.controls?.['password'].value,
        roleId : 2
      }

      this.authService.register(request).subscribe({
        next: () => this.router.navigate(['/']),
        error: (err) => this.handleError(err)
      });
    } else {
      console.error('Invalid form');
    }
  }

  goBack(): void {
    this.router.navigate(['/']);
  }

  private markFormAsTouched(): void {
    Object.values(this.form.controls).forEach(control => control.markAsTouched());
  }

  private handleError(error: any): void {
    
    if (error.status === 400) {
      console.error("This email is already in use.");
      this.errorMessage = 'This email is already in use. Please introduce a diferent email.';
    } else {
      console.error("Unexpected error.");
      this.errorMessage = 'Unexpected error. Please try later.';
    }
  }
}
