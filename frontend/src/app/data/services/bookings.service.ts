import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AddBookingDto } from '../interfaces/add_booking.interface';
import { all_bookingsDto } from '../interfaces/all_bookings.interface';
import { UpdateBookingDto } from '../interfaces/update_booking.interface';

@Injectable({
  providedIn: 'root'
})
export class BookingsService {
  http: HttpClient = inject(HttpClient)
  addNewBooking(data: AddBookingDto) {
    return this.http.post<AddBookingDto>("https://localhost:7060/api/Bookings", data);
  }
  deleteBooking(id: string) {
    return this.http.delete(`https://localhost:7060/api/Bookings/${id}`);
  }
  getAllBookings() {
    return this.http.get<all_bookingsDto[]>("https://localhost:7060/api/Bookings");
  }
  updateBooking(data:UpdateBookingDto){
    return this.http.put("https://localhost:7060/api/Bookings",data);
  }
 

}


