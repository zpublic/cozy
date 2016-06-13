import {Page, NavParams} from 'ionic-angular';
import {ModelService} from '../services/model.service';

@Page({
  templateUrl: './build/cozyrss/channeldetail/channeldetail.html'
})
export class ChannelDetailPage {
  channel: string;
  sources = [];

  constructor(private params: NavParams, private models: ModelService) {
    this.channel = params.data.channel;
    this.sources = this.models.getSources();
  }
}