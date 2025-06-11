import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-empty-bookinglist',
  imports: [],
  templateUrl: './empty-bookinglist.component.html',
  styleUrl: './empty-bookinglist.component.scss'
})
export class EmptyBookinglistComponent {
constructor(private router: Router){}
goToWorkspace(){
this.router.navigate(["/coworking"]);
}
}
