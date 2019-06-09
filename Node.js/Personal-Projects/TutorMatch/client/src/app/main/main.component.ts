import { Component, OnInit } from '@angular/core';
import { TutorService } from './tutor.service';

import Tutor from '../../../../common/models/tutor';
import CityName from '../../../../common/enums/cityName';
import SortByEnum from '../../../../common/enums/sortByEnum';

@Component( {
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
} )
export class MainComponent implements OnInit {

  constructor( private tutorService: TutorService ) { }

  // TODO: Receive from the server.
  tutors: Tutor[];
  cityNames: any = Object.values( CityName ).filter( city => city !== CityName.All );
  sortByNames: any = Object.values( SortByEnum ).filter( sortBy => sortBy !== SortByEnum.None );
  filterBy: string = CityName.All;
  sortBy: string = SortByEnum.None;

  ngOnInit() {
    this.getAllTutors();
  }

  private getAllTutors(): void {
    this.tutorService.getAllTutors()
        .subscribe( tutors => this.tutors = tutors );
  }

  public changeFilterBy( cityName: string ): void {
    if ( cityName === this.filterBy )
      this.filterBy = CityName.All;
    else
      this.filterBy = cityName;
  }

  public sortTutorsBy( sortBy: string ): void {
    this.tutors = this.tutors.sort( ( a, b ) => {
      const x = a[sortBy].toLowerCase();
      const y = b[sortBy].toLowerCase();
      return x < y ? -1 : x > y ? 1 : 0;
    } );

    this.sortBy = sortBy;
  }

}
