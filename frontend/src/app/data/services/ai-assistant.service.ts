import { Injectable } from "@angular/core";
import { all_bookingsDto } from "../interfaces/all_bookings.interface";
import { HttpClient } from "@angular/common/http";
import { map, Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AIAssistantService {
  constructor(private http: HttpClient) {}

  askQuestion(question: string, bookings: all_bookingsDto[]): Observable<string> {
    const bookingContext = JSON.stringify(bookings, (key, value) => {
      if (key === 'startDate' || key === 'endDate') {
        const d = new Date(value);
        const month = (d.getMonth() + 1).toString().padStart(2, '0');
        const day = d.getDate().toString().padStart(2, '0');
        const year = d.getFullYear();
        const hours = d.getHours().toString().padStart(2, '0');
        const minutes = d.getMinutes().toString().padStart(2, '0');
        return `${month}/${day}/${year} ${hours}:${minutes}`;
      }
      return value;
    });

   
    return this.http.post<{ answer: string }>('http://localhost:5009/api/Ai/ask', {
      question,
      Bookings: bookingContext 
    }).pipe(
      map(res => res.answer)
    );
  }
}
