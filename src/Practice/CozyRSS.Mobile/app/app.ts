import {App, Platform} from 'ionic-angular';
import {StatusBar} from 'ionic-native';
import {MainPage} from './pages/main/main';
import {ActionSheetsPage} from './pages/action-sheets/action-sheets';
import {NavigationPage} from './pages/navigation/navigation';
import {IconsPage} from './pages/icons/icons';
import {AlertsPage} from './pages/alerts/alerts';
import {BadgesPage} from './pages/badges/badges';
import {CheckboxesPage} from './pages/checkboxes/checkboxes';
import {DatetimePage} from './pages/datetime/datetime';
import {GesturesPage} from './pages/gestures/gestures';
import {GridPage} from './pages/grid/grid';
import {LoadingPage} from './pages/loading/loading';
import {ModalsPage} from './pages/modals/modals';
import {RadiosPage} from './pages/radios/radios';
import {ToastPage} from './pages/toast/toast';
import {ToastNativePage} from './native-pages/toast/toast';
import {SplashscreenService} from './native-pages/splashscreen/splashscreen';
import {DisplayDataAj2Page} from './angular-pages/display-data/display-data';
import {UserInputAj2Page} from './angular-pages/user-input/user-input';
import {FormsAj2Page} from './angular-pages/forms/forms';

@App({
  template: '<ion-nav [root]="rootPage"></ion-nav>',
  config: {} // http://ionicframework.com/docs/v2/api/config/Config/
})
export class MyApp {
  rootPage: any = MainPage;

  constructor(platform: Platform) {
    platform.ready().then(() => {
      SplashscreenService.show();
      SplashscreenService.hide();
      StatusBar.styleDefault();
    });
  }
}
