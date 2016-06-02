import {FORM_DIRECTIVES, FormBuilder, Validators, Control, ControlGroup} from '@angular/common';
import {Page} from 'ionic-angular';


@Page({
  templateUrl: './build/pages/inputs/inputs.html',
  providers: [FormBuilder]
})
export class InputsPage {
  form;

  constructor() {
    this.form = new ControlGroup({
      firstName: new Control("", Validators.required),
      lastName: new Control("", Validators.required)
    });
  }

  processForm(event) {
    // TODO: display input in a popup
    console.log(event);
  }

}