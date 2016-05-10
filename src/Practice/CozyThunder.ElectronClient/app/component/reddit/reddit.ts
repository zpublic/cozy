import { Component } from "angular2/core";
import { NgFor } from "angular2/common";
import { Model } from "../model";
import { ArticleComponent } from "../article/article";

@Component({
    selector: 'reddit',
    directives: [ArticleComponent],
    templateUrl:'build/component/reddit/reddit.html'
})
export class RedditApp {
    articles: Model[];

    constructor() {
        this.articles = [
            new Model('Angular 2', 'http://angular.io', 3),
            new Model('Fullstack', 'http://fullstack.io', 2),
            new Model('Angular Homepage', 'http://angular.io', 1),
        ];
    }

    addArticle(title: HTMLInputElement, link: HTMLInputElement): void {
        console.log(`Adding article title: ${title.value} and link: ${link.value}`);
        this.articles.push(new Model(title.value, link.value, 0));
        title.value = '';
        link.value = '';
    }

    sortedArticles(): Model[] {
        return this.articles.sort((a: Model, b: Model) => b.votes - a.votes);
    }

}