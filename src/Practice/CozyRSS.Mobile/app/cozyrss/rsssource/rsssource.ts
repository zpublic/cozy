import {Page, NavParams, NavController} from 'ionic-angular';
import {RssDetailPage} from '../rssdetail/rssdetail'

@Page({
    templateUrl: './build/cozyrss/rsssource/rsssource.html'
})
export class RssSourcePage {
    items = [
        {
            title: '由招聘要求看趋势 - VC++程序员路在何方',
            time: '十分钟前',
            source: 'C++博客',
            content:'',
        },
        {
            title: '有效利用标准库提供的type_traits，让程序在编译时作出分支选择',
            time: '昨天 12：21',
            source: 'c++博客',
            content:'',
        },
        {
            title: '.NET Core计划弃用project.json',
            time: '十分钟前',
            source: 'InfoQ',
            content:'Nam sodales elementum dolor non semper. Donec ac risus risus. Proin lacus nulla, bibendum aliquam nibh vel, viverra aliquam arcu. Ut eu tempus tellus. Maecenas euismod bibendum nisi, eget mollis urna sagittis sed. Duis molestie, metus ac facilisis pretium, urna nulla varius nisi, in volutpat tellus justo a neque. Sed ac mi dui. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed lacus dui, laoreet in urna eu, tristique lobortis odio. Donec dignissim erat metus, a molestie lorem vulputate eget. Sed vulputate dignissim purus, ut varius libero elementum at. Curabitur quis diam dui. Nullam a venenatis urna.',
        }
    ];
    title: string;

    constructor(public params: NavParams, public nav: NavController) {
        this.title = params.data.item.title;
    }

    itemSelected(item) {
        this.nav.push(RssDetailPage, { item: item });
    }
}