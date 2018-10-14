//-----------shared------------
//models
export { User } from './shared/models/user.model';
export { Game } from './shared/models/game.model';
export { MyDictionary } from './shared/models/my-dictionary.model';

//validators
export { stringValidatorArr, numberValidatorArr } from './shared/validators/sign-in.validator';

//services
export { UserService } from './shared/services/user.service'
export { GameService } from './shared/services/game.service';

export { Global } from './shared/global';

//components
export { AppComponent } from './app.component';
export { SignInComponent } from './components/sign-in/sign-in.component';
export { ChoosePartnerComponent } from './components/choose-partner/choose-partner.component';
export { ConfirmComponent } from './components/confirm/confirm.component';
export { GameComponent } from './components/game/game.component';
export { BoardComponent } from './components/board/board.component';
export { CardComponent } from './components/card/card.component';

//routing
export { routing } from './app.routing';