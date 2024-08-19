import { Component, Input, OnInit } from '@angular/core';
import { AnnouncementService } from '../../services/announcement.service';
import { Announcement } from '../../models/announcement';
import { Router, RouterLink, } from '@angular/router';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-announcement-details',
  standalone: true,
  imports: [
    RouterLink,
    DatePipe,
    MatIconModule,
  ],
  providers: [ 
    AnnouncementService,
  ],
  templateUrl: './announcement-details.component.html',
  styleUrl: './announcement-details.component.scss'
})
export class AnnouncementDetailsComponent implements OnInit {
  announcement: Announcement | undefined;
  showSimilarAnnouncements = false;
  similarAnnouncements: Announcement[] = [];

  @Input() id!: string;

  constructor(
    private announcementService: AnnouncementService, 
    private router: Router, 
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.announcementService.getAnnouncementById(this.id).subscribe({
      next: (result) => { 
        this.announcement = result;
      },
      error: () => this.toastr.error("Oops! An error occurred while retrieving the announcement!")
    })
  }

  findSimilarAnnouncements() {
    this.announcementService.getSimilarAnnouncements(this.id).subscribe({
      next: (result) => {
        this.showSimilarAnnouncements = true;
        this.similarAnnouncements = result
      },
      error: () => this.toastr.error("Oops! An error occurred while retrieving similar announcements!")
    });
  }

    deleteAnnouncement(id: string | undefined) {
      let self = this;
      if (id) {
        this.announcementService.deleteAnnouncement(id).subscribe({
          next() {
            self.router.navigate(['/']);
          },
        })
      }
    }
}

