import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../product.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(
    private shopService: ShopService,
    private activateRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.loadproduct()
  }

  loadproduct(){
    debugger;
    this.shopService.getProduct(
      Number(this.activateRoute.snapshot.paramMap.get('id'))
    )
    .subscribe(product => {
      this.product = product;
    }, error => {
      console.error(error);
    });
  }

}
