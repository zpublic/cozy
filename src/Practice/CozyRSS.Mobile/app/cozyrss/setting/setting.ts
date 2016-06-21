import {Page, Alert, NavController} from 'ionic-angular';
import {Http} from '@angular/http';

@Page({
  templateUrl: './build/cozyrss/setting/setting.html'
})
export class SettingPage {
  constructor(public nav: NavController, private http: Http) {

  }

  onAboutClicked() {
    let alert = Alert.create({
      title: '开发者:',
      message: 'zapline、Kingwl、MaxTan',
      buttons: ['好的']
    });

    this.nav.present(alert);
  }

  onHttpTest() {
    this.http.get('http://www.baidu.com').subscribe(res => {
      console.log(res);
      this.nav.present(Alert.create({
        title: 'http test',
        message: res.text(),
        buttons: ['ok']
      }))
    });
  }
}