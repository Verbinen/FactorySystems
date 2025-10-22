import { TestBed } from '@angular/core/testing';

import { FactorySystemsService } from './factory-systems.service';

describe('FactorySystemsService', () => {
  let service: FactorySystemsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FactorySystemsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
