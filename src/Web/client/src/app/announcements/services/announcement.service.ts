import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Announcement } from '../models/announcement';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {
  apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllAnnouncements(): Observable<Announcement[]> {
    return this.http.get<Announcement[]>(`${this.apiUrl}/announcement/all`)
  }
}
