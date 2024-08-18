export interface Announcement {
  id: string;
  title: string;
  description: string;
  dateAdded: Date;
}

export interface AnnouncementToAddDto {
  title: string;
  description: string;
}