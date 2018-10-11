import { Component } from '@angular/core';
import { DialogComponent, DialogService } from "ng2-bootstrap-modal";
import { Global } from '../../imports';

export interface ConfirmModel {
  title: string;
  msg: string;
}

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent extends DialogComponent<ConfirmModel, void> implements ConfirmModel {

  //----------------PROPERTIRS-------------------

  //implementaition of 'ConfirmModel' interface
  title: string;
  msg: string;

  //allow access 'sessionStorage' and 'Global' type via interpolation
  sessionStorage = sessionStorage;
  global = Global;

  //----------------CONSTRUCTOR------------------

  constructor(dialogService: DialogService) {
    super(dialogService);
  }
}
