import { Routes } from '@angular/router';
import { AnnouncementListComponent } from './announcements/components/announcement-list/announcement-list.component';
import { EditAnnouncementComponent } from './announcements/components/edit-announcement/edit-announcement.component';

export const routes: Routes = [
  {
    path: '',
    component: AnnouncementListComponent
  },
  {
    path: 'announcements/:id/edit',
    component: EditAnnouncementComponent
  },
];
