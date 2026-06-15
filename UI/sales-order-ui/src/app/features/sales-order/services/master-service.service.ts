import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Vendor } from '../models/vendor.model';
import { Observable } from 'rxjs';
import { Item } from '../models/item.model';

@Injectable({
  providedIn: 'root'
})
export class MasterServiceService {
  private apiUrl = 'https://localhost:7128';

  constructor(private http: HttpClient) { }

  getVendors(): Observable<Vendor[]> {
    return this.http.get<Vendor[]>(
      `${this.apiUrl}/api/master/vendors`
    );
  }

  getItems(): Observable<Item[]> {
    return this.http.get<Item[]>(
      `${this.apiUrl}/api/master/items`
    );
  }

  getVendorItems(vendorId: number): Observable<Item[]> {
    return this.http.get<Item[]>(
      `${this.apiUrl}/api/master/vendor-items/${vendorId}`
    );
  }
  saveSalesOrder(data: any): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/api/salesorder/save`,
      data
    );
  }
}
