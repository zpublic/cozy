import { Component, OnInit } from "angular2/core";
import { bootstrap } from "angular2/platform/browser";
import { Welcome } from "./component/welcome/welcome";
import { Page } from "./core/page";

@Component({
    selector: "main-view",
    template: "<my-page></my-page>",
    directives: [Page],
    inputs: ["rootPage"]
})
export class App implements OnInit {

    rootPage: Page;

    constructor() {
        this.rootPage = new Welcome();
    }

    ngOnInit() {
        //init
    }
}

bootstrap(App);