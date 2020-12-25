export interface Advertisement {
    id: number;
    name: string;
    description: string;
    price: number;
    categoryId: number;
    images: Array<string>;
}
