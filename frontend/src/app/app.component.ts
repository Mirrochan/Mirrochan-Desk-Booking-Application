import { Component, inject, OnInit } from '@angular/core';
import { NavigationEnd, Route, Router, RouterOutlet } from '@angular/router';
import { MyBookingsComponent } from './features/my-bookings/my-bookings.component';
import { WorkspacesService } from './data/services/workspaces.service.service';
import { CoworkingComponent } from "./features/coworking/coworking.component";
import { BookingFormComponent } from "./features/booking-form/booking-form.component";
import { MessageComponent } from "./features/message/message.component";
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule ,RouterOutlet, MyBookingsComponent, CoworkingComponent, BookingFormComponent, MessageComponent],
  templateUrl:'app.component.html',
  styleUrl:'app.component.scss'
})
export class AppComponent implements OnInit {
  workspacesService: WorkspacesService = inject(WorkspacesService)
  workspaces: any[] = [];
  isActive: number = 1;

  constructor(private router: Router) {}

  ngOnInit() {

    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        const url = event.urlAfterRedirects;
        if (url.includes('/coworking')) this.isActive = 1;
        else if (url.includes('/my-bookings')) this.isActive = 2;
        else if (url.includes('/bookings/edit')) this.isActive = 2;
      });
  }

  goToWorkspace() {
    this.router.navigate(['/coworking']);
  }

  goToMyBookings() {
    this.router.navigate(['/my-bookings']);
  }
}