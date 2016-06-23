import {Injectable} from '@angular/core';
import {Http, URLSearchParams} from '@angular/http';
import {FeedItem, RSSSource, RSSContent} from '../model';

@Injectable()
export class FeedService {

  private static get FEED_API_URL(): string { return "http://rss2json.com/api.json?rss_url=" };

  constructor(private http: Http) {

  }

  diffContent(source: RSSSource, items: FeedItem[]): RSSContent[] {
    let new_contents = items
      .filter(function (item) {
        return source.contents.find(function (content) {
          return content.url == item.guid;
        }) == undefined;
      })
      .map(function (item) {
        return {
          title: item.title,
          url: item.guid,
          time: item.pubDate,
          author: item.author,
          content: item.content,
          read: false,
        };
      });

    return new_contents;
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