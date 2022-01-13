import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, map } from 'rxjs/operators';
import { ICategory } from '../shared/models/ICategory';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { productParams } from '../shared/models/productParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  getProducts(shopParams: productParams) {
    let params = new HttpParams();
    
    if (shopParams.categoryId !== 0) {
      params = params.append('CategoryId', shopParams.categoryId.toString());
    }
    if(shopParams.search){
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());
    
    return this.http.get<IPagination>(
        this.baseUrl + 'productsGeneric',
        { observe: 'response', params}
      ).pipe(
        map(response =>{
          return response.body;
        })
      );
  }
  
  getProduct(id: number){
    console.clear();
    console.log(id);
    return this.http.get<IProduct>(this.baseUrl + 'ProductsGeneric/' + id);
  }

  getCategories() {
    return this.http.get<ICategory[]>(
      this.baseUrl + 'ProductsGeneric/Category'
    );
  }
}
