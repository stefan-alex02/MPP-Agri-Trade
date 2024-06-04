import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgIf} from "@angular/common";
import {MatFormField, MatFormFieldModule} from "@angular/material/form-field";
import {MatOption} from "@angular/material/autocomplete";
import {MatSelect} from "@angular/material/select";
import {MatInput} from "@angular/material/input";
import {MatButton} from "@angular/material/button";
import {MatExpansionPanel, MatExpansionPanelHeader} from "@angular/material/expansion";
import {FlexLayoutModule} from "@angular/flex-layout";
import {Router, RouterLink} from "@angular/router";
import {AuthService} from "../../../services/auth.service";
import {UserDetails} from "../../../models/user-details";
import {Address} from "../../../models/address";
import {
  MatDatepicker,
  MatDatepickerInput,
  MatDatepickerModule,
  MatDatepickerToggle
} from "@angular/material/datepicker";
import {MatNativeDateModule} from "@angular/material/core";

@Component({
  selector: 'app-register-account',
  templateUrl: './register-account.component.html',
  standalone: true,
  imports: [
    MatFormFieldModule,
    ReactiveFormsModule,
    NgIf,
    MatFormField,
    MatOption,
    MatSelect,
    MatInput,
    MatButton,
    MatExpansionPanelHeader,
    MatExpansionPanel,
    FlexLayoutModule,
    FlexLayoutModule,
    MatDatepickerToggle,
    MatDatepicker,
    MatDatepickerInput,
    MatDatepickerModule,
    MatNativeDateModule,
    RouterLink,
  ],
  styleUrls: ['./register-account.component.css']
})
export class RegisterAccountComponent implements OnInit {
  registerForm: FormGroup = null!;
  protected errorMessage: any;

  constructor(private formBuilder: FormBuilder,
              private authService: AuthService,
              private router: Router) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      dob: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(2)]],
      confirmPassword: ['', Validators.required],
      address: this.formBuilder.group({
        number: [''],
        street: [''],
        city: [''],
        county: [''],
        zipCode: ['', Validators.pattern('^[0-9]*$')]
      }),
      userType: ['', Validators.required]
    }, {
      validator: this.mustMatch('password', 'confirmPassword')
    });
  }

  mustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors['mustMatch']) {
        return;
      }

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }

  onSubmit() {
    if (this.registerForm.invalid) {
      return;
    }

    const formValue = this.registerForm.value;

    console.log(formValue.userType);

    // Create an Address object from the form values
    const address : Address | null =
      formValue.address.number && formValue.address.street && formValue.address.city &&
      formValue.address.county && formValue.address.zipCode ? {
        id: null,
        number: +formValue.address.number,
        street: formValue.address.street,
        city: formValue.address.city,
        county: formValue.address.county,
        zipCode: +formValue.address.zipCode
    } : null;

    let dateValue = formValue.dob

    let dobString : string | null = null;
    if (isNaN(new Date(dateValue).getTime())) {
      // The value is not a valid date
      console.error('Invalid date value:', dateValue);
    } else {
      // The value is a valid date
      const dob = new Date(formValue.dob);
      let dobUTC = new Date(Date.UTC(dob.getFullYear(), dob.getMonth(), dob.getDate()));
      const dobString = dobUTC.toISOString().split('T')[0];
    }


    // Create a UserDetails object from the form values
    const userDetails: UserDetails = {
      username: formValue.username,
      email: formValue.email,
      dob: dobString,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      password: formValue.password,
      address: address,
      userType: +formValue.userType
    };

    console.log(userDetails);
    this.authService.registerAccount(userDetails).subscribe({
        next: () => {
          this.router.navigate(['/login']);
        },
        error: (error) => {
          this.errorMessage = error.error;
        }
      }
    );
  }
}
