import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowAdvertisementComponent } from './show-advertisement.component';

describe('ShowAdvertisementComponent', () => {
  let component: ShowAdvertisementComponent;
  let fixture: ComponentFixture<ShowAdvertisementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowAdvertisementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowAdvertisementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
