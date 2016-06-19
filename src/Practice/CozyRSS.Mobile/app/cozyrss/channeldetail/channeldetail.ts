import {Page, NavParams, NavController, ActionSheet} from 'ionic-angular';
import {ModelService} from '../services/model.service';
import {RSSSource} from '../model'

@Page({
  templateUrl: './build/cozyrss/channeldetail/channeldetail.html'
})
export class ChannelDetailPage {
  channel: string;
  sources = [];

  constructor(private params: NavParams, private models: ModelService, private nav: NavController) {
    this.channel = params.data.channel;
    this.sources = this.models.getSources();
  }

  onSourceChange() {
    this.models.saveSources();
  }

  onSourcePress(source) {
    let actionSheet = ActionSheet.create({
      title: 'Action',
      buttons: [
        {
          text: 'Edit',
          role: 'edit',
          handler: () => {
            console.log("edit " + JSON.stringify(source));
          }
        },
        {
          text: 'remove',
          role: 'remove',
          handler: () => {
            this.models.mapSources(function (sources: RSSSource[]) {
              sources.splice(sources.indexOf(source), 1);
            });
          }
        }
      ],
    });

    this.nav.present(actionSheet);
  }
}