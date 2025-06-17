import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { WorkspacesService } from '../../data/services/workspaces.service.service';
import { WorkspaceInfoDto, AvailabilityOptionsDto, WorkspaceType } from '../../data/interfaces/workspace.interface';
import { FormsModule } from '@angular/forms';
import { all_bookingsDto } from '../../data/interfaces/all_bookings.interface';
import { BookingsService } from '../../data/services/bookings.service';
import { CoworkingService } from '../../data/services/coworking.service';
import { EmptyWorkingspacesComponent } from "../empty-workingspaces/empty-workingspaces.component";
import { Location } from '@angular/common';

@Component({
  selector: 'app-coworking',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './coworking.component.html',
  styleUrls: ['./coworking.component.scss']
})
export class CoworkingComponent implements OnInit {

  private coworkingService = inject(CoworkingService);
  private bookingService = inject(BookingsService);
  private route = inject(ActivatedRoute);
  workspaces: any[] = [];
  workspaceType = WorkspaceType;
  spaceId: string = '';
  constructor(private router: Router,private location: Location) { }

  lastValidBooking: all_bookingsDto | null = null;
 
goBack(): void {
  this.location.back();
}
  ngOnInit(): void {
    this.spaceId = this.route.snapshot.params['id'];
    this.coworkingService.getById(this.spaceId).subscribe({
  next: (val) => {
    this.workspaces = val;

  },
  error: (err) => {
    console.error('Error fetching workspaces', err);
  
  }
});
    this.bookingService.getLastValidBooking().subscribe({
      next: (val) => (this.lastValidBooking = val
    
        
      )
    });
   
    
  }

  navigateToMyBookings(): void {
    this.router.navigate(['/my-bookings']);
  }
  getWorkspaceImagePath(workspace: WorkspaceInfoDto, imageName: string): string {
    const workspaceFolder = workspace.name.toLowerCase().replace(' ', '_');
    return `assets/${workspaceFolder}/${imageName}.png`;
  }
  getAmenityImage(amenity: string): string {
    return `assets/img/${amenity}.svg`;
  }
  getCapacityDisplay(workspace: WorkspaceInfoDto): string {
    if (WorkspaceType.OpenSpace || workspace.name === "Open Space" || workspace.capacity.length === 0) {
      return `${workspace.availabilityOptions[0]?.quantity || 0} desks`;
    }

    if (workspace.capacity.length === 2) { return workspace.capacity.join(' or ') + ' people' }

    return workspace.capacity.join(', ') + ' people';
  }

  hasZeroCapacity(workspace: WorkspaceInfoDto): boolean {
    return workspace.capacity.length === 0 ||
      (workspace.capacity.length === 1 && workspace.capacity[0] === 0);
  }
  navigateToBooking(workspaceId: string): void {
    this.router.navigate(['/booking']);
  }
  selectedImages: { [workspaceId: string]: string } = {};

  getAllWorkspaceImages(workspace: any): string[] {
    return [
      this.getWorkspaceImagePath(workspace, 'image1'),
      this.getWorkspaceImagePath(workspace, 'image2'),
      this.getWorkspaceImagePath(workspace, 'image3'),
      this.getWorkspaceImagePath(workspace, 'image4')
    ];
  }

  selectImage(workspaceId: string, imageUrl: string) {
    this.selectedImages[workspaceId] = imageUrl;
  }
  formatDateRange(start?: string | Date, end?: string | Date): string {
    if (!start || !end) return '';

    const startDate = new Date(start);
    const endDate = new Date(end);

    const options: Intl.DateTimeFormatOptions = {
      month: 'short',
      day: 'numeric',
      year: 'numeric',
    };

    const formattedStart = startDate.toLocaleDateString('en-US', options);
    const formattedEnd = endDate.toLocaleDateString('en-US', options);
    return `${formattedStart} to ${formattedEnd}`;
  }


}