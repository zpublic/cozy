import {Component} from "angular2/core";
import {NgFor} from "angular2/common";
import {Article} from "../article/article";
import {Page} from "../../core/page";

@Component({
    selector: "my-app",
    templateUrl: "build/component/welcome/welcome.html",
    directives: [Article]
})
export class Welcome implements Page {

    name: string;
    names: string[];

    constructor() {
        this.name = "hello";
        this.names = ["Ari", "Carlos", "Felipe", "Nate"];
    }

    addArticle(title: HTMLInputElement, link: HTMLInputElement): void {
        console.log(`Adding article title: ${title.value} and link: ${link.value}`);
    }
}