import {Page, Alert, NavController} from 'ionic-angular';


@Page({
  templateUrl: './build/pages/alerts/basic.html'
})
export class BasicAlertPage {

  constructor(public nav: NavController) { }

  doAlert() {
    let alert = Alert.create({
      title: 'New Friend!',
      message: 'Your friend, Obi wan Kenobi, just approved your friend request!',
      buttons: ['Ok']
    });
    this.nav.present(alert);
  }

}
