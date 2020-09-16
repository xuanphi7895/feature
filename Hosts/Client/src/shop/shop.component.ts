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
 brandIdSelected = 0;
 typeIdSelected = 0;
 sortSelected = 'name';
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
    this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected, this.sortSelected).subscribe(response => {
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
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.typeIdSelected = typeId;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.sortSelected = sort;
    this.getProducts();
  }
}
