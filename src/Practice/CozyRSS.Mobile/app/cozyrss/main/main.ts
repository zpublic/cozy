import {Page, NavController} from 'ionic-angular';
import {RssListPage} from '../rsslist/rsslist';
import {SettingPage} from '../setting/setting';
import {RssChannelPage} from '../rsschannel/rsschannel';

@Page({
	templateUrl: './build/cozyrss/main/mian.html'
})
export class MainPage {

	constructor(public nav: NavController) {
  }

	rssList: any = RssListPage;
	rsschannel: any = RssChannelPage;
	setting: any = SettingPage;

}