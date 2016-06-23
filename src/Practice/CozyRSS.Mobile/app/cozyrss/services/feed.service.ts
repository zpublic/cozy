import {Injectable} from '@angular/core';
import {Http, URLSearchParams} from '@angular/http';

@Injectable()
export class FeedService {

  private static get FEED_API_URL(): string { return "http://rss2json.com/api.json?rss_url=" };

  constructor(private http: Http) {

  }

  readFeed(url: string): Promise<any> {
    let resolveFn, rejectFn;
    let promise = new Promise(function (resolve, reject) {
      resolveFn = resolve;
      rejectFn = reject;
    });

    let proxyUrl = FeedService.FEED_API_URL + url;

    this.http.get(proxyUrl)
      .subscribe(function (data) {
        let obj = data.json();
        if (obj.status == 'ok') {
          resolveFn(obj);
        } else {
          rejectFn(obj);
        }
      }, function (error) {
        rejectFn(error);
      });

    return promise;
  }
}