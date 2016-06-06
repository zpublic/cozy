import {Page, NavParams} from 'ionic-angular';

@Page({
  templateUrl: './build/cozyrss/channeldetail/channeldetail.html'
})
export class ChannelDetailPage {
  title: string;
  source: string;
  channels = [
    {
      name: 'InfoQ-donet',
      url: 'http://www.infoq.com/cn/feed/dotnet/news',
      enable: true,
    },
    {
      name: '酷壳',
      url: 'http://coolshell.cn/feed',
      enable: false,
    },
    {
      name: '科学松鼠会',
      url: 'http://songshuhui.net/feed',
      enable: false,
    }
  ];

  constructor(public params: NavParams) {
    this.title = params.data.channel;
  }
}