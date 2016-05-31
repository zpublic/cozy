import {NavController, NavParams} from 'ionic-angular';
import {Page} from 'ionic-angular';
import {BasicAlertPage} from './basic';
import {PromptAlertPage} from './prompt';
import {ConfirmAlertPage} from './confirm';
import {RadioAlertPage} from './radio';
import {CheckboxAlertPage} from './checkbox';

@Page({
  templateUrl: './build/pages/alerts/alerts.html'
})
export class AlertsPage {
  constructor(public nav: NavController) {
  }

  navBasicAlertPage() {
    this.nav.push(BasicAlertPage);
  }
  navPromptAlertPage() {
    this.nav.push(PromptAlertPage);
  }
  navConfirmAlertPage() {
    this.nav.push(ConfirmAlertPage);
  }
  navRadioAlertPage() {
    this.nav.push(RadioAlertPage);
  }
  navCheckboxAlertPage() {
    this.nav.push(CheckboxAlertPage);
  }
}
