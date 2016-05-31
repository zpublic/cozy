import {NavController, NavParams} from 'ionic-angular';
import {Page} from 'ionic-angular';
import {BasicAlertPage} from './basic';

@Page({
  templateUrl: './build/pages/alerts/alerts.html'
})
export class AlertsPage {
  items = [];

  constructor(public nav: NavController) {
  }

  navBasicAlertPage(item) {
    this.nav.push(BasicAlertPage);
  }
}
