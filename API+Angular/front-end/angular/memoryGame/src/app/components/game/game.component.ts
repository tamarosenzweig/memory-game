import { Component, OnInit } from '@angular/core';
import { asEnumerable } from 'linq-es2015';
import { DialogService } from 'ng2-bootstrap-modal';
import { Router } from '@angular/router';
import { Global, ConfirmComponent, User, UserService, GameService, MyDictionary } from '../../imports';
@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  //----------------PROPERTIRS-------------------

  game: { cardArray: MyDictionary[], currentTurn: string };
  user: User;
  userSetsCounter: number;
  partnerSetsCounter: number;
  toShowDialog: boolean;
  checkGameOverHandler:NodeJS.Timer;

  //allow access 'sessionStorage' and 'Global' via interpolation
  sessionStorage = sessionStorage;
  global = Global;

  //----------------CONSTRUCTOR------------------

  constructor(private gameService: GameService, private userSevice: UserService, private dialogService: DialogService, private router: Router) {
    this.game = { cardArray: null, currentTurn: '' };
    this.userSetsCounter = 0;
    this.partnerSetsCounter = 0;
    this.toShowDialog = true;

    this.checkGameOverHandler = setInterval(() => {
      this.checkGameOver();
    }, 1000);
  }

  //----------------METHODS-------------------

  ngOnInit() {
    let userName: string = sessionStorage.getItem(Global.USER);
    this.userSevice.getUser(userName)
      .subscribe((user: User) => {
        this.user = user;
        setInterval(() => {
          this.getGameByUserName(userName, false);
        }, 100);
      },
        err => {
          console.log(err);
        });
  }

  getGameByUserName(userName: string, isFinishedTurn: boolean) {
    this.gameService.getGameByUserName(userName).subscribe((game: any) => {
      game.cardArray = Object.keys(game.cardArray).map(key => ({ key: key, value: game.cardArray[key] }));
      this.game = game;
      if (isFinishedTurn) {
        
        //update board cards
        this.gameService.updateCardSubject.next();
      }
      this.gameService.updateBoardSubject.next(game.cardArray);
      
      //update sets counter
      this.userSetsCounter = asEnumerable(this.game.cardArray).Count(card => card.value == this.user.userName);
      this.partnerSetsCounter = asEnumerable(this.game.cardArray).Count(card => card.value == this.user.partnerUserName);
    },
      err => {
        console.log(err);
      }
    );
  }

  checkMatchingHandler(flippedCards: { card1: MyDictionary, card2: MyDictionary }) {
    this.gameService.checkGameStatus(this.user.userName, flippedCards.card1.key, flippedCards.card2.key)
      .subscribe((isFinished: boolean) => {
        this.getGameByUserName(this.user.userName, true);
        if (isFinished) {
          let message: string = this.getMessage();
          this.showWinner(message);
        }
      },
        err => {
          console.log(err);
        });
  }

  showWinner(msg: string) {
    if (this.toShowDialog) {
      this.dialogService.addDialog(ConfirmComponent, {
        title: 'Game is over',
        msg: msg,
      })
        .subscribe(() => {
          this.router.navigate(['/memoryGame/choosePartner']);
        });
    }
    this.toShowDialog = false;
  }

  checkGameOver() {
    let isOver: boolean = asEnumerable(this.game.cardArray).All(card => card.value != null);
    if (isOver) {
      clearInterval(this.checkGameOverHandler);
      let message: string = this.getMessage();
      this.showWinner(message);
    }
  }

  getMessage(): string {
    let message: string;
    if (this.userSetsCounter > this.partnerSetsCounter) {
      message = `${this.user.userName} won!!!`;
      this.userSetsCounter = asEnumerable(this.game.cardArray).Count(card => card.value == this.user.userName);
      this.user.score++;
    }
    else if (this.userSetsCounter < this.partnerSetsCounter)
      message = `${this.user.partnerUserName} won!!!`;
    else {
      message = 'Teiko!!';
      this.user.score++;
    }
    message += `Your score number: ${this.user.score}`;
    return message;
  }
}
