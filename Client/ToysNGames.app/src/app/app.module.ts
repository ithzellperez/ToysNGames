import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductListComponent } from './product-list/product-list.component';
import { DeleteConfirmationModal } from './shared/views/confirmation/confirmation.component';

import { ProductService } from './shared/product-service';
import { ShowModal } from './shared/views/modal/modal.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { MaterialModule } from './material.module';

@NgModule({
  declarations: [
    AppComponent,
    ProductListComponent,
    NavMenuComponent,
    ShowModal,
    DeleteConfirmationModal
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    LayoutModule,
    MaterialModule,
  ],
  entryComponents: [ShowModal, DeleteConfirmationModal],
  providers: [ProductService],
  bootstrap: [AppComponent]
})
export class AppModule { }
