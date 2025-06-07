import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmptyBookinglistComponent } from './empty-bookinglist.component';

describe('EmptyBookinglistComponent', () => {
  let component: EmptyBookinglistComponent;
  let fixture: ComponentFixture<EmptyBookinglistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmptyBookinglistComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmptyBookinglistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
