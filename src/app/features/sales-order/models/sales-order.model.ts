import { SalesOrderItem } from "./sales-order-item.model";

export interface SalesOrder {
  id: number;
  docNo: string;
  docDate: Date;
  vendorId: number;
  vendorName: string;
  items: SalesOrderItem[];
}