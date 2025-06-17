export interface UpdateBookingDto{
    id:string;
    userName:string;
    userEmail:string;
    workSpaceId:string;
     startDateLocal: Date;
  endDateLocal: Date;
    peopleCount:number;
}