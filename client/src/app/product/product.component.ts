import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ICategory } from '../shared/models/ICategory';
import { IProduct } from '../shared/models/product';
import { productParams } from '../shared/models/productParams';
import { ShopService } from './product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  products: IProduct[];
  categories: ICategory[];
  totalCount: number = 18;
  @ViewChild('search', {static:true}) searchTerm: ElementRef;

  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ];
  

  productParams = new productParams();

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
  }

  getProducts(){
    this.shopService
      .getProducts(this.productParams)
      .subscribe(response => {
      this.products = response!.data;
      this.productParams.pageNumber = response!.pageIndex;
      this.productParams.pageSize = response!.pageSize;
      this.totalCount = response!.count;
    }, error => {
      console.error(error);
    });
  }

  getCategories(){
    this.shopService.getCategories().subscribe(response => {
      this.categories = [{id: 0, name: "All"}, ...response];
    }, error => {
      console.error(error);
    })
  }

  onSortSelected(sort: string){
    this.productParams.sort = this.sortOptions.find(x => x.name==sort)!.value;
    this.getProducts();
  }
  
  onCategorySelected(categoryId: number){
    this.productParams.categoryId = categoryId;
    this.getProducts();
  }


  onPageChanged(event: any){
    if (this.productParams.pageNumber !== event) {
      this.productParams.pageNumber = event;
      this.getProducts();
    }
  }

  onSearch(){
    this.productParams.search = this.searchTerm.nativeElement.value;
    this.getProducts();
  }
  
}
