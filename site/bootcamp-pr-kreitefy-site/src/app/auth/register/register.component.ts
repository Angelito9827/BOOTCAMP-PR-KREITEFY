import { Component } from '@angular/core';
import { UserDto } from '../model/user.model';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../service/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  form!: FormGroup;
  errorMessage: string | null = null;

  constructor(
    private userService: UserService,
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
      roleName: ''
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
    this.userService.insertUser(userToSave).subscribe({
      next: (userInserted) => {
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
      password: ['', [Validators.required, this.passwordValidator()]],
      rePassword: ['', [Validators.required]],
    },
    { validators: this.passwordsMatchValidator('password', 'rePassword') }
  )
  }

  goBack(): void {
    this.router.navigate(['/'])
  }

  public passwordValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const password = control.value;
      if (!password) return null;
  
      const errors: any = {};
  
      if (password.length < 8) {
        errors.minLength = ' Password must be at least 8 characters. ';
      }
      if (!/[A-Z]/.test(password)) {
        errors.uppercase = ' Password must contain at least one uppercase letter. ';
      }
      if (!/[a-z]/.test(password)) {
        errors.lowercase = ' Password must contain at least one lowercase letter. ';
      }
      if (!/[0-9]/.test(password)) {
        errors.number = ' Password must contain at least one number. ';
      }
      if (!/[!@#$%^&*(),.?":{}|<>_-]/.test(password)) {
        errors.special = ' Password must contain at least one special character. ';
      }
  
      return Object.keys(errors).length > 0 ? errors : null;
    };
  }

  public passwordsMatchValidator(passwordField: string, confirmPasswordField: string): ValidatorFn {
    return (group: AbstractControl): ValidationErrors | null => {
      const password = group.get(passwordField)?.value;
      const rePassword = group.get(confirmPasswordField)?.value;

      return password === rePassword ? null : { passwordsMismatch: true };
    };
  }

  private markFormAsTouched(): void {
    Object.values(this.form.controls).forEach(control => control.markAsTouched());
  }

  public handleError(error: any): void {
    console.log(error);
    this.errorMessage = "An unexpected error occurred. Please try again.";
  }
}
