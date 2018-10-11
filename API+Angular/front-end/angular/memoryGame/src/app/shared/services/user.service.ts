import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Global } from '../../imports';

@Injectable()
export class UserService {
    //----------------PROPERTIRS-------------------

    basicURL: string = Global.BASE_ENDPOINT + `/user`;

    //----------------CONSTRUCTOR------------------

    constructor(private http: HttpClient) { }

    //----------------METHODS-------------------

    //POST
    signIn(userName: string, age: number): Observable<any> {
        let formData = new FormData();
        formData.append("userName", userName);
        formData.append("age", age.toString());
        let url: string = `${this.basicURL}/signIn`;
        return this.http.post(url, formData);
    }

    //GET
    getPartners(): Observable<any> {
        let url: string = `${this.basicURL}/getPartners`;
        return this.http.get(url);
    }

    //GET
    getUser(userName: string): Observable<any> {
        let url: string = `${this.basicURL}/getUser?userName=${userName}`;
        return this.http.get(url);
    }

    //PUT
    setPartner(currentUserName: string, partnerUserName: string): Observable<any> {
        let formData = new FormData();
        formData.append("currentUserName", currentUserName);
        formData.append("partnerUserName", partnerUserName);
        let url: string = `${this.basicURL}/setPartner`;
        return this.http.put(url, formData);
    }
}
