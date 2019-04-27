import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { MessageService } from '../message/message.service';
import { Hero } from '../../models/hero';

const httpOptions:object = {
  headers: new HttpHeaders({"Content-Type": "application/json"})
};

@Injectable({
  providedIn: 'root'
})
export class HeroService {

  constructor(
    private http:HttpClient,
    private messageService:MessageService) { }

  private heroesApiUrl = 'api/heroes';


  private log(message:string) {
    this.messageService.add(`HeroService: ${message}`);
  }

  private handleError<T>(operation = 'Operation', result?:T) {
    return (error:any):Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    }
  }

  getHero(id:number):Observable<Hero> {
    // this.messageService.add(`HeroService: fetched hero ${id}`);
    // return of(HeroesDB.find(hero => hero.id === id));
    return this.http.get<Hero>(`${this.heroesApiUrl}/${id}`)
                    .pipe(
                      tap(_ => this.log(`Fetched hero id=${id}`)),
                      catchError(this.handleError<Hero>(`getHero id=${id}`))
                    );
  }

  getHeroes():Observable<Hero[]> {
    // this.messageService.add('HeroService: fetched heroes');
    return this.http.get<Hero[]>(this.heroesApiUrl)
                    .pipe(
                      tap(_ => this.log('Fetched heroes')),
                      catchError(this.handleError<Hero[]>('getHeroes', []))
                    );
  }

  updateHero(hero:Hero):Observable<any> {
    return this.http.put(this.heroesApiUrl, hero, httpOptions)
                    .pipe(
                      tap(_ => this.log(`Updated hero id=${hero.id}`)),
                      catchError(this.handleError<any>('updateHero'))
                    );
  }

  addHero(hero:Hero):Observable<any> {
    return this.http.post(this.heroesApiUrl, hero, httpOptions)
                    .pipe(
                      tap(_ => this.log(`Added hero id=${hero.id}`)),
                      catchError(this.handleError<Hero>('addHero'))
                    );
  }

  deleteHero(id:number):Observable<any> {
    return this.http.delete(`${this.heroesApiUrl}/${id}`, httpOptions)
                    .pipe(
                      tap(_ => this.log(`Deleted hero id=${id}`)),
                      catchError(this.handleError<Hero>('addHero'))
                    );
  }

  searchHeroes(term:string):Observable<Hero[]> {
    if(!term.trim())
      return of([]);

      return this.http.get<Hero[]>(`${this.heroesApiUrl}/?name=${term}`)
                      .pipe(
                        tap(_ => this.log(`Found heroes matching "${term}"`)),
                        catchError(this.handleError<Hero[]>('searchHeroes', []))
                      );
  }

}
