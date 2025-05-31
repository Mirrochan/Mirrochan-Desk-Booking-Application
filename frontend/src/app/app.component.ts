import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MyBookingsComponent } from './features/my-bookings/my-bookings.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MyBookingsComponent],
  templateUrl:'app.component.html',
  styleUrl:'app.component.scss'
})
export class AppComponent {}