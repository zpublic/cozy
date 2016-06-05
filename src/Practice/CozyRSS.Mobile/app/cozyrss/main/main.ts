import {Page, NavController} from 'ionic-angular';
import {RssListPage} from '../rsslist/rsslist';
import {SettingPage} from '../setting/setting';
import {RssChannelPage} from '../rsschannel/rsschannel';
import {FavoritePage} from '../favorite/favorite'

@Page({
	templateUrl: './build/cozyrss/main/mian.html'
})
export class MainPage {
	
	rssList: any = RssListPage;
	rsschannel: any = RssChannelPage;
	setting: any = SettingPage;
	
	constructor(public nav: NavController) {
  }

	onFavorite(){
		this.nav.push(FavoritePage);
	}
}