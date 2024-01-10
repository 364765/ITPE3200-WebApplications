import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOrder } from './order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private baseUrl = 'api/order/';

  constructor(private _http: HttpClient) { }

  getOrders(): Observable<IOrder[]> {
    return this._http.get<IOrder[]>(this.baseUrl);
  }

  createOrder(newOrder: IOrder): Observable<any> {
    const createUrl = 'api/order/create';
    return this._http.post<any>(createUrl, newOrder);
  }

  getOrderById(orderId: number): Observable<any> {
    const url = `${this.baseUrl}/${orderId}`;
    return this._http.get(url);
  }


  updateOrder(orderId: number, newOrder: any): Observable<any> {
    const url = `${this.baseUrl}/update/${orderId}`;
    newOrder.orderId = orderId;
    return this._http.put<any>(url, newOrder);
  }

  deleteOrder(orderId: number): Observable<any> {
    const url = `${this.baseUrl}/delete/${orderId}`;
    return this._http.delete(url);
  }

} 
