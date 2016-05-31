import {App, Platform} from 'ionic-angular';
import {StatusBar} from 'ionic-native';
import {MainPage} from './pages/main/main';
import {ActionSheetsPage} from './pages/action-sheets/action-sheets';
import {NavigationPage} from './pages/navigation/navigation';
import {IconsPage} from './pages/icons/icons';


@App({
  template: '<ion-nav [root]="rootPage"></ion-nav>',
  config: {} // http://ionicframework.com/docs/v2/api/config/Config/
})
export class MyApp {
  rootPage: any = IconsPage;

  constructor(platform: Platform) {
    platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      StatusBar.styleDefault();
    });
  }
}
