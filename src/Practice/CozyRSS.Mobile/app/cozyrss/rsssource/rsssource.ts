import {Page, NavParams, NavController} from 'ionic-angular';
import {RssDetailPage} from '../rssdetail/rssdetail';
import {RSSSource} from '../model';

@Page({
    templateUrl: './build/cozyrss/rsssource/rsssource.html'
})
export class RssSourcePage {
    source: RSSSource;

    constructor(public params: NavParams, public nav: NavController) {
        this.source = params.data.item;
    }

    itemSelected(item) {
        this.nav.push(RssDetailPage, { item: item });
    }
}