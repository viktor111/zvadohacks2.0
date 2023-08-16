import { TestBed } from '@angular/core/testing';

import { BlogHttpServiceService } from './blog-http-service.service';

describe('BlogHttpServiceService', () => {
  let service: BlogHttpServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlogHttpServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
