import {Page, NavParams} from 'ionic-angular';
import {RSSContent} from '../model';

@Page({
  templateUrl: './build/cozyrss/rssdetail/rssdetail.html'
})
export class RssDetailPage {
  content: RSSContent;

  constructor(public params: NavParams) {
    this.content = params.data.item;
  }
}