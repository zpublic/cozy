import {Injectable} from '@angular/core';
import {File} from 'ionic-native';
import {RSSContent, RSSSource} from '../model';

@Injectable()
export class ModelService {
  private rssSourceList: RSSSource[];
  private rssFavoriteList: RSSContent[];

  private _saveFile(path, dir, file, data): Promise<any> {
    let resolveFn, rejectFn;

    let promise = new Promise<any>((resolve, reject) => {
      resolveFn = resolve;
      rejectFn = reject;
    });

    File.checkDir(path, dir)
      .catch(function (x) {
        return File.createDir(path, dir, false);
      })
      .then(function (x) {
        return File.createFile(path + dir, file, true);
      })
      .then(function (x) {
        return File.writeExistingFile(path + dir, file, JSON.stringify(data));
      })
      .then(function () {
        resolveFn();
      })
      .catch(function (error) {
        rejectFn(error);
      })

    return promise;
  }

  private _loadFile(path, dir, file): Promise<any> {
    let resolveFn, rejectFn;

    let promise = new Promise<any>((resolve, reject) => {
      resolveFn = resolve;
      rejectFn = reject;
    });

    File.checkDir(path, dir)
      .catch(function (x) {
        return File.createDir(path, dir, false);
      })
      .then(function (x) {
        return File.checkFile(path + dir, file);
      })
      .catch(function (error) {
        return File.createFile(path + dir, file, false);
      })
      .then(function (x) {
        return File.readAsText(path + dir, file);
      })
      .then(function (content) {
        if (content != null) {
          resolveFn(JSON.parse(content));
        }
        else {
          rejectFn({ error: 'load failed' });
        }
      })
      .catch(function (error) {
        rejectFn(error);
      })

    return promise;
  }

  saveSources(): Promise<any> {
    let path = 'file:///sdcard/';
    let dir = 'com.ionicframework.cozyrssmobile/';
    let file = 'rss_source.dat';

    return this._saveFile(path, dir, file, this.rssSourceList);
  }

  saveFavorite(): Promise<any> {
    let path = 'file:///sdcard/';
    let dir = 'com.ionicframework.cozyrssmobile/';
    let file = 'rss_favorite.dat';

    return this._saveFile(path, dir, file, this.rssFavoriteList);
  }

  loadSources(): Promise<any> {
    let path = 'file:///sdcard/';
    let dir = 'com.ionicframework.cozyrssmobile/';
    let file = 'rss_source.dat';

    return this._loadFile(path, dir, file);
  }

  loadFavorite(): Promise<any> {
    let path = 'file:///sdcard/';
    let dir = 'com.ionicframework.cozyrssmobile/';
    let file = 'rss_favorite.dat';

    return this._loadFile(path, dir, file);
  }

  loadAll(): Promise<any> {
    let resolveFn, rejectFn;
    let promise = new Promise<any>(function (resolve, reject) {
      resolveFn = resolve;
      rejectFn = reject;
    })

    let _self = this;

    Promise.all([_self.loadSources(), _self.loadFavorite()])
      .then(function (values) {
        _self.rssSourceList = values[0];
        _self.rssFavoriteList = values[1];
        return null;
      }).catch(function (error) {
        _self.rssSourceList = [];
        _self.rssFavoriteList = [];
        return _self.saveAll();
      }).then(function(x){
        resolveFn();
      }).catch(function(error){
        rejectFn(error);
      })

    return promise;
  }

  saveAll(): Promise<any> {
    return Promise.all([this.saveSources(), this.saveFavorite()]);
  }

  init(): Promise<any> {
    return this.loadAll();
  }

  getSources(): RSSSource[] {
    return this.rssSourceList;
  }

  mapSources(map): Promise<any> {
    map(this.rssSourceList);
    return this.saveSources();
  }

  getFavorite(): RSSContent[] {
    return this.rssFavoriteList;
  }

  mapFavorite(map): Promise<any> {
    map(this.rssFavoriteList);
    return this.saveFavorite();
  }

  testData() {
    this.rssSourceList = [
      {
        name: 'C++博客 原创精华区',
        url: 'http://www.baidu.com',
        enable: true,
        news: 11,
        channel: "科技",
        contents: [
          {
            title: '.NET Core计划弃用project.json',
            time: '十分钟前',
            author: 'InfoQ',
            content: 'Nam sodales elementum dolor non semper. Donec ac risus risus. Proin lacus nulla, bibendum aliquam nibh vel, viverra aliquam arcu. Ut eu tempus tellus. Maecenas euismod bibendum nisi, eget mollis urna sagittis sed. Duis molestie, metus ac facilisis pretium, urna nulla varius nisi, in volutpat tellus justo a neque. Sed ac mi dui. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed lacus dui, laoreet in urna eu, tristique lobortis odio. Donec dignissim erat metus, a molestie lorem vulputate eget. Sed vulputate dignissim purus, ut varius libero elementum at. Curabitur quis diam dui. Nullam a venenatis urna.'
          },
          {
            title: '由招聘要求看趋势 - VC++程序员路在何方',
            time: '十分钟前',
            author: 'C++博客',
            content: 'Nam sodales elementum dolor non semper. Donec ac risus risus. Proin lacus nulla, bibendum aliquam nibh vel, viverra aliquam arcu. Ut eu tempus tellus. Maecenas euismod bibendum nisi, eget mollis urna sagittis sed. Duis molestie, metus ac facilisis pretium, urna nulla varius nisi, in volutpat tellus justo a neque. Sed ac mi dui. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed lacus dui, laoreet in urna eu, tristique lobortis odio. Donec dignissim erat metus, a molestie lorem vulputate eget. Sed vulputate dignissim purus, ut varius libero elementum at. Curabitur quis diam dui. Nullam a venenatis urna.'
          },
        ]
      }
    ];

    this.rssFavoriteList = [
      {
        title: '.NET Core计划弃用project.json',
        time: '十分钟前',
        author: 'InfoQ',
        content: 'Nam sodales elementum dolor non semper. Donec ac risus risus. Proin lacus nulla, bibendum aliquam nibh vel, viverra aliquam arcu. Ut eu tempus tellus. Maecenas euismod bibendum nisi, eget mollis urna sagittis sed. Duis molestie, metus ac facilisis pretium, urna nulla varius nisi, in volutpat tellus justo a neque. Sed ac mi dui. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed lacus dui, laoreet in urna eu, tristique lobortis odio. Donec dignissim erat metus, a molestie lorem vulputate eget. Sed vulputate dignissim purus, ut varius libero elementum at. Curabitur quis diam dui. Nullam a venenatis urna.'
      },
    ];
  }

  constructor() {

  }
}