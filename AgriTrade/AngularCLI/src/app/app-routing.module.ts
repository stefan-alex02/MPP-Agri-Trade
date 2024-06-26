import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WelcomeComponent } from './welcome/welcome.component';
import {LoginComponent} from "./user-management/login/login.component";
import {UserLayoutComponent} from "./user-management/user-layout/user-layout.component";
import {MainLayoutComponent} from "./main-layout/main-layout.component";
import {CustomerDashboardComponent} from "./customer-dashboard/customer-dashboard.component";
import {StockDetailsComponent} from "./stock-details/stock-details.component";
import {ExploreMapComponent} from "./explore-map/explore-map.component";
import {AuthGuard} from "../guards/auth.guard";
import {BasketComponent} from "./basket/basket.component";
import {RegisterAccountComponent} from "./user-management/register-account/register-account.component";

const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', component: WelcomeComponent},
      { path: 'customer-dashboard', component: CustomerDashboardComponent,
        canActivate: [AuthGuard] },
      { path: 'stocks/:id', component: StockDetailsComponent,
        canActivate: [AuthGuard]},
      { path: 'explore-map', component: ExploreMapComponent,
        canActivate: [AuthGuard]},
      { path:'basket', component: BasketComponent }
    ]
  },
  {
    path: '',
    component: UserLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterAccountComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
