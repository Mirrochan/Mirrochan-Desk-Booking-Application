import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AddBookingDto } from '../../data/interfaces/add_booking.interface';
import { CommonModule } from '@angular/common';
import { Route, Router } from '@angular/router';
import { routes } from '../../app.routes';

@Component({
  selector: 'app-message',
  imports: [CommonModule],
  templateUrl: './confirmation-message.component.html',
  styleUrl: './confirmation-message.component.scss'
})

export class MessageComponent implements OnInit {
  @Input() message: string = '';
  @Input() data: AddBookingDto | null = null;
  @Output() close = new EventEmitter<void>();
  constructor(private router: Router) { }
 
  imgUrl: string = "assets/img/Error.svg";
  info: string = "Error";
  desc: string = this.message;
  buttonName: string = "Return";
  ngOnInit(): void {
    this.desc=this.message;
   console.log("data in messega"+this.data);
   console.log("message in messega"+this.message);
    switch (this.message) {
      case 'confirmation-message': {
        this.imgUrl = 'assets/img/messag-confirm.svg';
        this.info = "You're all set!";
        this.desc = '';
        this.buttonName = 'My bookings';
        break;
      }
      case 'notavailable-message': {
        this.imgUrl = 'assets/img/Error.svg';
        this.info = "Selected time is not available";
        this.desc = 'Please choose a different time slot';
        this.buttonName = 'Check availability';
        break;
      }
      case 'confirmation-update-message':{
        this.imgUrl = 'assets/img/messag-confirm.svg';
        this.info = "Your booking has been successfully updated.";
        this.desc = '';
        this.buttonName = 'My bookings';
        break;
      }
    }
  }
  exit() {
    this.close.emit();
  }
  navigateToPages() {
    switch (this.buttonName) {
      case 'My bookings': {
        this.router.navigate(['/my-bookings']);
        break;
      }
      case 'No, keep it': {
        this.close.emit();
        break;
      }
      case 'Return': {
        this.close.emit();
        break;
      }
      case 'Check availability': {
        this.close.emit();
      }
    }
  }
}



