import {Page, NavController, Toast} from 'ionic-angular';
import {ModelService} from '../services/model.service';

@Page({
  templateUrl: './build/cozyrss/addrsssource/addrsssource.html',
})
export class AddRSSSourcePage {
  channels = [
    "新闻",
    "科技",
    "生活",
    "财经",
    "体育",
    "娱乐",
    "博客",
    "其他",
  ];
  selectedChannel = this.channels[0];
  sourceName: string;
  sourceUrl: string;

  constructor(private nav: NavController, private models: ModelService) {

  }

  onChannelSelected(channel) {
    this.selectedChannel = channel;
  }

  onAddSourceDone() {
    let _self = this;

    this.models.mapSources(function (sources) {
      sources.push({
        name: _self.sourceName,
        url: _self.sourceUrl,
        enable: true,
        news: 0,
        channel: _self.selectedChannel,
        contents: [
           {
            title: '.NET Core计划弃用project.json',
            url: 'http://www.baidu.com',
            time: '十分钟前',
            author: 'InfoQ',
            content: 'Nam sodales elementum dolor non semper. Donec ac risus risus. Proin lacus nulla, bibendum aliquam nibh vel, viverra aliquam arcu. Ut eu tempus tellus. Maecenas euismod bibendum nisi, eget mollis urna sagittis sed. Duis molestie, metus ac facilisis pretium, urna nulla varius nisi, in volutpat tellus justo a neque. Sed ac mi dui. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed lacus dui, laoreet in urna eu, tristique lobortis odio. Donec dignissim erat metus, a molestie lorem vulputate eget. Sed vulputate dignissim purus, ut varius libero elementum at. Curabitur quis diam dui. Nullam a venenatis urna.'
          }
        ]
      });
    })

    let toast = Toast.create({
      message: 'add channel success',
      duration: 1000,
    })
    this.nav.present(toast);
    this.nav.pop();
  }
}