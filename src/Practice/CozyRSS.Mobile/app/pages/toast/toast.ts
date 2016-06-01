import {Page, NavController, Toast} from 'ionic-angular';


@Page({
  templateUrl: './build/pages/toast/toast.html'
})
export class ToastPage {
  constructor(
    public nav: NavController
  ) { }
  showToast() {
    let toast = Toast.create({
      message: 'Mmmm, buttered toast',
      duration: 3000
    });

    this.nav.present(toast);
  }
}
