import {Page, NavController, Loading, Toast} from 'ionic-angular';
import {RssSourcePage} from '../rsssource/rsssource';
import {FavoritePage} from '../favorite/favorite';
import {RSSSource, RSSContent, FeedItem} from '../model';
import {ModelService} from '../services/model.service';
import {FeedService} from '../services/feed.service';
import 'rxjs';

@Page({
  templateUrl: './build/cozyrss/rsslist/rsslist.html',
})
export class RssListPage {
  rssList: RSSSource[];

  constructor(private nav: NavController, private models: ModelService, private feeds: FeedService) {
    this.rssList = this.models.getSources();
  }


  itemSelected(item) {
    this.nav.push(RssSourcePage, { item: item });
  }

  onFavorite() {
    this.nav.push(FavoritePage);
  }

  onRefresh() {
    let loading = Loading.create({
      content: 'refresh rss source',
      dismissOnPageChange: true,
    });

    this.nav.present(loading);

    let _self = this;
    this.rssList.filter(function (x) {
      return x.enable;
    }).forEach(function (x) {

      _self.feeds.readFeed(x.url)
        .then(function (data) {
          return _self.feeds.diffContent(x, data.items);
        })
        .then(function (content) {
          return _self.models.mapSources(function (sources: RSSSource[]) {
            x.news += content.length;
            x.contents = x.contents.concat(content);
          });
        })
        .then(function (param) {
          let toast = Toast.create({
            message: 'refresh success',
          });
          _self.nav.present(toast);
        })
        .catch(function (err) {
          let toast = Toast.create({
            message: JSON.stringify(err),
          });
          _self.nav.present(toast);
        })
        .then(function (param) {
          loading.dismiss();
        })
    })
  }
}