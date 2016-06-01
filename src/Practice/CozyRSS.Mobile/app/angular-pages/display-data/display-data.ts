import { Component } from '@angular/core';
import {Page} from 'ionic-angular';

class Hero {
  constructor(
    public id:number,
    public name:string) { }
}

@Page({
  templateUrl: './build/angular-pages/display-data/display-data.html'
})
export class DisplayDataAj2Page {
  title: string;
  myHero: string;
  
  heroes = ['Windstorm', 'Bombasto', 'Magneta', 'Tornado'];
  heroes2 = [
  new Hero(1, 'Windstorm'),
  new Hero(13, 'Bombasto'),
  new Hero(15, 'Magneta'),
  new Hero(20, 'Tornado')
];

  constructor() {
    this.title = 'Tour of Heroes';
    this.myHero = 'Windstorm';
  }
}