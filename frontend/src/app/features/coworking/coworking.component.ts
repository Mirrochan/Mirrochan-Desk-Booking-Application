import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-coworking',
  imports: [],
  templateUrl: './coworking.component.html',
  styleUrl: './coworking.component.scss'
})

export class CoworkingComponent {
  constructor(private router: Router) {}

  navigateToBooking() {
    this.router.navigate(['/book']);
  }

  navigateToMyBookings() {
    this.router.navigate(['/my-bookings']);
  }
}