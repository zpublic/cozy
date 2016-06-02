import {Page, NavController} from 'ionic-angular';

@Page({
    templateUrl: './build/pages/tabs/tabs.html'
})
class TabPage{
    SelfId = "";
    
    constructor(private nav: NavController){
        this.SelfId = this.nav.id;
    }
}

@Page({
    templateUrl: './build/pages/tabs/tabs-badge.html'
})
export class TabsPage{
    tabOne = TabPage;
    tabTwo = TabPage;
    tabThree = TabPage;
    BadgeCountOne = 3;
    BadgeCountTwo = 14;
    BadgeCountThree = 1;
}