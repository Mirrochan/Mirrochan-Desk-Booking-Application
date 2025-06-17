export interface CoworkingSummaryDto {
  id: string;
  name: string;
  address: string;
  description:string;
  workspaceSummary: {
    [key: string]: number; 
  };
}
