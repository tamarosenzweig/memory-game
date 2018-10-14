import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from "ng2-bootstrap-modal";
import { UserService, User, Global, Game, ConfirmComponent } from '../../imports';

@Component({
  selector: 'app-choose-partner',
  templateUrl: './choose-partner.component.html',
  styleUrls: ['./choose-partner.component.css']
})
export class ChoosePartnerComponent implements OnInit {

  //----------------PROPERTIRS-------------------

  users: User[];
  partnersHandler:NodeJS.Timer;
  userHandler:NodeJS.Timer;
  toShowDialog: boolean;

  //----------------CONSTRUCTOR------------------

  constructor(private userService: UserService, private router: Router, private dialogService: DialogService) {
    this.users=[];
    this.toShowDialog=true;
   }

  //----------------METHODS-------------------

  ngOnInit() {
    this.partnersHandler = setInterval(() => { this.getPartners(); }, 1000);
    this.userHandler = setInterval(() => { this.getCurrentUser(); }, 1000);
  }

  getPartners() {
    this.userService.getPartners()
      .subscribe((users: User[]) => {
        this.users = users;
        let index: number = this.users.findIndex(user => user.userName == sessionStorage.getItem(Global.USER));
        this.users.splice(index, 1);
      },
        err => {
          console.log(err);
        });
  }

  getCurrentUser() {
    let userName: string = sessionStorage.getItem(Global.USER);
    this.userService.getUser(userName)
      .subscribe((user: User) => {
        if (user.partnerUserName != null) {
          clearInterval(this.partnersHandler);
          clearInterval(this.userHandler);
          this.userService.getUser(user.partnerUserName)
            .subscribe((partnerUser: User) => {
              let msg = this.getMessage(partnerUser.userName, partnerUser.age);
              this.showPartnerDetails(msg);
            },
              err => {
                console.log(err);
              });
        }
      },
        err => {
          console.log(err);
        });
  }

  choosePartner(index: number) {
    let currentUserName: string = sessionStorage.getItem(Global.USER);
    let partnerUserName: string = this.users[index].userName;
    this.userService.setPartner(currentUserName, partnerUserName)
      .subscribe((game: Game) => {
        console.log(game.cardArray);
        let msg: string = this.getDialogContent(game);
        this.showPartnerDetails(msg);
      },
        err => {
          console.log(err);
        });
  }

  showPartnerDetails(msg: string) {
    if (this.toShowDialog) {
      this.dialogService.addDialog(ConfirmComponent, {
        title: 'you Have a partner',
        msg: msg
      })
        .subscribe(() => {
          this.router.navigate(['memoryGame/game']);
        });
    }
    this.toShowDialog = false;
  }

  getDialogContent(game: Game): string {
    let msg: string;
    if (sessionStorage.getItem(Global.USER))
      msg = this.getMessage(game.player2.userName, game.player2.age);
    else
      msg = this.getMessage(game.player1.userName, game.player1.age);
    return msg;
  }
  
  getMessage(userName: string, age: number): string {
    let msg: string;
    msg = `Partner name: ${userName}\n Partner Age: ${age}`;
    return msg;
  }
}
