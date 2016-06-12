import {Page, NavController, Toast} from 'ionic-angular';
import {RssSourcePage} from '../rsssource/rsssource';
import {FavoritePage} from '../favorite/favorite';
import {RSSSource} from '../model';
import {ModelService} from '../services/model.service';

@Page({
  templateUrl: './build/cozyrss/rsslist/rsslist.html',
})
export class RssListPage {
  rssList: RSSSource[];

  constructor(private nav: NavController, private models: ModelService) {
    this.rssList = this.models.getSources();
  }

  itemSelected(item) {
    this.nav.push(RssSourcePage, { item: item });
  }

  onFavorite() {
    this.nav.push(FavoritePage);
  }
}