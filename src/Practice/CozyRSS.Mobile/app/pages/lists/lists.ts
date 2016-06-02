import {Page} from 'ionic-angular';

@Page({
  templateUrl: './build/pages/lists/lists.html'
})
export class ListsPage {
  items= [
    {
      title: 'Pokémon Yellow',
    },
    {
      title: 'Super Metroid',
    },
    {
      title: 'Mega Man X',
    },
    {
      title: 'The Legend of Zelda',
    },
    {
      title: 'Pac-Man',
    },
  ];
  
  itemSelected(item){
    console.log(item.title);
  }
}