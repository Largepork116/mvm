export interface ILogDataChange {
  dataChangeId: number;
  table: string;
  pk: number;
  changes: string;
  updatedBy: string;
  updatedAt: string;
}