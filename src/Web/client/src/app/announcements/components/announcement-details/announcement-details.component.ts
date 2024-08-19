import { Component, Input, OnInit } from '@angular/core';
import { AnnouncementService } from '../../services/announcement.service';
import { Announcement } from '../../models/announcement';
import { Router, RouterModule } from '@angular/router';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-announcement-details',
  standalone: true,
  imports: [
    RouterModule,
    DatePipe,
  ],
  providers: [ 
    AnnouncementService,
    Router,
  ],
  templateUrl: './announcement-details.component.html',
  styleUrl: './announcement-details.component.scss'
})
export class AnnouncementDetailsComponent implements OnInit {
  announcement: Announcement | undefined;

  @Input() id!: string;

  constructor(private announcementService: AnnouncementService, private router: Router, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.announcementService.getAnnouncementById(this.id).subscribe({
      next: (result) => { 
        this.announcement = result;
      },
      error: () => this.toastr.error("Oops! Something went wrong!")
    })
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

