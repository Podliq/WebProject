import { Component, OnInit } from '@angular/core';
import { Advertisement } from '../models/advertisement';
import { AdvertisementService } from '../services/advertisement.service';

@Component({
  selector: 'app-list-advertisements',
  templateUrl: './list-advertisements.component.html',
  styleUrls: ['./list-advertisements.component.css']
})
export class ListAdvertisementsComponent implements OnInit {
  advertisements;
  constructor(private advertisementService: AdvertisementService) { }

  ngOnInit(): void {
    this.getAdvertisements();
  }

  getAdvertisements(){
    this.advertisementService.getAdvertisements().subscribe(advertisements => {
      this.advertisements = advertisements.sort((a, b) => (a.id > b.id) ? -1 : 1);
    });
  }

  deleteAdvertisement(id) {
    this.advertisementService.delete(id).subscribe(result => {
      this.getAdvertisements();
    });
  }
}
