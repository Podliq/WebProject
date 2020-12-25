import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserDetails } from '../models/userDetails';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css'],
})
export class MyProfileComponent implements OnInit {
  public details: UserDetails;
  public userDetailsForm: FormGroup;
  public changePasswordForm: FormGroup;
  public message: string;

  constructor(private authService: AuthService, private fb: FormBuilder) {
    this.userDetailsForm = this.fb.group({
      email: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
    });

    this.changePasswordForm = this.fb.group({
      oldPassword: ['', [Validators.required]],
      newPassword: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.authService.getDetails().subscribe((data) => {
      this.details = data;
      this.details.profilePicture = this.details?.profilePicture ? this.details.profilePicture : '../../assets/default-user-image.png';
      this.userDetailsForm.patchValue(data);
    });
  }

  public changePassword(): void {}

  public saveUserDetails(): void {
    this.details = this.userDetailsForm.value;
    this.details.profilePicture = this.details.profilePicture;
    this.authService.setDetails(this.details).subscribe((data) => {});
  }

  public preview(files) {
    if (files.length === 0) return;

    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = 'Only images are supported.';
      return;
    }

    var reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = (_event) => {
      this.details.profilePicture = reader.result.toString();
    };
  }
}
