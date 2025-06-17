import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CoworkingSummaryDto } from '../interfaces/CoworkingSummaryDto';
import { CoworkingDetailDto } from '../interfaces/coworking-detail.dto';
import {  WorkspaceInfoDto } from '../interfaces/workspace.interface';

@Injectable({
  providedIn: 'root'
})
export class CoworkingService {
  private readonly baseUrl = 'http://localhost:5009/api/Coworking';

  constructor(private http: HttpClient) {}

  getAll(): Observable<CoworkingSummaryDto[]> {
    return this.http.get<CoworkingSummaryDto[]>(this.baseUrl);
  }

  getById(id: string) {
    return this.http.get<WorkspaceInfoDto[]>(`${this.baseUrl}/${id}`);
  }
}
