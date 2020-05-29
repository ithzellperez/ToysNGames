import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable, Inject } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { Product } from "./product";

@Injectable(
  {
    providedIn: 'root'
  })

export class ProductService {

  public products: Product[];

  private PRODUCT_API_SERVICE = 'http://localhost:60000/api';
  
  headers = new HttpHeaders({
    'Content-Type':  'application/json',
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'GET,HEAD,OPTIONS,POST,PUT,DELETE',
    'Access-Control-Allow-Headers': 'Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers'
  });

  responseType = "text" as "json"

  constructor(private http: HttpClient) { }

  handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
  
  public getAllProducts(): Observable<boolean> {
    let self = this;
    const requestUrl = `${self.PRODUCT_API_SERVICE}/product`;
    return this.http.get(requestUrl)
        .pipe(
            map((data: Product[]) => {
                self.products = data;
                return true;
            })
        );
  }

  public addNewItem(item){
    let self = this;
    const requestUrl = `${self.PRODUCT_API_SERVICE}/product`;
    const httpOptions = {
      headers: self.headers
    };
    return self.http.post(requestUrl, item, httpOptions).pipe(catchError(this.handleError));
  }

  public updateItem(item){
    let self = this;
    const requestUrl = `${self.PRODUCT_API_SERVICE}/product/${item.id}`;
    const httpOptions = {
      headers: self.headers
    };
    return self.http.put(requestUrl, item, httpOptions).pipe(catchError(this.handleError));
  }

  public deleteItem(id){
    let self = this;
    const requestUrl = `${self.PRODUCT_API_SERVICE}/product/${id}`;
    const httpOptions = {
      headers: self.headers
    };
    return self.http.delete(requestUrl, httpOptions).pipe(catchError(this.handleError));
  }
}
