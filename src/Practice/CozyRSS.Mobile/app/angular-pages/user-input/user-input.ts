import { Component } from '@angular/core';
import {Page} from 'ionic-angular';

@Page({
  templateUrl: './build/angular-pages/user-input/user-input.html'
})
export class UserInputAj2Page {
  clickMessage = 'click';
  onClickMe(){
    this.clickMessage ='You are my hero!';
  }
  
  values='';
  onKey(event:any) {
    this.values += event.target.value + ' | ';
  }
  
  values2='';
  onKey2(value:string) {
    this.values2 += value + ' | ';
  }
  
  values3=''
  values4=''
  
  heroes=['Windstorm', 'Bombasto', 'Magneta', 'Tornado'];
  addHero(newHero:string) {
    if (newHero) {
      this.heroes.push(newHero);
    }
  }
}