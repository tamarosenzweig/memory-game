import { environment } from '../../environments/environment';

export class Global {

    //base endpoint for user management RESTful APIs
    public static HOST: string = environment.host;
    public static BASE_ENDPOINT:string =`${Global.HOST}/api`;
    public static USER:string='user';
    public static MYCOLOR:string='myColor';
}