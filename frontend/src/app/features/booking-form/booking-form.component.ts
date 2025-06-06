import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { BookingsService } from '../../data/services/bookings.service';
import { AddBookingDto } from '../../data/interfaces/add_booking.interface';
import { WorkspaceDto, WorkspaceInfoDto } from '../../data/interfaces/workspace.interface';
import { WorkspacesService } from '../../data/services/workspaces.service.service';
import { DropDownListComponent } from '../drop-down-list/drop-down-list.component';
import { MessageComponent } from '../message/message.component';


@Component({
  selector: 'app-booking-form',
  imports: [DropDownListComponent, CommonModule, FormsModule, MessageComponent],
  templateUrl: './booking-form.component.html',
  styleUrl: './booking-form.component.scss'
})

export class BookingFormComponent implements OnInit {
  private bookingsService = inject(BookingsService);
  private workspaceService = inject(WorkspacesService);
  showMessage = false;
  messageType: string = '';
  bookingDto: AddBookingDto | null = null;
  workspaces: WorkspaceInfoDto[] = [];
  bookingData = {
    name: '',
    email: '',
    workspaceType: '',
    capacity: null,
    startDay: '',
    startMonth: '',
    startYear: '',
    endDay: '',
    endMonth: '',
    endYear: '',
    startTime: '',
    endTime: ''
  };

  workspaceTypes: string[] = [];

  availableCapacities: number[] = [];
  showCapacityOptions = false;

  // Date options
  daysNums = Array.from({ length: 31 }, (_, i) => i + 1);
  days = this.daysNums.map(d => d.toString());


  months = ['January', 'February', 'March', 'April', 'May', 'June', 'July',
    'August', 'September', 'October', 'November', 'December'];
  years = ["2025", "2026", "2027", "2028", "2029", "2030", "2031"];

  // Time slots
  timeSlots = [
    '8:00 AM', '8:30 AM', '9:00 AM', '9:30 AM', '10:00 AM', '10:30 AM',
    '11:00 AM', '11:30 AM', '12:00 PM', '12:30 PM', '1:00 PM', '1:30 PM',
    '2:00 PM', '2:30 PM', '3:00 PM', '3:30 PM', '4:00 PM', '4:30 PM',
    '5:00 PM', '5:30 PM', '6:00 PM', '6:30 PM', '7:00 PM', '7:30 PM'
  ];

  constructor(private router: Router) { }

  ngOnInit(): void {

    // Initialize with current date
    const today = new Date();
    this.bookingData.startDay = today.getDate().toString();
    this.bookingData.startMonth = this.months[today.getMonth()];
    this.bookingData.startYear = today.getFullYear().toString();
    this.bookingData.endDay = today.getDate().toString();
    this.bookingData.endMonth = this.months[today.getMonth()];
    this.bookingData.endYear = today.getFullYear().toString();

    // Set default times
    this.bookingData.startTime = '9:00 AM';
    this.bookingData.endTime = '5:00 PM';
    this.workspaceService.getAllWorkspaces().subscribe({
      next: (val) => {
        this.workspaces = val;
        this.workspaceTypes = val.map(v => v.name)
      },
      error: (err) => console.error('Error fetching workspaces', err)
    });
  }

  onWorkspaceTypeChange(): void {
    this.showCapacityOptions = this.bookingData.workspaceType !== 'Open Space';


    this.availableCapacities = [];
    this.bookingData.capacity = null;

    const selectedWorkspace = this.workspaces.find(
      w => w.name === this.bookingData.workspaceType
    );

    if (selectedWorkspace && selectedWorkspace.capacity) {
      this.availableCapacities = selectedWorkspace.capacity;
    }

  }


  onSubmit(): void {
    const startDate = this.combineDateTime(
      this.bookingData.startYear,
      this.bookingData.startMonth,
      this.bookingData.startDay,
      this.bookingData.startTime
    );

    const endDate = this.combineDateTime(
      this.bookingData.endYear,
      this.bookingData.endMonth,
      this.bookingData.endDay,
      this.bookingData.endTime
    );

    let workSpaceId: string = '';
    this.workspaces.forEach(element => {
      if (this.bookingData.workspaceType === element.name) {
        workSpaceId = element.id;
      }
    });

    this.bookingDto = {
      userName: this.bookingData.name,
      userEmail: this.bookingData.email,
      workSpaceId: workSpaceId,
      startDate: startDate,
      endDate: endDate,
      peopleCount: this.bookingData.capacity ?? 0
    };

    console.log('Booking submitted:', this.bookingData);
    this.bookingsService.addNewBooking(this.bookingDto).subscribe({
      next: () => {
        this.messageType = "confirmation-message";
        this.showMessage = true;
      },
      error: (err) => {
        this.messageType = err.error;
        console.log(this.messageType);
        if (this.messageType === "The selected workspace is not available for the specified time period and people count.") {
          this.messageType = "notavailable-message";
        }
        this.showMessage = true;
      }
    });

  }
  combineDateTime(year: string, monthName: string, day: string, time: string): Date {
    const monthIndex = this.months.indexOf(monthName); // 0-based index
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

}
