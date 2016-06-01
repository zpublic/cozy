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
import {ButtonsPage} from './pages/buttons/buttons';
import {SearchBarsPage} from './pages/searchbars/searchbars'

@App({
  template: '<ion-nav [root]="rootPage"></ion-nav>',
  config: {} // http://ionicframework.com/docs/v2/api/config/Config/
})
export class MyApp {
  rootPage: any = SearchBarsPage;

  constructor(platform: Platform) {
    platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      StatusBar.styleDefault();
    });
  }
}
