import {Page, NavController} from 'ionic-angular';
import {ChannelDetailPage} from '../channeldetail/channeldetail';
import {AddRSSSourcePage} from '../addrsssource/addrsssource';

@Page({
  templateUrl: './build/cozyrss/rsschannel/rsschannel.html'
})
export class RssChannelPage {
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

  constructor(public nav: NavController) {

  }

  channelSelected(channel) {
    this.nav.push(ChannelDetailPage, { channel: channel });
  }
  
  onAddChannelClicked(){
    this.nav.push(AddRSSSourcePage);
  }
}