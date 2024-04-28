import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {AppComponent} from "./app.component";
import {AppRoutingModule} from "./app-routing.module";
import {MatButton} from "@angular/material/button";
import {UserLayoutComponent} from "../user-management/user-layout/user-layout.component";
import {MainLayoutComponent} from "./main-layout/main-layout.component";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

@NgModule({
  declarations: [
    AppComponent,
    UserLayoutComponent,
    MainLayoutComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterOutlet,
    MatButton,
    HttpClientModule,
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
