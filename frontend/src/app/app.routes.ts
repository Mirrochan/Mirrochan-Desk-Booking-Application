import { Routes } from '@angular/router';
import { CoworkingComponent } from './features/coworking/coworking.component';
import { MyBookingsComponent } from './features/my-bookings/my-bookings.component';
import { BookingFormComponent } from './features/booking-form/booking-form.component';
import { MessageComponent } from './features/message/message.component';
import { CoworkingListComponent } from './features/coworking-list/coworking-list.component';

export const routes: Routes = [
    { path: '', redirectTo: 'coworkingList', pathMatch: 'full' },
{path:'coworkingList',component:CoworkingListComponent},
    { path: 'coworking/:id', component: CoworkingComponent },
    { path: 'booking', component: BookingFormComponent },
    { path: 'my-bookings', component: MyBookingsComponent },
    { path: 'message', component: MessageComponent },
    {
        path: 'bookings/edit/:id',
        component: BookingFormComponent,
        title: 'Edit Booking'}

];
