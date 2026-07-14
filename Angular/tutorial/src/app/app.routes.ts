import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Profile } from './pages/profile/profile';
import { Login } from './pages/login/login';
import { PageNotFound } from './pages/page-not-found/page-not-found';
import { UserDetail } from './pages/user-detail/user-detail';
import { User } from './pages/user/user';

//apply lazy loading in the about-us page by using the load component and removig the import from the top of the file and import it into the load component import as we can see 
export const routes: Routes = [

    {path:"",component:Home},
    {path:"about/:name/:age",loadComponent:()=>import('./pages/about-us/about-us').then((c)=>c.AboutUs)},
    {path:"about/:name",loadComponent:()=>import('./pages/about-us/about-us').then((c)=>c.AboutUs)},
    {path:"about",loadComponent:()=> import('./pages/about-us/about-us').then((c)=>c.AboutUs)},//this can be accessed without the parameter like we are giving in the upper ones 
    {path:"profile",component:Profile},
    {path:"login",component:Login},
    {path:"users",component:User},
    {path:"user-detail/:id",component:UserDetail},
    // {path:"container-example",loadComponent:()=>import('./pages/container-example-user/container-example-user').then((c)=>c.ContainerExampleUser)},
    {path:"**",component:PageNotFound},
    //{path:"**",redirectTo:"/"}  redirection automatically if you want with this 
];
