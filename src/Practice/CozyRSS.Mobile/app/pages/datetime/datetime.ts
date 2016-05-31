import {Page} from 'ionic-angular';


@Page({
  templateUrl: './build/pages/datetime/datetime.html'
})
export class DatetimePage {
    public event = {
    month: '1990-02-19',
    timeStarts: '07:43',
    timeEnds: '1990-02-20'
  }

}
