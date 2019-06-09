import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import Tutor from '../../../../common/models/tutor';

@Injectable({
  providedIn: 'root'
})
export class TutorService {

  constructor( private http: HttpClient ) { }

  private TUTORS_API_URL: string = 'api/tutors';

  getAllTutors(): Observable<Tutor[]> {
    return this.http.get<Tutor[]>( this.TUTORS_API_URL );
  }
}
