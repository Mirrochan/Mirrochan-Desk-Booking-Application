export interface WorkspaceDto {
  id: string;
  name: string;
  type: 'Desk' | 'PrivateRoom' | 'MeetingRoom';
}

export interface CoworkingDetailDto {
  id: string;
  name: string;
  address: string;
  workspaces: WorkspaceDto[];
}
