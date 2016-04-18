import { Component } from "angular2/core";
import { HelloWorldPage } from "../helloworld/helloworld";

@Component({
    selector: "my-app",
    templateUrl: "build/pages/main/main.html"
})
export class MainPage {

    rootPage: any;

    constructor() {
        // Todo
    }

    init(): void {
        this.rootPage = HelloWorldPage;
    }
}