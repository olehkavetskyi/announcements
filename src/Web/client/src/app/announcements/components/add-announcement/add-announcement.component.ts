import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AnnouncementService } from '../../services/announcement.service';
import { CustomInputComponent } from '../../../shared/components/custom-input/custom-input.component';
import { AnnouncementToAddDto } from '../../models/announcement';

@Component({
  selector: 'app-add-announcement',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CustomInputComponent,
  ],
  providers: [
    AnnouncementService,
  ],
  templateUrl: './add-announcement.component.html',
  styleUrl: './add-announcement.component.scss'
})
export class AddAnnouncementComponent {

  @Output() announcementAdded = new EventEmitter<any>();
  
  constructor (private fb: FormBuilder, private announcementService: AnnouncementService) {  }

  addForm = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required]
  })

  onSubmit(): void {
    this.announcementService.addAnnouncement(this.addForm.value as AnnouncementToAddDto).subscribe({
      next: (response) => {
        this.announcementAdded.emit(response);
        this.addForm.reset({
          title: '',
          description: '',
        })
      },
      error: (err) => console.log('err')
    });
  }
}
