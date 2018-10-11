import { Routes, RouterModule } from '@angular/router';

import {
    SignInComponent,
    ChoosePartnerComponent,
    GameComponent
} from './imports';

export const routes: Routes = [
    {
        path: 'memoryGame',
        children: [
            { path: 'signIn', component: SignInComponent },
            { path: 'choosePartner', component: ChoosePartnerComponent },
            { path: 'game', component: GameComponent },
        ]
    },
    { 
        path: '', redirectTo: 'memoryGame/signIn', pathMatch: 'prefix' 
    },
    // otherwise redirect to home
    { 
        path: '**', redirectTo: 'memoryGame/signIn' 
    }
];

export const routing = RouterModule.forRoot(routes);