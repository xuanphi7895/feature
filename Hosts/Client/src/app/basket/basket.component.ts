import { IBasket } from './../shared/models/basket';
import { BasketService } from './basket.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {

  id: string;
  basket: IBasket;
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.getBasket();
    console.log(this.getBasket);
  }

  getBasket(){
    return this.basketService.getBasket(this.id);
  }

  setBasket(){
    return this.basketService.setBasket(this.basket);
  }

}
