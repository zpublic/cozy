import {Page} from 'ionic-angular';

@Page({
  templateUrl: './build/pages/toolbars/toolbars.html'
})
export class ToolbarsPage {
  searchQuery = '';
  items;
  constructor() {
    this.initializeItems();
  }

  initializeItems() {
    this.items = [
      'Angular 1.x',
      'Angular 2',
      'ReactJS',
      'EmberJS',
      'Meteor',
      'Typescript',
      'Dart',
      'CoffeeScript'
    ];
  }

  getItems(searchbar) {
    this.initializeItems();

    var q = searchbar.value;

    if (q.trim() == '') {
      return;
    }

    this.items = this.items.filter((v) => {
      if (v.toLowerCase().indexOf(q.toLowerCase()) > -1) {
        return true;
      }
      return false;
    })
  }
}