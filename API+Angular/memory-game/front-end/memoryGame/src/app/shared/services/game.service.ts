import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable, Subject } from 'rxjs';
import { Global, MyDictionary } from '../../imports';

@Injectable()
export class GameService {

    //----------------PROPERTIRS-------------------

    basicURL: string = Global.BASE_ENDPOINT + `/game`;
    updateBoardSubject: Subject<MyDictionary[]>;
    updateCardSubject: Subject<void>;

    //----------------CONSTRUCTOR------------------

    constructor(private http: HttpClient) {
        this.updateBoardSubject = new Subject<MyDictionary[]>();
        this.updateCardSubject = new Subject<void>();
    }
    //----------------METHODS-------------------

    //GET
    getGameByUserName(userName: string): Observable<any> {
        let url: string = `${this.basicURL}/getGameByUserName?userName=${userName}`;
        return this.http.get(url);
    }
    //PUT
    checkGameStatus(userName: string, card1: string, card2: string): Observable<any> {
        let formData = new FormData();
        formData.append("userName", userName);
        formData.append("card1", card1);
        formData.append("card2", card2);
        let url: string = `${this.basicURL}/checkGameStatus`;
        return this.http.put(url, formData);
    }
}