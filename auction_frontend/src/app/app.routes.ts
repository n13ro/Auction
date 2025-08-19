import { Routes } from '@angular/router';
import { Main } from './main/main';
import { Register } from './register/register';
import { Login } from './login/login';
import { Lot } from './lot/lot';
import { UserPage } from './user-page/user-page';
import { Carousel } from './carousel/carousel';
import { CreateLot } from './create-lot/create-lot';
import { Test } from './test/test';

export const routes: Routes = [
    {path: '',component:Test},
    {path: 'register', component:Register},
    {path: 'login', component: Login},
    {path: 'lot', component:Lot},
    {path: 'me', component:UserPage},
    {path: 'car', component:Carousel},
    {path: 'a', component: CreateLot},
    {path: 'test', component: Test}
];
