import { CheckoutComponent } from './checkout/checkout.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NotfoundComponent } from './core/notfound/notfound.component';

const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: 'Home'}},
  {path: 'test-error', component: TestErrorComponent, data: {breadcrumb: 'Test-Error'}},
  {path: 'server-error', component: ServerErrorComponent, data: {breadcrumb: 'Server-Error'}},
  {path: 'not-found', component: NotfoundComponent, data: {breadcrumb: 'Not-Found'}},
   {path: 'shop', loadChildren: () => import('./shop/shop.module').then(mode => mode.ShopModule), data:{breadcrumb: 'Shop'}},
   {path: 'basket', loadChildren: () => import('./basket/basket.module').then(mode => mode.BasketModule), data:{breadcrumb: 'Basket'}},
   {path: 'checkout', loadChildren: () => import('./checkout/checkout.module').then(mode => mode.CheckoutModule), data: { breadcrumb: 'Checkout'}},
   {path: 'account', loadChildren: () => import('./account/account.module').then(mode => mode.AccountModule), data: { breadcrumb: {skip : true}}},
   
   {path: '**', redirectTo: 'not-found', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
