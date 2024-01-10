import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IRental } from './rental';

@Injectable({
  providedIn: 'root'
})
export class RentalService {

  private baseUrl = 'api/rental/';

  constructor(private _http: HttpClient) { }

  getRentals(): Observable<IRental[]> {
    return this._http.get<IRental[]>(this.baseUrl);
  }

  createRental(newRental: IRental): Observable<any> {
    const createUrl = 'api/rental/createRental';
    return this._http.post<any>(createUrl, newRental);
  }

  getRentalById(rentalId: number): Observable<any> {
    const url = `${this.baseUrl}details/${rentalId}`;
    return this._http.get<IRental>(url);
  }

  updateRental(rentalId: number, newRental: any): Observable<any> {
    const url = `${this.baseUrl}/update/${rentalId}`;
    newRental.rentalId = rentalId;
    return this._http.put<any>(url, newRental);
  }

  deleteRental(rentalId: number): Observable<any> {
    const url = `${this.baseUrl}/delete/${rentalId}`;
    return this._http.delete(url);
  }
}
