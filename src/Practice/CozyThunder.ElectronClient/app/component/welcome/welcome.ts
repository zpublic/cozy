import { Component } from "angular2/core";
import { NgFor } from "angular2/common";

@Component({
    selector: "my-app",
    templateUrl: "build/component/welcome/welcome.html"
})
export class Welcome {

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