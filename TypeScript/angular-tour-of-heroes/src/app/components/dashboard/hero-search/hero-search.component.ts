import { Component, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import {
  debounceTime, distinctUntilChanged, switchMap
} from 'rxjs/operators';

import { Hero } from '../../../models/hero';
import { HeroService } from '../../../services/hero/hero.service';

@Component({
  selector: 'app-hero-search',
  templateUrl: './hero-search.component.html',
  styleUrls: ['./hero-search.component.less']
})
export class HeroSearchComponent implements OnInit {

  constructor(private heroService: HeroService) { }

  heroes$: Observable<Hero[]>;
  private searchTerms = new Subject<string>();

  ngOnInit() {
    this.heroes$ = this.searchTerms.pipe(
      // wait 300ms after each keystroke before considering the term
      debounceTime(300),

      // ignore new term if same as previous term
      distinctUntilChanged(),

      // switch to new search observable each time the term changes
      switchMap((term:string) => this.heroService.searchHeroes(term)),
    );
  }

  search(term:string):void {
    this.searchTerms.next(term);
  }

}
