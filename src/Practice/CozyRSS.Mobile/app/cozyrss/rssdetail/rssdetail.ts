import {Page, NavParams} from 'ionic-angular';

@Page({
    templateUrl: './build/cozyrss/rssdetail/rssdetail.html'
})
export class RssDetailPage {

    title: string;

    constructor(private params: NavParams) {
        this.title = params.data.item.title;
    }
}