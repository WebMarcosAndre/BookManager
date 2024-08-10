import { Author } from "./Author";

export interface Book {
    id?: number,
    title: string,
    publisherBook: string,
    edition: number,
    yearPublication: string,
    authors: Author[],
    authorIds?: (number|undefined)[]
}