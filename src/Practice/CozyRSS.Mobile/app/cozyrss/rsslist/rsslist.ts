import {Page, NavController} from 'ionic-angular';
import {RssSourcePage} from '../rsssource/rsssource';
import {FavoritePage} from '../favorite/favorite';

@Page({
  templateUrl: './build/cozyrss/rsslist/rsslist.html'
})
export class RssListPage {

  constructor(public nav: NavController) {

  }

  items = [
    {
      title: 'C++博客 原创精华区',
      num: 0,
      icon: 'arrow-forward',
      isAdd: false,
    },
    {
      title: '虎嗅网',
      num: 10,
      icon: 'arrow-forward',
      isAdd: false,
    },
    {
      title: '知乎日报',
      num: 2,
      icon: 'arrow-forward',
      isAdd: false,
    },

    {
      title: '从Url添加',
      num: 0,
      icon: 'add',
      isAdd: true,
    }
  ];

  itemSelected(item) {
    if (item.isAdd) {

    } else {
      this.nav.push(RssSourcePage, { item: item });
    }
  }

  onFavorite() {
    this.nav.push(FavoritePage);
  }
}