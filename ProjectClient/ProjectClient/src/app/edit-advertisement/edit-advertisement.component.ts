import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Advertisement } from '../models/advertisement';
import { AdvertisementService } from '../services/advertisement.service';

@Component({
  selector: 'app-edit-advertisement',
  templateUrl: './edit-advertisement.component.html',
  styleUrls: ['./edit-advertisement.component.css', '../create-advertisement/create-advertisement.component.css',]
})
export class EditAdvertisementComponent implements OnInit {
  advertisementForm: FormGroup;
  message: String;
  imagesData = [];
  nextImageId: number = 1;
  objectKeys = Object.keys;
  advertisement: Advertisement;

  constructor(private fb: FormBuilder, private advertisementService: AdvertisementService,
     private router: Router,
     private route: ActivatedRoute) {
    this.advertisementForm = this.fb.group({
      'name': ['', [Validators.required]],
      'price': ['', [Validators.required]],
      'description': ['', [Validators.required]],
      'imageString': ['', [Validators.required]],
    });

    this.route.params.subscribe(data => {
      this.advertisementService.getAdvertisement(data['id']).subscribe(advertisement => {
        this.advertisement = advertisement;
        this.advertisementForm.patchValue(advertisement);

        advertisement.images.forEach(imgTxt => {
          this.saveImage(imgTxt);
        });
      })
    })
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

    this.advertisementService.create(val).subscribe(() => {
      this.router.navigate(['advertisements']);
    });
  }

  public preview(files) {
    if (files.length === 0)
      return;

    let mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = "Only images are supported.";
      return;
    }

    const reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = (_event) => {
      this.saveImage(reader.result);
    }
  }

  private saveImage(result){
    this.imagesData[this.nextImageId++] = result;
      console.log(this.imagesData);
  }

  public removeImage(key: number) {
    delete this.imagesData[key];
  }
}
