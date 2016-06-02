import {Page} from 'ionic-angular';

@Page({
    templateUrl: './build/pages/cards/cards.html'
})
export class CardsPage{
    lists=[
        {
            ico: 'cart',
            content: 'Shopping',
        },
        {
            ico: 'medical',
            content: 'Hospital',
        },
        {
            ico: 'cafe',
            content: 'Cafe',
        },
        {
            ico: 'paw',
            content: 'Dog Park',
        },
        {
            ico: 'beer',
            content: 'Pub',
        },
        {
            ico: 'planet',
            content: 'Space',
        },        
    ];
}