import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAuthorBookComponent } from './add-author-book.component';

describe('AddAuthorBookComponent', () => {
  let component: AddAuthorBookComponent;
  let fixture: ComponentFixture<AddAuthorBookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddAuthorBookComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddAuthorBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
