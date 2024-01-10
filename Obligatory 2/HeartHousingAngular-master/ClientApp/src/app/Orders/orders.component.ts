import { Component, OnInit } from '@angular/core';
import { IOrder } from './order';
import { HttpClient } from '@angular/common/http';
import { OrderService } from './orders.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ["./orders.component.css"]
})

export class OrdersComponent implements OnInit {
  viewTitle: string = 'Table';
  orders: IOrder[] = [];

  constructor(
    private _http: HttpClient,
    private _router: Router,
    private _orderService: OrderService) { }

  deleteOrder(order: IOrder): void {
    const confirmDeleteOrder = confirm(`Are you sure you want to delete the rental "${order.OrderId}"?`);

    if (confirmDeleteOrder) {
      this._orderService.deleteOrder(order.OrderId)
        .subscribe(
          (response) => {
            if (response.success) {
              console.log(response.message);
              this.orders = this.orders.filter(i => i !== order);
            }
          },
          (error) => {
            console.error("Error deleting order: ", error);
          });
    }
  }

  getOrders(): void {
    this._orderService.getOrders()
      .subscribe(data => {
        console.log('All', JSON.stringify(data));
        this.orders = data;
      }
      );
  }

  listOrder: IOrder[] = this.orders;


  navigateToOrderform() {
    this._router.navigate(["/orderform"]);
  }

  ngOnInit(): void {
    this.getOrders();
    console.log("OrdersComponent created");
  }

} 
