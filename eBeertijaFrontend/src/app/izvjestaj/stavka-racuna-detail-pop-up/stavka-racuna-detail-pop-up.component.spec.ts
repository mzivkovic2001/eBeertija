import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StavkaRacunaDetailPopUpComponent } from '@src/app/izvjestaj/stavka-racuna-detail-pop-up/stavka-racuna-detail-pop-up.component';

describe('StavkaRacunaDetailPopUpComponent', () => {
  let component: StavkaRacunaDetailPopUpComponent;
  let fixture: ComponentFixture<StavkaRacunaDetailPopUpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StavkaRacunaDetailPopUpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StavkaRacunaDetailPopUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
