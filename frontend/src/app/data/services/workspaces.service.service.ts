import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { WorkspaceInfoDto } from '../interfaces/workspace.interface';

@Injectable({
  providedIn: 'root'
})
export class WorkspacesService {

 http: HttpClient = inject(HttpClient)
 getAllWorkspaces(){
  return this.http.get<WorkspaceInfoDto[]>("https://localhost:7060/api/workspaces")
 }
  getWorkspacesList(){
  return this.http.get<WorkspaceInfoDto[]>("https://localhost:7060/api/workspacesList")
 }
 
}
