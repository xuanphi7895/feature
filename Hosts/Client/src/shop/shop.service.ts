import { IType } from './../shared/models/productTypes';
import { IBrand } from './../shared/models/productBrand';
import { IPagination } from './../shared/models/pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, delay } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';
  constructor(private http: HttpClient) { }
  // tslint:disable-next-line: typedef
  getProducts(brandId?: number, typeId?: number, sort?: string){
    let params = new HttpParams();

    if (brandId){
      params = params.append('brandId', brandId.toString());
    }
    if (typeId){
      params = params.append('typeId', typeId.toString());
    }
    if (sort) {
      params = params.append('sort', sort);
    }

    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})
            .pipe(
              delay(1000),
              map(response => {
                return response.body;
              })
            );
  }

  // tslint:disable-next-line: typedef
  getProductBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  // tslint:disable-next-line: typedef
  getProductTypes(){
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
}
