import { Component, inject } from '@angular/core';
import { all_bookingsDto } from '../../data/interfaces/all_bookings.interface';
import { BookingsService } from '../../data/services/bookings.service';
import { CommonModule } from '@angular/common';
import { DeleteMessageComponent } from "../delete-message/delete-message.component";

@Component({
  selector: 'app-my-bookings',
  imports: [CommonModule, DeleteMessageComponent],
  templateUrl: './my-bookings.component.html',
  styleUrl: './my-bookings.component.scss'
})
export class MyBookingsComponent {

  allBookings: all_bookingsDto[] | null = null;

  bookingService = inject(BookingsService);
  ngOnInit() {
this.loadBookings();
  }
  showMessage: boolean = false;
  chosenBookingId: string = '';

  deleteBooking(id: string) {
this.chosenBookingId=id;
this.showMessage=true;
this.loadBookings();
  }

  printDate(startDateRaw: Date | string, endDateRaw: Date | string) {
    const startDate = new Date(startDateRaw);
    const endDate = new Date(endDateRaw);

    const day1 = startDate.getDate();
    const year1 = startDate.getFullYear();
    const monthNames = [
      'January', 'February', 'March', 'April', 'May', 'June',
      'July', 'August', 'September', 'October', 'November', 'December'
    ];
    const monthName1 = monthNames[startDate.getMonth()];

    const day2 = endDate.getDate();
    const year2 = endDate.getFullYear();
    const monthName2 = monthNames[endDate.getMonth()];

    const diffInMs = endDate.getTime() - startDate.getTime();
    const diffInDays = Math.ceil(diffInMs / (1000 * 60 * 60 * 24));

    if (day1 === day2 && monthName1 === monthName2 && year1 === year2) {
      return `${monthName1} ${day1}, ${year1}`;
    }

    if (diffInDays > 0) {
      return `${monthName1} ${day1}, ${year1} - ${monthName2} ${day2}, ${year2} (${diffInDays} days)`;
    }

    return `${monthName1} ${day1}, ${year1} - ${monthName2} ${day2}, ${year2}`;
  }

  printTime(startDateRaw: Date | string, endDateRaw: Date | string, duration: number) {
    const startDate = new Date(startDateRaw);
    const endDate = new Date(endDateRaw);

    const hours = startDate.getHours();
    const minutes = startDate.getMinutes().toString().padStart(2, '0');
    const suffix = hours >= 12 ? 'PM' : 'AM';
    const formattedHours = hours % 12 || 12;
    const time = `${formattedHours}:${minutes} ${suffix}`;

    const hours2 = endDate.getHours();
    const minutes2 = endDate.getMinutes().toString().padStart(2, '0');
    const suffix2 = hours2 >= 12 ? 'PM' : 'AM';
    const formattedHours2 = hours2 % 12 || 12;
    const time2 = `${formattedHours2}:${minutes2} ${suffix2}`;

    if (duration < 24) {
      return `from ${time} to ${time2} (${duration} hours)`;
    }

    return `from ${time} to ${time2}`;
  }

  getImage(name: string) {
    const nameFolder = name.toLocaleLowerCase().replace(' ', '_')
    return `assets/${nameFolder}/image1.svg`;
  }
  loadBookings(){
        this.bookingService.getAllBookings().subscribe({
      next: (val) => (this.allBookings = val),
      error: err => console.error()
    });
  }
}
