import {Page, Loading, NavController} from 'ionic-angular';


@Page({
  templateUrl: './build/pages/loading/loading.html'
})
export class LoadingPage {

  constructor(public nav: NavController) { }

  presentLoading() {
    let loading = Loading.create({
      content: "Please wait...",
      duration: 3000,
      dismissOnPageChange: true
    });
    this.nav.present(loading);
  }

}
