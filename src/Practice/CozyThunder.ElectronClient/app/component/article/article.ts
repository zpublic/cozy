import { Component } from "angular2/core";
import { Model } from "../model";


@Component({
    selector: 'reddit-article',
    inputs: ['article'],
    host: {
        class: 'row'
    }, 
    templateUrl:'build/component/article/article.html'
})
export class ArticleComponent {
    article: Model;
    
    voteUp(): boolean {
        this.article.voteUp();
        return false;
    }

    voteDown(): boolean {
        this.article.voteDown();
        return false;
    }
}