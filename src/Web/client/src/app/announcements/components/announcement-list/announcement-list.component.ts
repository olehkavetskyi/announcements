import { Component, OnInit } from '@angular/core';
import { AnnouncementService } from '../../services/announcement.service';
import { Announcement } from '../../models/announcement';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-announcement-list',
  standalone: true,
  imports: [
    DatePipe,
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
      error: () => console.log('error')
    });
  }
}
