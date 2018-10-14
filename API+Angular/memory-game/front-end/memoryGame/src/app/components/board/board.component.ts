import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MyDictionary, GameService } from '../../imports';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {

    //----------------PROPERTIRS-------------------

  boardCards: MyDictionary[];
  countFlippedCards: number;
  flippedCards: { card1: MyDictionary, card2: MyDictionary };

  @Input()
  cards: MyDictionary[];

  @Input()
  isEnable:boolean;

  @Output()
  checkMatchingEvent: EventEmitter<{ card1: MyDictionary, card2: MyDictionary }>;

    //----------------CONSTRUCTOR------------------

  constructor(private gameService: GameService) {
    this.cards=[];
    this.boardCards = [];
    this.countFlippedCards = 0;
    this.flippedCards = { card1: null, card2: null };
    this.checkMatchingEvent = new EventEmitter<{ card1: MyDictionary, card2: MyDictionary }>();
    
    //update board cards using next method
    this.gameService.updateBoardSubject.subscribe(
      (next:MyDictionary[]) => {
          this.updateBoard(next);
      });
  }

    //----------------METHODS-------------------

  ngOnInit() {
    //duplicating and mixing cards
    let tmpCards: MyDictionary[] = [...this.cards].concat([...this.cards]);
    while (tmpCards.length > 0) {
      let index: number = Math.floor(Math.random() * tmpCards.length);
      this.boardCards.push(tmpCards[index]);
      tmpCards.splice(index, 1);
    }
    console.log(this.boardCards);
  }
  
  CardFippedHandler(card: MyDictionary) {
    //first flipping
    if (this.countFlippedCards == 0) {
      this.countFlippedCards++;
      this.flippedCards.card1 = card;
    }
    //second flipping
    else {
      this.countFlippedCards++;
      this.flippedCards.card2 = card;
      this.checkMatchingEvent.emit(this.flippedCards);
      this.resetFlippedCards();
    }
  }

  resetFlippedCards() {
    this.flippedCards.card1 = null;
    this.flippedCards.card2 = null;
    setTimeout(() => {
      this.countFlippedCards = 0;
    }, 1000);
   
  }

  updateBoard(cards:MyDictionary[]) {
    cards.forEach(card => {
      this.boardCards.forEach(boardCard => {
        if (boardCard.key == card.key)
          boardCard.value = card.value;
      });
    });
  }
}
