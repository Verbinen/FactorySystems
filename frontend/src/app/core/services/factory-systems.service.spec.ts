import { TestBed } from '@angular/core/testing';

import { FactorySystemsService } from './factory-systems.service';
import { HttpClient, HttpHandler } from '@angular/common/http';

describe('FactorySystemsService', () => {
  let service: FactorySystemsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpClient, HttpHandler]
    });
    service = TestBed.inject(FactorySystemsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
