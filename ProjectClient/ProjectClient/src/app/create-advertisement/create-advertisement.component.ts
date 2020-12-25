import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdvertisementService } from '../services/advertisement.service';

@Component({
  selector: 'app-create-advertisement',
  templateUrl: './create-advertisement.component.html',
  styleUrls: ['./create-advertisement.component.css']
})
export class CreateAdvertisementComponent implements OnInit {
  advertisementForm: FormGroup;
  message: String;
  imagesData = [];
  nextImageId: number = 1;
  objectKeys = Object.keys;

  constructor(private fb: FormBuilder, private advertisementService: AdvertisementService, private router: Router) {
    this.advertisementForm = this.fb.group({
      'name': ['', [Validators.required]],
      'price': ['', [Validators.required]],
      'description': ['', [Validators.required]],
      'imageString': ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
  }

  public get name() {
    return this.advertisementForm.get('name');
  }

  public get price() {
    return this.advertisementForm.get('price');
  }

  public get description() {
    return this.advertisementForm.get('description');
  }

  public create() {
    let val = this.advertisementForm.value;
    val['images'] = this.imagesData.filter(function (el) {
      return el != null;
    });

    this.advertisementService.create(val).subscribe(data => {
      this.router.navigate(['advertisements']);
      console.log(data);
    });
  }

  public preview(files) {
    if (files.length === 0)
      return;

    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = "Only images are supported.";
      return;
    }

    var reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = (_event) => {
      this.imagesData[this.nextImageId++] = reader.result;
      console.log(this.imagesData);
    }
  }

  public removeImage(key: number) {
    console.log(key);
    delete this.imagesData[key];
  }
}
