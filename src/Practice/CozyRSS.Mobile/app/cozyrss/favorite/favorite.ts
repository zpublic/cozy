import {Page, NavController} from 'ionic-angular';
import {RssDetailPage} from '../rssdetail/rssdetail';
import {ModelService} from '../services/model.service';

@Page({
  templateUrl: './build/cozyrss/favorite/favorite.html',
})
export class FavoritePage {
  items: any;

  constructor(private models: ModelService, private nav: NavController) {
    this.items = this.models.getFavorite();
  }

  itemSelected(item) {
    this.nav.push(RssDetailPage, { item: item });
  }
}