import {App, Platform} from 'ionic-angular';
import {StatusBar} from 'ionic-native';
import {MainPage} from './cozyrss/main/main';
@App({
  template: '<ion-nav [root]="rootPage"></ion-nav>'
})

export class MyApp {
  rootPage: any = MainPage;
  constructor(platform: Platform) {
    platform.ready().then(() => {
      StatusBar.styleDefault();
    });
  }
}
