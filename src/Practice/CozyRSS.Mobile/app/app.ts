import {App, Platform} from 'ionic-angular';
import {StatusBar} from 'ionic-native';
import {WelcomePage} from './cozyrss/welcome/welcome';
import {ModelService} from './cozyrss/services/model.service';

@App({
  template: '<ion-nav [root]="rootPage"></ion-nav>',
  providers: [ModelService],
})

export class MyApp {
  rootPage: any = WelcomePage;
  constructor(platform: Platform) {
    platform.ready().then(() => {
      StatusBar.styleDefault();
    });
  }
}
