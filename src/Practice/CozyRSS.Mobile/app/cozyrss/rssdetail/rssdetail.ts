import {Page, NavParams} from 'ionic-angular';

@Page({
  templateUrl: './build/cozyrss/rssdetail/rssdetail.html'
})
export class RssDetailPage {
  item: any;

  constructor(public params: NavParams) {
    this.item = params.data.item;
    console.log(JSON.stringify(this.item));
  }
}