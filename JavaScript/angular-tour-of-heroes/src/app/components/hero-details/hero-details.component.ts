import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { HeroService }  from '../../services/hero/hero.service';
import { MessageService } from '../../services/message/message.service';
import { Hero } from '../../models/hero';

@Component({
  selector: 'app-hero-details',
  templateUrl: './hero-details.component.html',
  styleUrls: ['./hero-details.component.less']
})
export class HeroDetailsComponent implements OnInit {

  constructor(
    private heroService:HeroService,
    private messageService:MessageService,
    private route:ActivatedRoute,
    private location:Location
  ) { }

  hero:Hero;

  ngOnInit() {
    this.getHero();
  }

  goBack():void {
    this.location.back();
  }

  getHero():void {
    let id:number

    try {
      id = parseInt( this.route.snapshot.paramMap.get('id') );

    } catch (e) {
      this.messageService.add('Invalid Hero id. Could not fetch him.');
    }

    this.heroService.getHero(id)
                    .subscribe(hero => { this.hero = hero; console.log(this.hero) });
  }

}
