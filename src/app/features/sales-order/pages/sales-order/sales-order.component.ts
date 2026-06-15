import { Component, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule
} from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sales-order',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './sales-order.component.html',
  styleUrls: ['./sales-order.component.scss']
})
export class SalesOrderComponent implements OnInit {

  salesOrderForm!: FormGroup;

  vendors = [
    { id: 1, code: 'V001', name: 'ABC Traders' },
    { id: 2, code: 'V002', name: 'XYZ Suppliers' }
  ];

  itemsMaster = [
    {
      id: 1,
      code: 'ITM001',
      name: 'Steel Pipe',
      uom: 'Nos'
    },
    {
      id: 2,
      code: 'ITM002',
      name: 'Valve',
      uom: 'Nos'
    },
    {
      id: 3,
      code: 'ITM003',
      name: 'Paint',
      uom: 'Ltr'
    }
  ];

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {

    this.salesOrderForm = this.fb.group({
      vendorCode: [''],
      vendorName: [''],
      docNo: ['SO-001'],
      docDate: [new Date().toISOString().substring(0, 10)],
      items: this.fb.array([])
    });
  }

  get items(): FormArray {
    return this.salesOrderForm.get('items') as FormArray;
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

  save(): void {
    console.log('Sales Order Data:', this.salesOrderForm.value);
    alert('Sales Order Saved');
  }

  onVendorChange(): void {

  const vendorCode = this.salesOrderForm.get('vendorCode')?.value;

  const vendor = this.vendors.find(v => v.code === vendorCode);

  if (vendor) {

    this.salesOrderForm.patchValue({
      vendorName: vendor.name
    });

    // Clear existing rows
    this.items.clear();

    this.loadItems();
  }
  else {

    this.salesOrderForm.patchValue({
      vendorName: ''
    });

    this.items.clear();

  }
}

}