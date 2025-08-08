import { Routes } from '@angular/router';
import { Main } from './main/main';
import { Register } from './register/register';
import { Login } from './login/login';

export const routes: Routes = [
    {path: '',component:Main},
    {path: 'register', component:Register},
    {path: 'login', component: Login}
];
