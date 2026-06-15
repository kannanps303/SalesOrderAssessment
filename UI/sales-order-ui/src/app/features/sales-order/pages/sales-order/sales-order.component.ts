import { Component, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs/internal/Observable';
import { Vendor } from '../../models/vendor.model';
import { Item } from '../../models/item.model';
import { MasterServiceService } from '../../services/master-service.service';

@Component({
  selector: 'app-sales-order',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './sales-order.component.html',
  styleUrls: ['./sales-order.component.scss']
})
export class SalesOrderComponent implements OnInit {

  salesOrderForm!: FormGroup;

  vendors: Vendor[] = [];
  itemsMaster: Item[] = [];

  constructor(private fb: FormBuilder, private service: MasterServiceService) { }

  ngOnInit(): void {
    this.salesOrderForm = this.fb.group({
      vendorCode: [''],
      vendorName: [''],
      docNo: ['SO-001'],
      docDate: [new Date().toISOString().substring(0, 10)],
      items: this.fb.array([])
    });
    this.loadMasters();
  }

  get items(): FormArray {
    return this.salesOrderForm.get('items') as FormArray;
  }


  loadMasters(): void {
    this.service.getVendors().subscribe({
      next: (res) => {
        this.vendors = res;
      },
      error: (err) => {
        console.error('Error loading vendors', err);
      }
    });

    this.service.getItems().subscribe({
      next: (res) => {
        this.itemsMaster = res;
      },
      error: (err) => {
        console.error('Error loading items', err);
      }
    });
  }

  createRow(item?: any): FormGroup {

    return this.fb.group({
      itemId: [item?.id || ''],
      itemCode: [item?.code || ''],
      itemName: [item?.name || ''],
      uom: [item?.uom || ''],
      quantity: [1]
    });

  }

  loadItems(): void {
    this.items.clear();
    this.itemsMaster.forEach(item => {
      this.items.push(this.createRow(item));
    });

  }

  addRow(): void {
    this.items.push(
      this.fb.group({
        itemId: [''],
        itemCode: [''],
        itemName: [''],
        uom: [''],
        quantity: [1]
      })
    );

  }

  removeRow(index: number): void {
    if (this.items.length > 1) {
      this.items.removeAt(index);
    }
  }

  onItemChange(index: number): void {
    const row = this.items.at(index);
    const itemId = Number(row.get('itemId')?.value);
    const selectedItem = this.itemsMaster.find(
      x => x.id === itemId
    );
    if (!selectedItem) {
      return;
    }
    row.patchValue({
      itemName: selectedItem.name,
      uom: selectedItem.uom
    });

  }



  onVendorChange(): void {

    const vendorCode =
      this.salesOrderForm.get('vendorCode')?.value;

    const vendor =
      this.vendors.find(v => v.code === vendorCode);

    if (vendor) {

      this.salesOrderForm.patchValue({
        vendorName: vendor.name
      });

      this.items.clear();

      this.service
        .getVendorItems(vendor.id)
        .subscribe({
          next: (items) => {

            this.itemsMaster = items;

            this.loadItems();
          }
        });
    }
    else {

      this.salesOrderForm.patchValue({
        vendorName: ''
      });

      this.items.clear();

      this.itemsMaster = [];
    }
  }

  save(): void {

    if (this.salesOrderForm.invalid) {
      this.salesOrderForm.markAllAsTouched();
      return;
    }

    const vendorCode =
      this.salesOrderForm.get('vendorCode')?.value;

    const vendor =
      this.vendors.find(v => v.code === vendorCode);

    if (!vendor) {
      alert('Please select a vendor');
      return;
    }

    const payload = {
      docNo: this.salesOrderForm.value.docNo,
      docDate: this.salesOrderForm.value.docDate,
      vendorId: vendor.id,
      items: this.salesOrderForm.value.items.map((x: any) => ({
        itemId: Number(x.itemId),
        quantity: Number(x.quantity)
      }))
    };

    console.log(payload);

    this.service
      .saveSalesOrder(payload)
      .subscribe({
        next: (response) => {

          alert('Sales Order Saved Successfully');

          console.log(response);

        },
        error: (error) => {

          console.error(error);

          alert('Failed to save sales order');

        }
      });
  }

}