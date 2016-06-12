import {Page, NavController} from 'ionic-angular';
import {RssSourcePage} from '../rsssource/rsssource';
import {FavoritePage} from '../favorite/favorite';
import {RSSSource} from '../model';

@Page({
  templateUrl: './build/cozyrss/rsslist/rsslist.html'
})
export class RssListPage {
  rssList: RSSSource[];

  testData() {
    this.rssList = [
      {
        name: 'C++博客 原创精华区',
        url: 'http://www.baidu.com',
        enable: true,
        channel: "科技",
        contents: [
          {
            title: '.NET Core计划弃用project.json',
            time: '十分钟前',
            author: 'InfoQ',
            content: 'Nam sodales elementum dolor non semper. Donec ac risus risus. Proin lacus nulla, bibendum aliquam nibh vel, viverra aliquam arcu. Ut eu tempus tellus. Maecenas euismod bibendum nisi, eget mollis urna sagittis sed. Duis molestie, metus ac facilisis pretium, urna nulla varius nisi, in volutpat tellus justo a neque. Sed ac mi dui. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed lacus dui, laoreet in urna eu, tristique lobortis odio. Donec dignissim erat metus, a molestie lorem vulputate eget. Sed vulputate dignissim purus, ut varius libero elementum at. Curabitur quis diam dui. Nullam a venenatis urna.'
          },
          {
            title: '由招聘要求看趋势 - VC++程序员路在何方',
            time: '十分钟前',
            author: 'C++博客',
            content: ''
          },
        ]
      }
    ];
  }

  constructor(public nav: NavController) {
    this.testData();
  }

  itemSelected(item) {
    this.nav.push(RssSourcePage, { item: item });
  }

  onFavorite() {
    this.nav.push(FavoritePage);
  }
}