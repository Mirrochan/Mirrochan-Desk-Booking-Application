import { Component } from '@angular/core';
import { Location } from '@angular/common';
@Component({
  selector: 'app-empty-workingspaces',
  imports: [],
  templateUrl: './empty-workingspaces.component.html',
  styleUrl: './empty-workingspaces.component.scss'
})
export class EmptyWorkingspacesComponent {
  refreshList() {
    throw new Error('Method not implemented.');
  }
  constructor(private location: Location) {}
 

}
