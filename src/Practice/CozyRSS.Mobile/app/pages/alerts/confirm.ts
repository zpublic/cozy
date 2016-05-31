import {Page, Alert, NavController} from 'ionic-angular';


@Page({
  templateUrl: './build/pages/alerts/confirm.html'
})
export class ConfirmAlertPage {

  constructor(public nav: NavController) { }

  doConfirm() {
    let confirm = Alert.create({
      title: 'Use this lightsaber?',
      message: 'Do you agree to use this lightsaber to do good across the intergalactic galaxy?',
      buttons: [
        {
          text: 'Disagree',
          handler: () => {
            console.log('Disagree clicked');
          }
        },
        {
          text: 'Agree',
          handler: () => {
            console.log('Agree clicked');
          }
        }
      ]
    });
    this.nav.present(confirm);
  }

}
