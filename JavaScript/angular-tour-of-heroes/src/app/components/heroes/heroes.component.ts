import { Component, OnInit } from '@angular/core';
import { Hero } from '../../models/hero';
import { HeroService } from '../../services/hero/hero.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.less']
})
export class HeroesComponent implements OnInit {

  constructor(private heroService:HeroService) { }

  heroes:Hero[];
  selectedHero:Hero;

  ngOnInit() {
    this.getAllHeroes();
  }

  getAllHeroes() {
    this.heroService.getHeroes()
                    .subscribe(heroes => this.heroes = heroes);
  }

}
