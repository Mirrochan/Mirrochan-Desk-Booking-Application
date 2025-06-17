import { Component, inject, OnInit } from '@angular/core';
import { NavigationEnd, Route, Router, RouterOutlet } from '@angular/router';
import { WorkspacesService } from './data/services/workspaces.service.service';
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
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
    this.router.navigate(['/coworkingList']);
  }

  goToMyBookings() {
    this.router.navigate(['/my-bookings']);
  }
}