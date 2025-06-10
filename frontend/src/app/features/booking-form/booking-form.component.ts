import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingsService } from '../../data/services/bookings.service';
import {  UpdateBookingDto } from '../../data/interfaces/update_booking.interface';
import { WorkspacesService } from '../../data/services/workspaces.service.service';
import { DropDownListComponent } from '../drop-down-list/drop-down-list.component';
import { MessageComponent } from '../message/message.component';
import { WorkspaceInfoDto } from '../../data/interfaces/workspace.interface';
import { AddBookingDto } from '../../data/interfaces/add_booking.interface';

@Component({
  selector: 'app-booking-form',
  standalone: true,
  imports: [DropDownListComponent, CommonModule, FormsModule, MessageComponent],
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.scss']
})
export class BookingFormComponent implements OnInit {

  private bookingsService = inject(BookingsService);
  private workspaceService = inject(WorkspacesService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
 bookingDate:AddBookingDto | null=null;
  mode: 'add' | 'edit' = 'add';
  bookingId: string | null = null;
  showMessage = false;
  messageType: string = '';
  
  workspaces: WorkspaceInfoDto[] = [];
  workspaceTypes: string[] = [];
  availableCapacities: number[] = [];
  showCapacityOptions = false;

  months = [
    { name: 'January', days: 31 },
    { name: 'February', days: 28 },
    { name: 'March', days: 31 },
    { name: 'April', days: 30 },
    { name: 'May', days: 31 },
    { name: 'June', days: 30 },
    { name: 'July', days: 31 },
    { name: 'August', days: 31 },
    { name: 'September', days: 30 },
    { name: 'October', days: 31 },
    { name: 'November', days: 30 },
    { name: 'December', days: 31 }
  ];

  monthNames: string[] = this.months.map(m => m.name);
  years = ["2025", "2026", "2027", "2028", "2029", "2030", "2031"];
  startDays: string[] = [];
  endDays: string[] = [];

  timeSlots = [
    '8:00 AM', '8:30 AM', '9:00 AM', '9:30 AM', '10:00 AM', '10:30 AM',
    '11:00 AM', '11:30 AM', '12:00 PM', '12:30 PM', '1:00 PM', '1:30 PM',
    '2:00 PM', '2:30 PM', '3:00 PM', '3:30 PM', '4:00 PM', '4:30 PM',
    '5:00 PM', '5:30 PM', '6:00 PM', '6:30 PM', '7:00 PM', '7:30 PM'
  ];

  bookingData = {
    name: '',
    email: '',
    workspaceType: '',
    capacity: null as number | null,
    startDay: '',
    startMonth: '',
    startYear: '',
    endDay: '',
    endMonth: '',
    endYear: '',
    startTime: '9:00 AM',
    endTime: '5:00 PM'
  };
goToMyBookings() {
this.router.navigate(['/my-bookings']);
}
  ngOnInit(): void {
    this.bookingId = this.route.snapshot.params['id'];
    this.mode = this.bookingId ? 'edit' : 'add';

    this.initializeForm();
    this.loadWorkspaces();

    if (this.mode === 'edit' && this.bookingId) {
      this.loadBookingData(this.bookingId);
    }
  }

  private initializeForm(): void {
    const today = new Date();
    const currentMonth = this.months[today.getMonth()];
    
    this.bookingData.startMonth = currentMonth.name;
    this.bookingData.endMonth = currentMonth.name;
    this.bookingData.startYear = today.getFullYear().toString();
    this.bookingData.endYear = today.getFullYear().toString();
    
    this.updateDaysForMonth(today.getMonth(), today.getFullYear(), true);
    this.updateDaysForMonth(today.getMonth(), today.getFullYear(), false);
    
    this.bookingData.startDay = today.getDate().toString();
    this.bookingData.endDay = today.getDate().toString();
  }

  private loadWorkspaces(): void {
    this.workspaceService.getAllWorkspaces().subscribe({
      next: (workspaces) => {
        this.workspaces = workspaces;
        this.workspaceTypes = workspaces.map(w => w.name);
      },
      error: (err) => console.error('Error fetching workspaces', err)
    });
  }

  private loadBookingData(id: string): void {
    this.bookingsService.getBookingById(id).subscribe({
      next: (booking) => {
        this.setFormData(booking);
      },
      error: (err) => console.error('Error loading booking', err)
    });
  }

  private setFormData(booking: UpdateBookingDto): void {
    this.bookingData.name = booking.userName;
    this.bookingData.email = booking.userEmail;
    this.bookingData.capacity = booking.peopleCount;

    const startDate = new Date(booking.startDate);
    const endDate = new Date(booking.endDate);

    this.setDateFields(startDate, true);
    this.setDateFields(endDate, false);

    this.bookingData.startTime = this.formatTime(startDate);
    this.bookingData.endTime = this.formatTime(endDate);

    const workspace = this.workspaces.find(w => w.id === booking.workSpaceId);
    if (workspace) {
      this.bookingData.workspaceType = workspace.name;
      this.onWorkspaceTypeChange();
    }
  }

  private setDateFields(date: Date, isStart: boolean): void {
    const month = this.months[date.getMonth()];
    const year = date.getFullYear();
    const day = date.getDate();

    if (isStart) {
      this.bookingData.startMonth = month.name;
      this.bookingData.startYear = year.toString();
      this.bookingData.startDay = day.toString();
    } else {
      this.bookingData.endMonth = month.name;
      this.bookingData.endYear = year.toString();
      this.bookingData.endDay = day.toString();
    }

    this.updateDaysForMonth(date.getMonth(), year, isStart);
  }

  onWorkspaceTypeChange(): void {
    this.showCapacityOptions = this.bookingData.workspaceType !== 'Open Space';
    this.availableCapacities = [];
    this.bookingData.capacity = null;

    const selectedWorkspace = this.workspaces.find(
      w => w.name === this.bookingData.workspaceType
    );

    if (selectedWorkspace?.capacity) {
      this.availableCapacities = selectedWorkspace.capacity;
    }
  }

  onMonthChange(isStart: boolean): void {
    const monthName = isStart ? this.bookingData.startMonth : this.bookingData.endMonth;
    const year = parseInt(isStart ? this.bookingData.startYear : this.bookingData.endYear);
    
    const monthIndex = this.months.findIndex(m => m.name === monthName);
    if (monthIndex !== -1) {
      this.updateDaysForMonth(monthIndex, year, isStart);
    }
  }

  onYearChange(isStart: boolean): void {
    const monthName = isStart ? this.bookingData.startMonth : this.bookingData.endMonth;
    if (monthName === 'February') {
      this.onMonthChange(isStart);
    }
  }

  private updateDaysForMonth(monthIndex: number, year: number, isStart: boolean): void {
    let daysInMonth = this.months[monthIndex].days;
    
    if (monthIndex === 1 && this.isLeapYear(year)) {
      daysInMonth = 29;
    }
    
    const days = Array.from({ length: daysInMonth }, (_, i) => (i + 1).toString());
    
    if (isStart) {
      this.startDays = days;
      if (parseInt(this.bookingData.startDay) > daysInMonth) {
        this.bookingData.startDay = daysInMonth.toString();
      }
    } else {
      this.endDays = days;
      if (parseInt(this.bookingData.endDay) > daysInMonth) {
        this.bookingData.endDay = daysInMonth.toString();
      }
    }
  }

  private isLeapYear(year: number): boolean {
    return (year % 4 === 0 && year % 100 !== 0) || year % 400 === 0;
  }

  private formatTime(date: Date): string {
    let hours = date.getHours();
    const minutes = date.getMinutes();
    const ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12;
    return `${hours}:${minutes < 10 ? '0' + minutes : minutes} ${ampm}`;
  }

  onSubmit(): void {
    const bookingData = this.prepareBookingData();
    
    if (this.mode === 'add') {
      this.bookingsService.addNewBooking(bookingData).subscribe({
      
        next: () => {this.bookingDate=bookingData;this.handleSuccess('Booking created successfully!')},
        error: (err) => this.handleError(err)
      });
    } else if (this.bookingId) {
      this.bookingsService.updateBooking({ ...bookingData, id: this.bookingId }).subscribe({
        next: () => this.handleSuccess('Booking updated successfully!'),
        error: (err) => this.handleError(err)
      });
    }
  }

  private prepareBookingData(): AddBookingDto {
    const workspace = this.workspaces.find(w => w.name === this.bookingData.workspaceType);
    
    if (!workspace) {
      throw new Error('Workspace not found');
    }

    return {
      userName: this.bookingData.name,
      userEmail: this.bookingData.email,
      workSpaceId: workspace.id,
      startDate: this.combineDateTime(
        this.bookingData.startYear,
        this.bookingData.startMonth,
        this.bookingData.startDay,
        this.bookingData.startTime
      ),
      endDate: this.combineDateTime(
        this.bookingData.endYear,
        this.bookingData.endMonth,
        this.bookingData.endDay,
        this.bookingData.endTime
      ),
      peopleCount: this.bookingData.capacity ?? 0
    };
  }

  private combineDateTime(year: string, monthName: string, day: string, time: string): Date {
    const monthIndex = this.months.findIndex(m => m.name === monthName);
    const [hourString, minuteString, period] = time.match(/(\d+):(\d+)\s?(AM|PM)/i)!.slice(1);
    
    let hour = parseInt(hourString);
    const minute = parseInt(minuteString);

    if (period.toUpperCase() === 'PM' && hour < 12) hour += 12;
    if (period.toUpperCase() === 'AM' && hour === 12) hour = 0;

    return new Date(
      parseInt(year),
      monthIndex,
      parseInt(day),
      hour,
      minute
    );
  }

  private handleSuccess(message: string): void {
    this.messageType = "confirmation-message";
    this.showMessage = true;
    setTimeout(() => this.router.navigate(['/bookings']), 2000);
    if(this.mode !== 'add'){
       this.messageType = "confirmation-update-message";
    this.showMessage = true;
    }
  }

  private handleError(error: any): void {
    this.messageType = error.error.error;
      
   if(this.messageType === undefined){
      this.messageType = error.error;
    }
    
     if (this.messageType === "The selected workspace is not available for the specified time period and people count.") {
      this.messageType = "notavailable-message";
    }
    console.log("тип1"+this.messageType);
    this.showMessage = true;
    console.log("тип"+this.messageType);
    console.error('Error:', error);
  }
}