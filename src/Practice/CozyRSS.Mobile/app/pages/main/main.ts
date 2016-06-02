import {Page, Alert, NavController} from 'ionic-angular';
import {ActionSheetsPage} from '../action-sheets/action-sheets';
import {NavigationPage} from '../navigation/navigation';
import {IconsPage} from '../icons/icons';
import {AlertsPage} from '../alerts/alerts';
import {BadgesPage} from '../badges/badges';
import {CheckboxesPage} from '../checkboxes/checkboxes';
import {DatetimePage} from '../datetime/datetime';
import {GesturesPage} from '../gestures/gestures';
import {GridPage} from '../grid/grid';
import {LoadingPage} from '../loading/loading';
import {ModalsPage} from '../modals/modals';
import {RadiosPage} from '../radios/radios';
import {ButtonsPage} from '../buttons/buttons';
import {SearchBarsPage} from '../searchbars/searchbars'
import {SelectsPage} from '../selects/selects'
import {ToastPage} from '../toast/toast';
import {InputsPage} from '../inputs/inputs'

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
  
  navToast(){
    this.nav.push(ToastPage);
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
