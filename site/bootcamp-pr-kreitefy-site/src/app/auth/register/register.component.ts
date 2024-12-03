import { Component } from '@angular/core';
import { UserDto } from '../model/user.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth/auth.service';
import { CustomValidators } from '../Validators/custom-validators';

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
  ) {this.buildForm()}

  private initializeUser(): UserDto{
    const{name, lastName, email, password} = this.form?.value;
    return {
      id: undefined,       
      name,   
      lastName, 
      email,   
      password,
      roleId: 2,
      roleName: '',
      token: ''
    };
  }

  public saveUser(): void {
    this.markFormAsTouched();
    if (this.form.valid) {
      this.insertUser(this.initializeUser())
    } else {
      console.error("invalid form.")
    }
  }

  insertUser(userToSave: UserDto): void {
    this.authService.register(userToSave).subscribe({
      next: (userInserted) => {
        this.authService.saveToken(userInserted.token);
        localStorage.setItem('userName', userInserted.name);
        console.log("Created successfully");
        console.log(userInserted);
        this.router.navigate(['/']);
      },
      error: (err) => {this.handleError(err);}
    })
  }

  public buildForm(): void {
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
    },
    { validators: CustomValidators.passwordsMatchValidator('password', 'rePassword') }
  )
  }

  goBack(): void {
    this.router.navigate(['/'])
  }

  private markFormAsTouched(): void {
    Object.values(this.form.controls).forEach(control => control.markAsTouched());
  }

  public handleError(error: any): void {
    console.log(error);
    if (error.status === 400) {
      this.errorMessage = "This email is alredy in use"
    } else {
      this.errorMessage = "An unexpected error occurred. Please try again.";
    }
  }
}
