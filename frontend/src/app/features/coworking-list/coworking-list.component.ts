import { Component, inject, OnInit } from '@angular/core';
import { CoworkingSummaryDto } from '../../data/interfaces/CoworkingSummaryDto';
import { CoworkingService } from '../../data/services/coworking.service';
import { CommonModule } from '@angular/common';
import { Route, Router } from '@angular/router';
import { EmptyWorkingspacesComponent } from "../empty-workingspaces/empty-workingspaces.component";

@Component({
  selector: 'app-coworking-list',
  imports: [CommonModule, EmptyWorkingspacesComponent],
  templateUrl: './coworking-list.component.html',
  styleUrl: './coworking-list.component.scss'
})
export class CoworkingListComponent implements OnInit {
  constructor(private coworkingService: CoworkingService) { }

  private router = inject(Router);
  navigateToWorkspace(id: string) {
    this.router.navigate(['/coworking', id]);
  }
  coworkings: CoworkingSummaryDto[] = [];

  isEmpty:boolean=false;
  ngOnInit(): void {
    this.coworkingService.getAll().subscribe({
      next: (data) => {
        this.coworkings = data;
        console.log(this.coworkings);

      this.isEmpty = this.coworkings.length === 0;
  },
  error: (err) => {
    console.error('Error fetching workspaces', err);
    this.isEmpty = true; 
  }
    });
  }

  getCount(summary: CoworkingSummaryDto, type: string): string {
    const nameType = type.replace(/([a-z])([A-Z])/g, '$1 $2').toLowerCase();
    if(type=='OpenSpace'){
      if(summary.workspaceSummary[type]==1 ){
        `${summary.workspaceSummary[type]} desk`
      }
      else  if(summary.workspaceSummary[type]>1 ) {return `${summary.workspaceSummary[type]} desks`}
else{return 'No desks'}
    }
    if (summary.workspaceSummary[type] == null) {
   
      return `No ${nameType}s`
    }
    if (summary.workspaceSummary[type] == 1) {
      return `${summary.workspaceSummary[type]} ${nameType}`
    }
    return `${summary.workspaceSummary[type]} ${nameType}s`;
  }
}