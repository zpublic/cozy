import {Page, NavController} from 'ionic-angular';
import {Toast} from 'ionic-native';


@Page({
  templateUrl: './build/native-pages/toast/toast.html'
})
export class ToastNativePage {
  constructor(
    public nav: NavController
  ) { }
  showToast() {
    Toast.show("I'm a toast", "5000", "center").subscribe(
    toast => {
        console.log(toast);
    });
  }
}




