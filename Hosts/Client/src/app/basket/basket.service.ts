import { IProduct } from './../shared/models/product';
import { map } from 'rxjs/operators';
import { Basket, IBasket, IBasketItem } from './../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();


  constructor(private http: HttpClient) { }

  getBasket(id: string){
   return this.http.get(this.baseUrl + 'basket?id=' + id).pipe(
     map((basket: IBasket) => {
       this.basketSource.next(basket);
     })
   );
  }

  setBasket(basket: IBasket){
    return this.http.post(this.baseUrl + 'basket', basket).subscribe(
      (response: IBasket) => {
        this.basketSource.next(response);
      }, error => {
        console.log(error);
      });
  }

  getCurrentBasketValue(){
    return this.basketSource.value;
  }

  addItemBasket(item: IProduct, quantity = 1){
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item,quantity);
    const basket = this.getCurrentBasketValue() ?? this.createBasket(); // call createBasket
    //basket.items.push(itemToAdd);
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
  }
 private  addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = items.findIndex(i => i.id === itemToAdd.id);
    if ( index === -1){
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    } else {
      items[index].quantity += quantity;
    }
    return items;
  }

  // Create a basket
  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity,
      pictureUrl: item.pictureUrl,
      description: item.description
    }
  }

}