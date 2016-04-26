import {Component, OnInit} from 'angular2/core';

@Component({
    selector: 'my-article',
    templateUrl: 'build/component/article/article.html'
})
export class Article implements OnInit {

    votes: number;
    title: string;
    link: string;

    constructor() {
        this.votes = 10;
        this.title = 'Angular 2';
        this.link = 'http://angular.io';
    }

    ngOnInit() { }

    voteUp(): boolean {
        this.votes += 1;
        return false;
    }

    voteDown(): boolean {
        this.votes -= 1;
        return false;
    }

}