import {Page, NavController, ActionSheet} from 'ionic-angular';
import {RssDetailPage} from '../rssdetail/rssdetail';
import {ModelService} from '../services/model.service';
import {RSSContent} from '../model';

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

  itemHold(item) {
    let actionSheet = ActionSheet.create({
      title: 'actions',
      buttons: [
        {
          text: 'remove from favorite',
          role: 'remove',
          handler: () => {
            this.models.mapFavorite(function (favorite: RSSContent[]) {
              favorite.splice(favorite.indexOf(item), 1);
            })
          }
        }
      ],
    });

    this.nav.present(actionSheet);
  }
}