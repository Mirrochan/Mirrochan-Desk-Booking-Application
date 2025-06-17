import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AddBookingDto } from "../interfaces/add_booking.interface";
import { Injectable } from "@angular/core";
import { all_bookingsDto } from "../interfaces/all_bookings.interface";
import { map, Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class AIAssistantService {
    private readonly apiUrl = 'https://api.groq.com/openai/v1/chat/completions';

    constructor(private http: HttpClient) { }

    askQuestion(question: string, bookings: all_bookingsDto[]): Observable<string> {
        const prompt = this.buildPrompt(question, bookings);

        const headers = new HttpHeaders({
            'Authorization': `Bearer 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`,
            'Content-Type': 'application/json'
        });

        const body = {
            model: 'meta-llama/llama-4-scout-17b-16e-instruct',
            messages: [{ role: 'user', content: prompt }],
            temperature: 0.7
        };

        return this.http.post<any>(this.apiUrl, body, { headers }).pipe(
            map(res => res.choices?.[0]?.message?.content ?? 'Sorry, no response from AI.')
        );
    }
    private buildPrompt(question: string, bookings: all_bookingsDto[]): string {
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

        console.log(bookingContext);
        return `
      You are a booking assistant. Answer questions based on the following information::
      ${bookingContext}
      
      Question: ${question}
      You  must support:
Counting all bookings
Listing upcoming bookings
Showing bookings for a specific day
Listing bookings for the previous week
Filtering by workspace type
and other function

      Rules:
      - Be concise
      - Only use provided booking data
      - Format dates as MM/DD/YYYY
      - If unsure, say "Sorry, I didn’t understand that. Please try rephrasing your question."
      answer like it:📅 May 18, 2025 — Private room for 2 people at WorkClub Pechersk (10:00 – 12:00)
📅 May 20, 2025 — Private room for 2 people at UrbanSpace Podil (09:00 - 17:00)
      `;
    }
}