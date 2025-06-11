import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { all_bookingsDto } from '../../data/interfaces/all_bookings.interface';
import { Router } from '@angular/router';
import { BookingsService } from '../../data/services/bookings.service';

@Component({
  selector: 'app-delete-message',
  imports: [],
  templateUrl: './delete-message.component.html',
  styleUrl: './delete-message.component.scss'
})
export class DeleteMessageComponent {
  @Input() idDelete: string = '';
  @Output() close = new EventEmitter<void>();
  bookingService = inject(BookingsService);
  constructor(private router: Router) { }
  exit() {
    this.close.emit();
  }
  cancelBooking() {
    this.bookingService.deleteBooking(this.idDelete).subscribe({
      error: err => console.log()
    });
    setTimeout(() => {
 this.router.navigate(["/my-bookings"]);
        this.close.emit();
}, 1000);
   
  }

}
