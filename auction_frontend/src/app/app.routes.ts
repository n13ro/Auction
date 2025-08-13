import { Routes } from '@angular/router';
import { Main } from './main/main';
import { Register } from './register/register';
import { Login } from './login/login';
import { Lot } from './lot/lot';
import { Bet } from './bet/bet';
import { UserPage } from './user-page/user-page';

export const routes: Routes = [
    {path: '',component:Main},
    {path: 'register', component:Register},
    {path: 'login', component: Login},
    {path: 'lot', component:Lot},
    {path: 'test', component:Bet},
    {path: 'me', component:UserPage}
];
