import { Component, OnInit } from '@angular/core';
import { AnnouncementService } from '../../services/announcement.service';
import { Announcement } from '../../models/announcement';
import { CommonModule, DatePipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-announcement-list',
  standalone: true,
  imports: [
    DatePipe,
    MatIconModule,
    CommonModule, RouterModule
  ],
  providers: [
    AnnouncementService,

  ],
  templateUrl: './announcement-list.component.html',
  styleUrl: './announcement-list.component.scss'
})
export class AnnouncementListComponent implements OnInit {
  announcementList: Announcement[] = [];

  constructor(private announcementService: AnnouncementService) {}

  ngOnInit(): void {
      this.getAnnouncementList();
  }

  getAnnouncementList(): void {
    this.announcementService.getAllAnnouncements().subscribe({
      next: (response) => this.announcementList = response,
      error: (err) => console.log('err')
    });
  }

  editAnnouncement(id: string): void {

  }

  deleteAnnouncement(id: string): void {
    this.announcementService.deleteAnnouncement(id).subscribe({
      next: (responese) => {
        this.announcementList = this.announcementList.filter(a => a.id !== id);
        console.log('success');
      },
      error: (err) => console.log('fail')
    });
  }
}
