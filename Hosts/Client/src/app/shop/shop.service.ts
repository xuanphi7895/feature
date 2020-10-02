import { ShopParams } from './../shared/models/shopParams';
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
  getProducts(shopParams: ShopParams){
    let params = new HttpParams();

    if (shopParams.brandId !== 0){
      params = params.append('brandId', shopParams.brandId.toString());
    }
    if (shopParams.typeId !== 0){
      params = params.append('typeId', shopParams.typeId.toString());
    }
    if (shopParams.search){
      params = params.append('search', shopParams.search);
      console.log(params);
    }
    params = params.append('sort', shopParams.sortSelected);
    params = params.append('pageIndex', shopParams.pageIndex.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());
    console.log(params);
    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})
            .pipe(
              delay(1000),
              map(response => {
                console.log(response.body);
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
