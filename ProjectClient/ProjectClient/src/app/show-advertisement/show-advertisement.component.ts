import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { from } from 'rxjs';
import { AdvertisementService } from '../services/advertisement.service';
import { Advertisement } from '../models/advertisement';

@Component({
  selector: 'app-show-advertisement',
  templateUrl: './show-advertisement.component.html',
  styleUrls: ['./show-advertisement.component.css']
})
export class ShowAdvertisementComponent implements OnInit {
  advertisement : Advertisement;
  constructor(private route: ActivatedRoute, private advertisementService: AdvertisementService) {
   }

  ngOnInit(): void {
    this.route.params.subscribe(routeParams => {
      this.advertisementService.getAdvertisement(routeParams['id']).subscribe(advertisement => {
        this.advertisement = advertisement;
      });
    });
  }
}
