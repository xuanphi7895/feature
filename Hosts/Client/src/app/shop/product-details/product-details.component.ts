import { Component, OnInit } from '@angular/core';
import { ShopService } from './../shop.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  id: number;
  quantity = 1;

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute,
              private basketService: BasketService ) { }

  ngOnInit(): void {
    this.loadProduct();
  }
  addItemToBasket(){  
    this.basketService.addItemBasket(this.product, this.quantity);
  }

  incrementQuantity(){
    this.quantity++;
  }
  decrementQuantity(){
    if (this.quantity > 1){
    this.quantity--;
    }
  }

  loadProduct(){
    this.id = parseInt(this.activateRoute.snapshot.paramMap.get('id'));
    this.shopService.getProduct(this.id).subscribe(product => {
      this.product = product;
    }, error => {
      console.log(error);
    });
  }

}
