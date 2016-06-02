import {Page} from 'ionic-angular';

@Page({
    templateUrl : './build/pages/searchbars/searchbars.html'
})
export class SearchBarsPage{
    searchQuery: string = '';
    items;
    constructor(){
        this.initializeItems();
    }
    
    initializeItems(){
        this.items = [
            'Amsterdam',
      'Bogota',
      'Buenos Aires',
      'Cairo',
      'Dhaka',
      'Edinburgh',
      'Geneva',
      'Genoa',
      'Glasglow',
      'Hanoi',
      'Hong Kong',
      'Islamabad',
      'Istanbul',
      'Jakarta',
      'Kiel',
      'Kyoto',
      'Le Havre',
      'Lebanon',
      'Lhasa',
      'Lima',
      'London',
      'Los Angeles',
      'Madrid',
      'Manila',
      'New York',
      'Olympia',
      'Oslo',
      'Panama City',
      'Peking',
      'Philadelphia',
      'San Francisco',
      'Seoul',
      'Taipeh',
      'Tel Aviv',
      'Tokio',
      'Uelzen',
      'Washington'
        ];
    }
    
    getItems(searchbar){
        this.initializeItems();
        
        var q = searchbar.value;
        
        if(q.trim() == ''){
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