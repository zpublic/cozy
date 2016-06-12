import {File, Cordova, Plugin} from 'ionic-native';
import {Alert, NavController} from 'ionic-angular';
import {Injectable} from '@angular/core';
import {RSSContent, RSSSource} from '../model';

@Injectable()
export class StorageService {

  constructor(public nav: NavController) {

  }

  testPath(dir, file): Promise<any> {
    let resolveFn, rejectFn;
    let promise = new Promise(function (resolve, reject) {
      resolveFn = resolve;
      rejectFn = reject;
    });

    File.checkDir(dir, file)
      .then(function (fileSystem) {
        return resolveFn();
      })
      .catch(function (err) {
        if (err.code == 1) {
          return File.createDir(dir, file, false).then(function (x) {
            return resolveFn();
          }).catch(function (err) {
            return rejectFn({ error: 'create failed' });
          });
        }
      });
    return promise;
  };

  saveSources(sources: RSSSource[]): Promise<any> {
    let resolveFn, rejectFn;

    let promise = new Promise<any>((resolve, reject) => {
      resolveFn = resolve;
      rejectFn = reject;
    });

    this.testPath('file:///sdcard/', 'com.ionicframework.cozyrssmobile/')
      .then(function (x) {
        console.log("succ");
        resolveFn();
      }).catch(function (err) {
        console.log(err);
        rejectFn();
      })

    return promise;
  }
}
