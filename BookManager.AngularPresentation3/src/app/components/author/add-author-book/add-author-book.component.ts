import { Component, Input, input, OnInit } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { AsyncPipe } from '@angular/common';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Author } from '../../../model/Author';
import { AuthorService } from '../../../services/author.service';
import { AuthorParent } from '../../../model/AuthorParent';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { Book } from '../../../model/Book';

@Component({
  selector: 'app-add-author-book',
  standalone: true,
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    AsyncPipe,
    MatIconModule,
    MatButtonModule,

    MatListModule,
    MatDividerModule],
  templateUrl: './add-author-book.component.html',
  styleUrl: './add-author-book.component.css'
})
export class AddAuthorBookComponent implements OnInit {
  @Input() book?: Book;
  myControl = new FormControl<string | Author>('');
  authorParent?: AuthorParent;
  authors: Author[] = [];
  author: Author = {
    name: ""
  };

  //authorsAdded: Author[] = [];

  authorFilteredOptions?: Observable<Author[]>;

  constructor(private authorService: AuthorService) { }

  ngOnInit(): void {
    // console.log("book", this.book);
    this.authorService.getAuthors(this.author).subscribe(data => {
      this.authorParent = data;
      this.authors = data.authors;
    });

    this.authorFilteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ? this._filter(name as string) : this.authors.slice();
      }),
    );
  }

  displayFn(author: Author): string {

    return author && author.name ? author.name : '';
  }

  private _filter(name: string): Author[] {

    const filterValue = name.toLowerCase();

    return this.authors.filter(option =>
      option.name.toLowerCase().includes(filterValue));
  }

  onAddAuthor(author: Author) {
    if (!this.book?.authors.some(authorAdded => authorAdded.id === author.id)) {
      this.book?.authors.push(author);
    }
    this.myControl.setValue('');
  }

  onDeleteAuthor(author: Author) {
    if (this.book && this.book?.authors) {
      this.book.authors = this.book?.authors.filter(authorBooks => authorBooks.id != author.id);
    }
  }
}
