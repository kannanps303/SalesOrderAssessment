import { Injectable } from '@angular/core';
import { ITEMS } from '../../../assets/mock-data/items';
import { VENDORS } from '../../../assets/mock-data/vendors';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalesorderService {

   getVendors() {
    return of(VENDORS);
  }

  getItems() {
    return of(ITEMS);
  }
}
