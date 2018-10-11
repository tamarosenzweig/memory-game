import {User,MyDictionary} from '../../imports';

export class Game {
    constructor(
        public player1: User,
        public player2: User,
        public currentTurn: string,
        public cardArray:MyDictionary[]
    ) { }
}
