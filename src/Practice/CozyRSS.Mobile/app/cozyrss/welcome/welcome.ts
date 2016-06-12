//
//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0\  =  /0
//                    ___/`---'\___
//                  .' \\|     |// '.
//                 / \\|||  :  |||// \
//                / _||||| -:- |||||- \
//               |   | \\\  -  /// |   |
//               | \_|  ''\---/''  |_/ |
//               \  .-\__  '-'  ___/-. /
//             ___'. .'  /--.--\  `. .'___
//          ."" '<  `.___\_<|>_/___.' >' "".
//         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//         \  \ `_.   \_ __\ /__ _/   .-` /  /
//     =====`-.____`.___ \_____/___.-`___.-'=====
//                       `=---='
//
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//
//               佛祖保佑         永无BUG
//
//
//

import {Page, NavController, Loading, Toast} from 'ionic-angular';
import {MainPage} from '../main/main';
import {ModelService} from '../services/model.service';

@Page({
  templateUrl: './build/cozyrss/welcome/welcome.html',
})
export class WelcomePage {
  constructor(private nav: NavController, private models: ModelService) {
    let _self = this;

    let loading = Loading.create({
      content: 'Please wait...',
      duration: 5000,
    });
    this.nav.present(loading);

    _self.models.init()
      .then(function (x) {
        let toast = Toast.create({
          message: 'Load Success',
          duration: 3000
        });
        _self.nav.present(toast);
      })
      .catch(function (error) {
        let toast = Toast.create({
          message: 'Load Failed : ' + JSON.stringify(error),
          duration: 3000
        });
      })
      .then(function (x) {
        loading.dismiss();
        _self.nav.push(MainPage);
      });
  }
}