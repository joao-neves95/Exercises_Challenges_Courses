import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [HomeComponent]
})
export class AppModule { }
