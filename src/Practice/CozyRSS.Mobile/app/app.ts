import {App, Platform} from 'ionic-angular';
import {StatusBar} from 'ionic-native';
import {WelcomePage} from './cozyrss/welcome/welcome';
import {ModelService} from './cozyrss/services/model.service';
import {FileService} from './cozyrss/services/file.service';
import {FeedService} from './cozyrss/services/feed.service';

@App({
  template: '<ion-nav [root]="rootPage"></ion-nav>',
  providers: [ModelService, FileService, FeedService],
})

export class MyApp {
  rootPage: any = WelcomePage;
  constructor(platform: Platform) {
    platform.ready().then(() => {
      StatusBar.styleDefault();
    });
  }
}
