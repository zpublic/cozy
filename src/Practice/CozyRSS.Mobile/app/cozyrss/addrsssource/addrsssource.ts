import {Page} from 'ionic-angular';

@Page({
  templateUrl: './build/cozyrss/addrsssource/addrsssource.html'
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
  selectedChannel: any;

  onChannelSelected(channel) {
    this.selectedChannel = channel;
  }
}