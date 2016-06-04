import {Page, NavController} from 'ionic-angular';
import {RssListPage} from '../rsslist/rsslist';
import {SettingPage} from '../setting/setting';

@Page({
	templateUrl: './build/cozyrss/main/mian.html'
})
export class MainPage {

	constructor(public nav: NavController) {
  }

	rssList: any = RssListPage;
	setting: any = SettingPage;

}