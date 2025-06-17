export interface AvailabilityOptionsDto {
  capacity: number;
  unitType: string;
  quantity: number;
}
 export enum WorkspaceType
 {
     OpenSpace,
     PrivateRoom,
     MeetingRoom
 }
export interface WorkspaceInfoDto {
  id: string;
  name: string;
  description: string;
  type: string; 
  capacity: number[];
  amenities: string[];
  availabilityOptions: AvailabilityOptionsDto[];
 
}
