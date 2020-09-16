import { ShopParams } from './../shared/models/shopParams';
import { IType } from './../shared/models/productTypes';
import { IBrand } from './../shared/models/productBrand';
import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
 products: IProduct[];
 productBrands: IBrand[];
 productTypes: IType[];
 shopParams = new ShopParams();
 sortOptions = [
   {name: 'Alphabetical', value: 'name'},
   {name: 'Price: Low to high', value: 'priceAsc'},
   {name: 'Price: High to Low', value: 'priceDesc'}
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      console.log(response.data);
    }, error => {
      console.log(error);
    });
  }

  getProductBrands() {
    this.shopService.getProductBrands().subscribe(response => {
      this.productBrands = [{id: 0, name: 'All'}, ...response];
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  getProductTypes() {
    this.shopService.getProductTypes().subscribe(response => {
      this.productTypes = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

   onBrandSelected(brandId: number) {
    this.shopParams.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeIdSelected = typeId;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sortSelected = sort;
    this.getProducts();
  }
}
