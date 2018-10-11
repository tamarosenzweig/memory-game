import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import { BootstrapModalModule } from 'ng2-bootstrap-modal';

import {
  AppComponent,
  SignInComponent,
  ChoosePartnerComponent,
  ConfirmComponent,
  GameComponent,
  BoardComponent,
  CardComponent,
  UserService,
  GameService,
  routing
}
  from './imports';

@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
    ChoosePartnerComponent,
    ConfirmComponent,
    GameComponent,
    BoardComponent,
    CardComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    RouterModule,
    HttpClientModule,
    BootstrapModalModule.forRoot({container:document.body}),
    routing
  ],
  entryComponents: [
    ConfirmComponent
  ],
  providers: [UserService,GameService],
  bootstrap: [AppComponent]
})
export class AppModule { }
