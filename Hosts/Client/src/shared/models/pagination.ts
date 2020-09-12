import {IProduct} from './product';

export interface IPagination{
    pageSize: number;
    pageIndex: number;
    total: number;
    data: IProduct[];
}