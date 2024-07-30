import { Component, Input ,Output, EventEmitter} from '@angular/core';
import { Book } from '../../../Book';

@Component({
  selector: 'app-book-item',
  standalone: true,
  imports: [],
  templateUrl: './book-item.component.html',
  styleUrl: './book-item.component.css'
})
export class BookItemComponent {

  @Input() book!:Book;
  @Output() onDeleteBook = new EventEmitter<Book>();
  authors:string="teste, teste, teste";

  onDelete(book:Book){
    this.onDeleteBook.emit(book);
  }

  onChange(book:Book){
    alert("Ainda não implementado")
  }

}
