import { Component, Input, OnInit } from '@angular/core';
import { CustomInputComponent } from "../../../shared/components/custom-input/custom-input.component";
import { AnnouncementService } from '../../services/announcement.service';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { TimeService } from '../../../shared/services/time.service';
import {Location} from '@angular/common'; 
import { RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-announcement',
  standalone: true,
  imports: [
    CustomInputComponent,
    ReactiveFormsModule,
    RouterModule,
  ],
  providers: [
    AnnouncementService,
    TimeService,
  ],
  templateUrl: './edit-announcement.component.html',
  styleUrl: './edit-announcement.component.scss'
})
export class EditAnnouncementComponent implements OnInit {

  constructor(
    private fb: FormBuilder, 
    private announcementService: AnnouncementService, 
    private toast: ToastrService,
    private timeService: TimeService,
    private location: Location) {}

  @Input() id!: string;

  editForm = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    dateAdded: ['', Validators.required],
  });

  ngOnInit(): void {
    this.getAnnouncement(this.id);
  }

  getAnnouncement(id: string) {
    this.announcementService.getAnnouncementById(id).subscribe({
      next: (result) => {

        const date = new Date(result.dateAdded);
        const formattedDate = this.timeService.formatDateTimeForInput(date);

        this.editForm.patchValue({
          ...result,
          dateAdded: formattedDate, 
        });
      },
      error: (error) => this.toast.error("Oops! An error occurred while retrieving the announcement!")
    });
  }

  onSubmit() {
    (this.editForm as any).addControl('id', new FormControl(this.id, Validators.required));
    this.announcementService.editAnnouncement(this.editForm.value).subscribe({
      next: () => {
        this.toast.success("Announcement successfully updated!");
        this.location.back();
      },
      error: (err) =>this.toast.error("Oops! An error occurred while updating the announcement!")
    });
  }
}
