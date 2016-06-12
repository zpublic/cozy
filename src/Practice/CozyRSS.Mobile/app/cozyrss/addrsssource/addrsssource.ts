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
        contents: []
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