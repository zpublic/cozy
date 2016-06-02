import {App, Platform} from 'ionic-angular';
import {StatusBar} from 'ionic-native';
import {MainPage} from './pages/main/main';
import {ToastNativePage} from './native-pages/toast/toast';
import {SplashscreenService} from './native-pages/splashscreen/splashscreen';
import {DisplayDataAj2Page} from './angular-pages/display-data/display-data';
import {UserInputAj2Page} from './angular-pages/user-input/user-input';
import {FormsAj2Page} from './angular-pages/forms/forms';
import {CozyRssMainPage} from './cozyrss/mainpage'
@App({
  template: '<ion-nav [root]="rootPage"></ion-nav>',
  config: {} // http://ionicframework.com/docs/v2/api/config/Config/
})

export class MyApp {
  rootPage: any = CozyRssMainPage;
  constructor(platform: Platform) {
    platform.ready().then(() => {
      StatusBar.styleDefault();
    });
  }
}
