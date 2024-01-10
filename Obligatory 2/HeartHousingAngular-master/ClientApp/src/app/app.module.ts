import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { RentalsComponent } from "./Rentals/rentals.component";
import { RentalformComponent } from "./Rentals/rentalform.component";
import { OrdersComponent } from "./Orders/orders.component";
import { OrderformComponent } from "./Orders/orderform.component";
import { FavoritesComponent } from "./home/favorites.component";

import { ConvertToCurrency } from "./shared/convert-to-currency.pipe";
import { DetailsComponent } from "./Rentals/details.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatCardModule} from "@angular/material/card";
import { FlexModule } from "@angular/flex-layout";
import { MatIconModule } from '@angular/material/icon';
import {MatButtonModule} from "@angular/material/button";
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RentalsComponent,
    RentalformComponent,
    OrdersComponent,
    OrderformComponent,
    ConvertToCurrency,
    DetailsComponent,
    FavoritesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: "rentals", component: RentalsComponent },
      { path: "rentalform", component: RentalformComponent },
      { path: "favorites", component: FavoritesComponent },
      { path: "orders", component: OrdersComponent },
      { path: "orderform/:mode/:id", component: OrderformComponent },
      { path: "details/:id", component: DetailsComponent },
      { path: "rentalform/:mode/:id", component: RentalformComponent },
      { path: "**", redirectTo: '', pathMatch: 'full'}
    ]),
    BrowserAnimationsModule,
    MatCardModule,
    FlexModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
