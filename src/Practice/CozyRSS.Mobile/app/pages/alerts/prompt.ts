import {Page, Alert, NavController} from 'ionic-angular';


@Page({
  templateUrl: './build/pages/alerts/prompt.html'
})
export class PromptAlertPage {

  constructor(public nav: NavController) { }

  doPrompt() {
    let prompt = Alert.create({
      title: 'Login',
      message: "Enter a name for this new album you're so keen on adding",
      inputs: [
        {
          name: 'title',
          placeholder: 'Title'
        },
      ],
      buttons: [
        {
          text: 'Cancel',
          handler: data => {
            console.log('Cancel clicked');
          }
        },
        {
          text: 'Save',
          handler: data => {
            console.log('Saved clicked');
          }
        }
      ]
    });
    this.nav.present(prompt);
  }

}
