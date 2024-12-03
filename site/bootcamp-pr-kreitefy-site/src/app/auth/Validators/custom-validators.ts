import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class CustomValidators {

  public static passwordValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const password = control.value;
      if (!password) return null;

      const errors: any = {};

      if (password.length < 8) {
        errors.minLength = 'Password must be at least 8 characters.';
      }
      if (!/[A-Z]/.test(password)) {
        errors.uppercase = 'Password must contain at least one uppercase letter.';
      }
      if (!/[a-z]/.test(password)) {
        errors.lowercase = 'Password must contain at least one lowercase letter.';
      }
      if (!/[0-9]/.test(password)) {
        errors.number = 'Password must contain at least one number.';
      }
      if (!/[!@#$%^&*(),.?":{}|<>_-]/.test(password)) {
        errors.special = 'Password must contain at least one special character.';
      }

      return Object.keys(errors).length > 0 ? errors : null;
    };
  }

  public static passwordsMatchValidator(passwordField: string, confirmPasswordField: string): ValidatorFn {
    return (group: AbstractControl): ValidationErrors | null => {
      const password = group.get(passwordField)?.value;
      const rePassword = group.get(confirmPasswordField)?.value;

      return password === rePassword ? null : { passwordsMismatch: true };
    };
  }
}
