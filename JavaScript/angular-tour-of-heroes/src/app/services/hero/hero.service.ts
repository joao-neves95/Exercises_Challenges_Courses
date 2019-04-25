import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { MessageService } from '../message/message.service';
import { HeroesDB } from '../../heroesMockDB';
import { Hero } from '../../models/hero';
import { type } from 'os';

@Injectable({
  providedIn: 'root'
})
export class HeroService {

  constructor(private messageService:MessageService) { }

  getHero(id:number):Observable<Hero> {
    this.messageService.add(`HeroService: fetched hero ${id}`);
    console.debug(HeroesDB.find(hero => hero.id === id))
    return of(HeroesDB.find(hero => hero.id === id));
  }

  getHeroes():Observable<Hero[]> {
    this.messageService.add('HeroService: fetched heroes');
    return of(HeroesDB);
  }

}
