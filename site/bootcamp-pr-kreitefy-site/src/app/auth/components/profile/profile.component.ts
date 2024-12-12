import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../service/auth/auth.service';
import { UserDto } from '../../model/user.model';
import { CustomValidators } from '../../validators/custom-validators';
import { Router } from '@angular/router';
import { HistoryProfileDto, PaginatedResponse } from '../../model/history-profile.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  form!: FormGroup;
  userId: string = '';
  user!: UserDto;
  errorMessage: string | null = null;

  currentPage: number = 1;
  totalPages: number = 0;
  pageSize: number = 9;
  totalCount: number = 0;
  items: HistoryProfileDto [] = [];

  constructor(
    private authService: AuthService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {this.buildForm();}

  ngOnInit(): void {
    this.authService.getUserIdObservable().subscribe(id => {
      this.userId = id;
    });
    this.getUserById();
    this.getHistoryById();
  }

  private buildForm(): void {
    this.form = this.formBuilder.group({
      id: [{value:undefined, disabled:true}],
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      lastName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      email: ['', [
        Validators.required,
        Validators.email,
        Validators.minLength(5),
        Validators.maxLength(100)
      ]],
      roleId: [''],
      roleName: [''],
      password: ['', [Validators.required, CustomValidators.passwordValidator()]],
      rePassword: ['', [Validators.required]],
    }, {
      validators: CustomValidators.passwordsMatchValidator('password', 'rePassword')
    });
  }

  private getHistoryById(): void {
    this.authService.getHistory(this.userId, this.currentPage, this.pageSize).subscribe({
      next:(response: PaginatedResponse<HistoryProfileDto>) => {
        this.items = response.items;
        this.currentPage = response.currentPage;
        this.totalPages = response.totalPages;
        this.pageSize = response.pageSize;
        this.totalCount = response.totalCount;
      },
        error:(err) => {
          console.error("Error getting history")
        }
    }) 
  }

  private getUserById(): void {
    this.authService.getUserById(this.userId).subscribe({
      next:(response) => {
        this.user = response;
        this.form.patchValue({
          id: this.user.id,
          name: this.user.name,
          lastName: this.user.lastName,
          email: this.user.email,
          password: '',
          roleId: this.user.roleId,
          roleNamem: this.user.roleName
        })
      },
        error:(err) => {
          console.error("Error getting user")
        }
    }) 
  }

  updateUser(): void {
    this.markFormAsTouched();
    if (this.form.valid) {
      const request : UserDto = {
        id : this.form.controls?.['id'].value,
        email : this.form.controls?.['email'].value,
        lastName : this.form.controls?.['lastName'].value,
        name : this.form.controls?.['name'].value,
        password : this.form.controls?.['password'].value,
        roleId : 2,
        roleName : this.form.controls?.['roleName'].value
      }

      this.authService.putUserById(request).subscribe({
        next: () => console.log(request),
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

  public nextPage(): void {
    this.currentPage += 1;
    this.getHistoryById();
  }

  public previousPage(): void {
    this.currentPage -= 1;
    this.getHistoryById(); 
  }
}
