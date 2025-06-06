import { Component, inject, OnInit } from '@angular/core';
import { Route, Router, RouterOutlet } from '@angular/router';
import { MyBookingsComponent } from './features/my-bookings/my-bookings.component';
import { WorkspacesService } from './data/services/workspaces.service.service';
import { CoworkingComponent } from "./features/coworking/coworking.component";
import { BookingFormComponent } from "./features/booking-form/booking-form.component";
import { MessageComponent } from "./features/message/message.component";
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule ,RouterOutlet, MyBookingsComponent, CoworkingComponent, BookingFormComponent, MessageComponent],
  templateUrl:'app.component.html',
  styleUrl:'app.component.scss'
})
export class AppComponent {
workspacesService:WorkspacesService = inject(WorkspacesService)
workspaces:any=[];
isActive: number=1;

constructor(private router: Router){}
goToWorkspace(){
this.router.navigate(["/coworking"]);
this.isActive=1;
}
goToMyBookings(){
  this.router.navigate(["my-bookings"]);
  this.isActive=2;
}
}