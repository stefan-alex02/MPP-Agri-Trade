import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {RouterModule, RouterOutlet} from '@angular/router';
import {AppComponent} from "./app.component";
import {AppRoutingModule} from "./app-routing.module";
import {MatButton} from "@angular/material/button";
import {UserLayoutComponent} from "./user-management/user-layout/user-layout.component";
import {MainLayoutComponent} from "./main-layout/main-layout.component";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {AuthInterceptor} from "../guards/auth.interceptor";
import {GoogleMapsModule} from "@angular/google-maps";
import {MatFormFieldModule} from "@angular/material/form-field";
import {FlexLayoutModule} from "@angular/flex-layout";

@NgModule({
  declarations: [
    AppComponent,
    UserLayoutComponent,
    MainLayoutComponent,
  ],
  imports: [
    BrowserModule,
    MatFormFieldModule,
    AppRoutingModule,
    RouterOutlet,
    MatButton,
    HttpClientModule,
    BrowserAnimationsModule,
    GoogleMapsModule,
    FlexLayoutModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
