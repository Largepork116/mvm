import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IAppHttpResponse, ITrackHttpError } from '@app/interfaces/app-http-response.interface';
import { ThemeService } from '@app/services/others/theme.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { APIDefinition, Columns, Config, DefaultConfig } from 'ngx-easy-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { LogDataChangeService } from '../../../services/api/log-data-change.service';
import { ILogDataChange } from '../../../interfaces/log-data-change.interface';

@Component({
  selector: 'app-log-data-change',
  templateUrl: './log-data-change.component.html',
  styleUrls: ['./log-data-change.component.scss']
})
export class LogDataChangeComponent implements OnInit {

  @ViewChild('table', { static: true }) table: APIDefinition;

  public columns: Columns[] = [
    { key: '', title: '', width: '10px', orderEnabled: false, searchEnabled: false },
    { key: 'table', title: 'Tabla', width: '50px' },
    { key: 'pk', title: 'PK', width: '20px' },
    { key: 'changes', title: 'Cambios', width: '60%' },
    { key: 'updatedBy', title: 'Usuario', width: '15%' },
    { key: 'updatedAt', title: 'Fecha y hora', width: '15%' },
    { key: '', title: '', width: '90px', orderEnabled: false, searchEnabled: false },
  ];

  public collection: any[];
  public configuration: Config;
  public toggledRows = new Set<number>();
  subscriptions = new Subject();

  constructor(private logDataChangeService: LogDataChangeService,
    private themeService: ThemeService,
    private router: Router,
    private modalService: NgbModal) { 

  }

  ngAfterContentInit(): void {
    this.getDocuments();
  }

  ngOnInit(): void {
    this.configuration = { ...DefaultConfig };
    this.configuration.detailsTemplate = true;
    this.configuration.showDetailsArrow = false;
    this.configuration.paginationRangeEnabled = false;
    this.configuration.paginationMaxSize = 7;
    this.configuration.paginationEnabled = false;
  }

  async getDocuments() {
    this.collection = null;
    this.logDataChangeService.get()
      .pipe(takeUntil(this.subscriptions))
      .subscribe(
        (response: IAppHttpResponse<ILogDataChange[]>) => {
          this.collection = response.data;
          console.log("DATA => ", this.collection);
        },
        (error: ITrackHttpError) => {
          console.log("ERROR => ", error);
        }
      );
  }

}
