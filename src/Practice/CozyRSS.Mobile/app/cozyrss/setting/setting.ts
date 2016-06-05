import {Page, Alert, NavController} from 'ionic-angular';

@Page({
  templateUrl: './build/cozyrss/setting/setting.html'
})
export class SettingPage {
  constructor(public nav: NavController) {

  }

  onAboutClicked() {
    let alert = Alert.create({
      title: '开发者:',
      message: 'zapline、Kingwl、MaxTan',
      buttons: ['好的']
    });

    this.nav.present(alert);
  }
}