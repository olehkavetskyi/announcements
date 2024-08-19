import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Announcement, AnnouncementToAddDto } from '../models/announcement';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {
  apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllAnnouncements(): Observable<Announcement[]> {
    return this.http.get<Announcement[]>(`${this.apiUrl}/announcement/all`);
  }

  getSimilarAnnouncements(id: string): Observable<Announcement[]> {
    return this.http.get<Announcement[]>(`${this.apiUrl}/announcement/similar/${id}`);
  }

  getAnnouncementById(id: string): Observable<Announcement> {
    return this.http.get<Announcement>(`${this.apiUrl}/announcement/${id}`);
  }

  addAnnouncement(announcement: AnnouncementToAddDto): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/announcement/add`, announcement);
  }

  editAnnouncement(announcement: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/announcement/update?id=${announcement.id}`, announcement);
  }

  deleteAnnouncement(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/announcement/delete/${id}`);
  }
}
