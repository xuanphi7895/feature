import { Component, OnInit } from '@angular/core';
import { ShopService } from './../shop.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  id: number;

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();
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
