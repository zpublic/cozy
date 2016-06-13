import {Page, NavParams, NavController, Toast} from 'ionic-angular';
import {RSSContent} from '../model';
import {ModelService} from '../services/model.service';

@Page({
  templateUrl: './build/cozyrss/rssdetail/rssdetail.html'
})
export class RssDetailPage {
  content: RSSContent;

  constructor(private params: NavParams, private models: ModelService, private nav: NavController) {
    this.content = params.data.item;
  }

  addFavorite() {
    let _self = this;
    this.models.mapFavorite(function (favorite: RSSContent[]) {
      if (favorite.find(function (x) {
        return x.url == _self.content.url;
      }) == undefined) {
        favorite.push(_self.content);

        let toast = Toast.create({
          message: 'add favorite success',
          duration: 1000,
        });

        _self.nav.present(toast);
      } else {
        let toast = Toast.create({
          message: 'favorite is existing',
          duration: 1000,
        });

        _self.nav.present(toast);
      }
    })
  }

  copyToClipBoard() {

  }
}