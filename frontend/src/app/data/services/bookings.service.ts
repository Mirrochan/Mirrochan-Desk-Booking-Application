import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AddBookingDto } from '../interfaces/add_booking.interface';
import { all_bookingsDto } from '../interfaces/all_bookings.interface';
import { UpdateBookingDto } from '../interfaces/update_booking.interface';

@Injectable({
  providedIn: 'root'
})
export class BookingsService {
  private readonly apiUrl = "https://localhost:7060/api/Bookings";
  http: HttpClient = inject(HttpClient);

  addNewBooking(data: AddBookingDto) {
    return this.http.post<AddBookingDto>(this.apiUrl, data);
  }

  deleteBooking(id: string) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getAllBookings() {
    return this.http.get<all_bookingsDto[]>(this.apiUrl+'/getAll');
  }

  getBookingById(id: string) {
    return this.http.get<UpdateBookingDto>(`${this.apiUrl}?id=${id}`);
  }

  updateBooking(data: UpdateBookingDto) {
    return this.http.put(this.apiUrl, data);
  }
  getLastValidBooking(){
    return this.http.get<all_bookingsDto>(this.apiUrl+"/last");
  }
}
