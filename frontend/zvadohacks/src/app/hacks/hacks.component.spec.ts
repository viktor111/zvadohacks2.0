import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HacksComponent } from './hacks.component';

describe('HacksComponent', () => {
  let component: HacksComponent;
  let fixture: ComponentFixture<HacksComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HacksComponent]
    });
    fixture = TestBed.createComponent(HacksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
