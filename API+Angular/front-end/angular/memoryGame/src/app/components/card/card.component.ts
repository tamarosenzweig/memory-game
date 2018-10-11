import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MyDictionary, GameService, Global } from '../../imports';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent {

  //----------------PROPERTIRS-------------------

  @Input()
  card: MyDictionary;

  @Input()
  canFlip: boolean = true;

  @Input()
  isBoardEnable: boolean;

  @Output()
  flippedEvent: EventEmitter<void>;

  isFlipped: boolean = false;

  //allow access 'sessionStorage' and 'Global' via interpolation
  sessionStorage = sessionStorage;
  global = Global;

  //----------------CONSTRUCTOR------------------

  constructor(private gameService: GameService) {
    this.flippedEvent = new EventEmitter<void>();
    
    this.gameService.updateCardSubject.subscribe(
      () => {
        setTimeout(() => {
          this.isFlipped = false;
        }, 1000);
      });
  }

  //----------------METHODS-------------------

  toggleCard() {
    if (this.isBoardEnable && !this.isFlipped && this.canFlip) {
      this.isFlipped = true;
      this.flippedEvent.emit();
    }
  }
}
