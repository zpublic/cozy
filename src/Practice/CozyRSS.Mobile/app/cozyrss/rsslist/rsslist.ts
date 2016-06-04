import {Page, NavController} from 'ionic-angular';
import {RssDetailPage} from '../rssdetail/rssdetail';

@Page({
    templateUrl: './build/cozyrss/rsslist/rsslist.html'
})
export class RssListPage {


    constructor(public nav: NavController) {
    }

    items = [
        {
            title: 'Pokémon Yellow',
        },
        {
            title: 'Super Metroid',
        },
        {
            title: 'Mega Man X',
        },
        {
            title: 'The Legend of Zelda',
        },
        {
            title: 'Pac-Man',
        },
    ];

    itemSelected(item) {
        this.nav.push(RssDetailPage, { item: item });
    }
}