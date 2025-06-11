export interface UpdateBookingDto{
    id:string;
    userName:string;
    userEmail:string;
    workSpaceId:string;
    startDate:Date;
    endDate:Date;
    peopleCount:number;
}