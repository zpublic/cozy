import {Page, NavController, Loading} from 'ionic-angular';
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

  private _updateSource(source: RSSSource, items: FeedItem[]) {
    let count = 0;
    let contents = items.map(function (item): RSSContent {
      if (source.contents.find(function (content) {
        return content.url == item.guid;
      }) == undefined) {
        count++;
      }
      return {
        title: item.title,
        url: item.guid,
        time: item.pubDate,
        author: item.author,
        content: item.content,
      };
    });

    source.news = count;
    source.contents = contents;

    return this.models.saveSources();
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

      let source = x;
      _self.feeds.readFeed(source.url)
        .then(function (data) {
          return _self._updateSource(x, data.items);
        })
        .catch(function (error) {
          console.log(error);
        })
        .then(function (x) {
          loading.dismiss();
        });
    })
  }
}