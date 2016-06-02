import {Page, Alert, NavController} from 'ionic-angular';
import {ActionSheetsPage} from '../../pages/action-sheets/action-sheets';
import {NavigationPage} from '../../pages/navigation/navigation';
import {IconsPage} from '../../pages/icons/icons';
import {AlertsPage} from '../../pages/alerts/alerts';
import {BadgesPage} from '../../pages/badges/badges';
import {CheckboxesPage} from '../../pages/checkboxes/checkboxes';
import {DatetimePage} from '../../pages/datetime/datetime';
import {GesturesPage} from '../../pages/gestures/gestures';
import {GridPage} from '../../pages/grid/grid';
import {LoadingPage} from '../../pages/loading/loading';
import {ModalsPage} from '../../pages/modals/modals';
import {RadiosPage} from '../../pages/radios/radios';
import {ButtonsPage} from '../../pages/buttons/buttons';
import {SearchBarsPage} from '../../pages/searchbars/searchbars'
import {SelectsPage} from '../../pages/selects/selects'
import {ToastPage} from '../../pages/toast/toast';
import {ToastNativePage} from '../../native-pages/toast/toast';
import {SplashscreenService} from '../../native-pages/splashscreen/splashscreen';
import {DisplayDataAj2Page} from '../../angular-pages/display-data/display-data';
import {UserInputAj2Page} from '../../angular-pages/user-input/user-input';
import {FormsAj2Page} from '../../angular-pages/forms/forms';
import {InputsPage} from '../../pages/inputs/inputs'
import {SlidesPage} from '../../pages/slides/slides'
import {TogglesPage} from '../../pages/toggles/toggles'
import {CardsPage} from '../../pages/cards/cards'

@Page({
  templateUrl: './build/pages/main/main.html'
})
export class MainPage {
  constructor(public nav: NavController) {
  }
  
  navActionSheets(){
    this.nav.push(ActionSheetsPage);
  }
  
  navAlerts(){
    this.nav.push(AlertsPage);
  }
  
  navBadges(){
    this.nav.push(BadgesPage);
  }
  
  navButtons(){
    this.nav.push(ButtonsPage);
  }
  
  navCards(){
    this.nav.push(CardsPage);
  }
  
  navCheckBoxes(){
    this.nav.push(CheckboxesPage);
  }
  
  navDateTime(){
    this.nav.push(DatetimePage);
  }
  
  navGestures(){
    this.nav.push(GesturesPage);
  }
  
  navGrid(){
    this.nav.push(GridPage);
  }
  
  navIcons(){
    this.nav.push(IconsPage);
  }
  
  navInputs(){
    this.nav.push(InputsPage);
  }
  
  navLoading(){
    this.nav.push(LoadingPage);
  }
  
  navModals(){
    this.nav.push(ModalsPage);
  }
  
  navNavigation(){
    this.nav.push(NavigationPage);
  }
  
  navRadios(){
    this.nav.push(RadiosPage);
  }
  
  navSearchBars(){
    this.nav.push(SearchBarsPage);
  }
  
  navSelects(){
    this.nav.push(SelectsPage);
  }
  
  navSlides(){
    this.nav.push(SlidesPage);
  }
  
  navToast(){
    this.nav.push(ToastPage);
  }
  
  navToggles(){
    this.nav.push(TogglesPage);
  }
  
  navUndefined(){
    let alert = Alert.create({
      title: 'undefined',
      message: 'call to undefined page',
      buttons: ['Ok']
     });
     this.nav.present(alert);
  }
}
