import { Component, OnInit } from '@angular/core';
import { AnnouncementService } from '../../services/announcement.service';
import { Announcement } from '../../models/announcement';
import { DatePipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { AddAnnouncementComponent } from "../add-announcement/add-announcement.component";
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-announcement-list',
  standalone: true,
  imports: [
    DatePipe,
    MatIconModule, 
    RouterLink,
    AddAnnouncementComponent,
],
  providers: [
    AnnouncementService,
  ],
  templateUrl: './announcement-list.component.html',
  styleUrl: './announcement-list.component.scss'
})
export class AnnouncementListComponent implements OnInit {
  announcementList: Announcement[] = [];
  showAddForm: boolean = false;

  constructor(private announcementService: AnnouncementService,  private toastr: ToastrService) {}

  ngOnInit(): void {
      this.getAnnouncementList();
  }

  getAnnouncementList(): void {
    this.announcementService.getAllAnnouncements().subscribe({
      next: (response) => this.announcementList = response
    });
  }

  onAnnouncementAdded(announcement: Announcement): void {
    this.announcementList = [
      ...this.announcementList,
      announcement
    ];
  }

  toggleAddFormVisability() {
    this.showAddForm = !this.showAddForm;
  }

  deleteAnnouncement(id: string): void {
    this.announcementService.deleteAnnouncement(id).subscribe({
      next: (responese) => {
        this.announcementList = this.announcementList.filter(a => a.id !== id);
        this.toastr.success("Successfully deleted");
      },
      error: (err) => this.toastr.error("Oops! An error occurred while deleting the announcement!")
    });
  }
}
